using AutoMapper;
using Forage.Core.Entities;
using Forage.Core.Repositories;
using Forage.Data.Context;
using Forage.Service.Dtos.Courses;
using Forage.Service.Extensions;
using Forage.Service.Responses;
using Forage.Service.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Forage.Service.Services.Implementations
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _repository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _evn;
        private readonly IHttpContextAccessor _http;
        private readonly ISkillRepository _skillRepository;
        private readonly ICourseSkillRepository _courseSkillRepository;
        private readonly ForageAppDbContext _context;
        public CourseService(ICourseRepository repository, IMapper mapper, IWebHostEnvironment evn, IHttpContextAccessor http, ISkillRepository skillRepository, ICourseSkillRepository courseSkillRepository, ForageAppDbContext context)
        {
            _repository = repository;
            _mapper = mapper;
            _evn = evn;
            _http = http;
            _skillRepository = skillRepository;
            _courseSkillRepository = courseSkillRepository;
            _context = context;
        }
        public async Task<ApiResponse> CreateAsync(CoursePostDto dto)
        {
            Course Course = _mapper.Map<Course>(dto);
            Course.AboutImage = dto.AboutImage.CreateImage(_evn.WebRootPath, "Images/Courses");
            Course.AboutImageUrl = _http.HttpContext?.Request.Scheme + "://" + _http.HttpContext?.Request.Host
                + $"Images/Courses/{Course.AboutImage}";
            Course.AboutVideo = dto.AboutVideo.CreateImage(_evn.WebRootPath, "Videos/Courses");
            Course.AboutVideoUrl = _http.HttpContext?.Request.Scheme + "://" + _http.HttpContext?.Request.Host
                + $"Videos/Courses/{Course.AboutVideo}";
            foreach (var item in dto.SkillIds)
            {
                if (!await _skillRepository.isExsist(x => x.Id == item))
                {
                    return new ApiResponse
                    {
                        StatusCode = 404,
                        Description = "Invalid Skill Id"
                    };
                }
                CourseSkill CourseSkill = new CourseSkill
                {
                    CreatedAt = DateTime.Now,
                    Course = Course,
                    SkillId = item
                };
                await _courseSkillRepository.AddAsync(CourseSkill);
                Course.CourseSkills?.Add(CourseSkill);
            }
            await _repository.AddAsync(Course);
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 201,
                items = Course
            };
        }

        public async Task<ApiResponse> GetAllAsync()
        {
            IEnumerable<Course> Companies = await _repository.GetAllAsync(x => !x.IsDeleted);
            return new ApiResponse
            {
                items = Companies,
                StatusCode = 200
            };
        }

        public async Task<ApiResponse> GetAsync(int id)
        {
            Course? Course = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (Course == null)
            {
                return new ApiResponse
                {
                    StatusCode = 404
                };
            }
            return new ApiResponse
            {
                StatusCode = 200,
                items = Course
            };
        }

        public async Task<ApiResponse> RemoveAsync(int id)
        {
            Course Course = await _repository.GetAsync(x => x.Id == id);
            if (Course is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            Course.IsDeleted = true;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = Course
            };
        }

        public async Task<ApiResponse> UpdateAsync(int id, CourseUpdateDto dto)
        {
            Course Course = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (Course is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            if (dto.AboutImage is not null)
            {
                Course.AboutImage = dto.AboutImage.CreateImage(_evn.WebRootPath, "Images/Courses");
                Course.AboutImageUrl = _http.HttpContext?.Request.Scheme + "://" + _http.HttpContext?.Request.Host
                    + $"Images/Courses/{Course.AboutImage}";
            }
            if (dto.AboutVideo is not null)
            {
                Course.AboutVideo = dto.AboutVideo.CreateImage(_evn.WebRootPath, "Videos/Courses");
                Course.AboutVideoUrl = _http.HttpContext?.Request.Scheme + "://" + _http.HttpContext?.Request.Host
                    + $"Videos/Courses/{Course.AboutVideo}";
            }
            List<CourseSkill> RemoveableSkill = await _context.CourseSkills.
              Where(x => !dto.SkillIds.Contains(x.SkillId) && x.CourseId == Course.Id).ToListAsync();

            _context.CourseSkills.RemoveRange(RemoveableSkill);
            foreach (var item in dto.SkillIds)
            {
                if (_context.CourseSkills.Where(x => x.CourseId == id && x.SkillId == item).Count() > 0)
                    continue;

                await _courseSkillRepository.AddAsync(new CourseSkill
                {
                    CourseId = id,
                    SkillId = item,
                });
            }
            Course.UpdatedAt = DateTime.UtcNow.AddHours(4);
            Course.Name = dto.Name;
            Course.CourseDuration = dto.CourseDuration;
            Course.About = dto.About;
            Course.CompanyId = dto.CompanyId;
            Course.CourseCategoryId = dto.CourseCategoryId;
            Course.CourseLevelId = dto.CourseLevelId;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = Course
            };
        }
    }
}
