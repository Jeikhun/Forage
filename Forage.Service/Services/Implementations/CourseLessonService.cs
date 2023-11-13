using AutoMapper;
using Forage.Core.Entities;
using Forage.Core.Repositories;
using Forage.Data.Context;
using Forage.Service.Dtos.CourseLessons;
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
    public class CourseLessonService : ICourseLessonService
    {
        private readonly ICourseLessonRepository _repository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _evn;
        private readonly IHttpContextAccessor _http;
        private readonly ISkillRepository _skillRepository;
        private readonly ForageAppDbContext _context;
        public CourseLessonService(ICourseLessonRepository repository, IMapper mapper, IWebHostEnvironment evn, IHttpContextAccessor http, ISkillRepository skillRepository,  ForageAppDbContext context)
        {
            _repository = repository;
            _mapper = mapper;
            _evn = evn;
            _http = http;
            _skillRepository = skillRepository;
            _context = context;
        }
        public async Task<ApiResponse> CreateAsync(CourseLessonPostDto dto)
        {
            CourseLesson CourseLesson = _mapper.Map<CourseLesson>(dto);
            CourseLesson.Video = dto.Video.CreateImage(_evn.WebRootPath, "Videos/CourseLessons");
            CourseLesson.VideoUrl = _http.HttpContext?.Request.Scheme + "://" + _http.HttpContext?.Request.Host
                + $"Videos/CourseLessons/{CourseLesson.Video}";
            await _repository.AddAsync(CourseLesson);
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 201,
                items = CourseLesson
            };
        }

        public async Task<ApiResponse> GetAllAsync()
        {
            IEnumerable<CourseLesson> Companies = await _repository.GetAllAsync(x => !x.IsDeleted);
            return new ApiResponse
            {
                items = Companies,
                StatusCode = 200
            };
        }

        public async Task<ApiResponse> GetAsync(int id)
        {
            CourseLesson? CourseLesson = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (CourseLesson == null)
            {
                return new ApiResponse
                {
                    StatusCode = 404
                };
            }
            return new ApiResponse
            {
                StatusCode = 200,
                items = CourseLesson
            };
        }

        public async Task<ApiResponse> RemoveAsync(int id)
        {
            CourseLesson CourseLesson = await _repository.GetAsync(x => x.Id == id);
            if (CourseLesson is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            CourseLesson.IsDeleted = true;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = CourseLesson
            };
        }

        public async Task<ApiResponse> UpdateAsync(int id, CourseLessonUpdateDto dto)
        {
            CourseLesson CourseLesson = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (CourseLesson is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            if (dto.Video is not null)
            {
                CourseLesson.Video = dto.Video.CreateImage(_evn.WebRootPath, "Videos/CourseLessons");
                CourseLesson.VideoUrl = _http.HttpContext?.Request.Scheme + "://" + _http.HttpContext?.Request.Host
                    + $"Videos/CourseLessons/{CourseLesson.Video}";
            }
            CourseLesson.UpdatedAt = DateTime.UtcNow.AddHours(4);
            CourseLesson.Name = dto.Name;
            CourseLesson.CourseDuration = dto.CourseDuration;
            CourseLesson.Description = dto.Description;
            CourseLesson.LessonLine = dto.LessonLine;
            CourseLesson.CourseId = dto.CourseId;
            CourseLesson.CourseLessonLevelId = dto.CourseLessonLevelId;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = CourseLesson
            };
        }
    }
}
