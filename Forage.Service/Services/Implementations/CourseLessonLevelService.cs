using AutoMapper;
using Forage.Core.Entities;
using Forage.Core.Repositories;
using Forage.Service.Dtos.CourseLessonLevels;
using Forage.Service.Responses;
using Forage.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Service.Services.Implementations
{
    public class CourseLessonLevelService : ICourseLessonLevelService
    {
        private readonly ICourseLessonLevelRepository _repository;
        private readonly IMapper _mapper;

        public CourseLessonLevelService(ICourseLessonLevelRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ApiResponse> CreateAsync(CourseLessonLevelPostDto dto)
        {
            if (await _repository.isExsist(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower()))
            {
                return new ApiResponse
                {
                    StatusCode = 400,
                    Description = $"{dto.Name} Already exists"
                };
            }
            CourseLessonLevel CourseLessonLevel = _mapper.Map<CourseLessonLevel>(dto);
            await _repository.AddAsync(CourseLessonLevel);
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 201,
                items = CourseLessonLevel
            };
        }

        public async Task<ApiResponse> GetAllAsync()
        {
            IEnumerable<CourseLessonLevel> CourseLessonLevels = await _repository.GetAllAsync(x => !x.IsDeleted);
            return new ApiResponse
            {
                items = CourseLessonLevels,
                StatusCode = 200
            };
        }

        public async Task<ApiResponse> GetAsync(int id)
        {
            CourseLessonLevel? CourseLessonLevel = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (CourseLessonLevel == null)
            {
                return new ApiResponse
                {
                    StatusCode = 404
                };
            }
            return new ApiResponse
            {
                StatusCode = 200,
                items = CourseLessonLevel
            };
        }

        public async Task<ApiResponse> RemoveAsync(int id)
        {
            CourseLessonLevel CourseLessonLevel = await _repository.GetAsync(x => x.Id == id);
            if (CourseLessonLevel is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            CourseLessonLevel.IsDeleted = true;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = CourseLessonLevel
            };
        }

        public async Task<ApiResponse> UpdateAsync(int id, CourseLessonLevelUpdateDto dto)
        {
            CourseLessonLevel CourseLessonLevel = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (CourseLessonLevel is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            if (CourseLessonLevel.Name.ToLower() != dto.Name.ToLower())
            {
                if (await _repository.isExsist(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower()))
                {
                    return new ApiResponse
                    {
                        StatusCode = 400,
                        Description = $"{dto.Name} Already exists"
                    };
                }
            }
           
            CourseLessonLevel.UpdatedAt = DateTime.UtcNow.AddHours(4);
            CourseLessonLevel.Name = dto.Name;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = CourseLessonLevel
            };
        }
    }
}
