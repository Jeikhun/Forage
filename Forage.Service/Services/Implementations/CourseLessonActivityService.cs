using AutoMapper;
using Forage.Core.Entities;
using Forage.Core.Repositories;
using Forage.Service.Dtos.CourseLessonActivities;
using Forage.Service.Exceptions;
using Forage.Service.Responses;
using Forage.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Service.Services.Implementations
{
    public class CourseLessonActivityService : ICourseLessonActivityService
    {
        private readonly ICourseLessonActivityRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public CourseLessonActivityService(ICourseLessonActivityRepository repository, IMapper mapper, IHttpContextAccessor httpContextAccessor = null, UserManager<AppUser> userManager = null)
        {
            _repository = repository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        public async Task<ApiResponse> CreateAsync(CourseLessonActivityPostDto dto)
        {
            string userId = await GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null) { throw new NotFoundException(nameof(user)); }

            if (await _repository.isExsist(c => c.AppUserId == userId && c.CourseLessonId == dto.CourseLessonId))
            {
                return new ApiResponse
                {
                    StatusCode = 500,
                    Description = "Already Exist"
                };
            }
            CourseLessonActivity CourseLessonActivity = _mapper.Map<CourseLessonActivity>(dto);
            CourseLessonActivity.AppUserId = userId;
            await _repository.AddAsync(CourseLessonActivity);
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 201,
                items = CourseLessonActivity
            };
        }

        public async Task<ApiResponse> GetAllAsync()
        {
            IEnumerable<CourseLessonActivity> CourseLessonActivitys = await _repository.GetAllAsync(x => !x.IsDeleted);
            return new ApiResponse
            {
                items = CourseLessonActivitys,
                StatusCode = 200
            };
        }

        public async Task<ApiResponse> GetAsync(int id)
        {
            CourseLessonActivity? CourseLessonActivity = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (CourseLessonActivity == null)
            {
                return new ApiResponse
                {
                    StatusCode = 404
                };
            }
            return new ApiResponse
            {
                StatusCode = 200,
                items = CourseLessonActivity
            };
        }
        public async Task<ApiResponse> UpdateAsync(int id, CourseLessonActivityUpdateDto dto)
        {
            string userId = await GetUserId();
            CourseLessonActivity CourseLessonActivity = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (CourseLessonActivity is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "User Not found"
                };
            }
            CourseLessonActivity.UpdatedAt = DateTime.UtcNow.AddHours(4);
            CourseLessonActivity.IsFinished = dto.IsFinished;
            CourseLessonActivity.Percent = dto.Percent;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = CourseLessonActivity
            };
        }
        public async Task<string> GetUserId()
        {
            string? username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
            AppUser? user = await _userManager.FindByNameAsync(username);
            return user.Id is not null ? user.Id : throw new NotFoundException(nameof(user));
        }
    }
}
