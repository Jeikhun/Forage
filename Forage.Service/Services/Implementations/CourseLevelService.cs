using AutoMapper;
using Forage.Core.Entities;
using Forage.Core.Repositories;
using Forage.Service.Dtos.CourseLevels;
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
    public class CourseLevelService : ICourseLevelService
    {
        private readonly ICourseLevelRepository _repository;
        private readonly IMapper _mapper;

        public CourseLevelService(ICourseLevelRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ApiResponse> CreateAsync(CourseLevelPostDto dto)
        {
            if (await _repository.isExsist(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower()))
            {
                return new ApiResponse
                {
                    StatusCode = 400,
                    Description = $"{dto.Name} Already exists"
                };
            }
            CourseLevel CourseLevel = _mapper.Map<CourseLevel>(dto);
            await _repository.AddAsync(CourseLevel);
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 201,
                items = CourseLevel
            };
        }

        public async Task<ApiResponse> GetAllAsync()
        {
            IEnumerable<CourseLevel> CourseLevels = await _repository.GetAllAsync(x => !x.IsDeleted);
            return new ApiResponse
            {
                items = CourseLevels,
                StatusCode = 200
            };
        }

        public async Task<ApiResponse> GetAsync(int id)
        {
            CourseLevel? CourseLevel = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (CourseLevel == null)
            {
                return new ApiResponse
                {
                    StatusCode = 404
                };
            }
            return new ApiResponse
            {
                StatusCode = 200,
                items = CourseLevel
            };
        }

        public async Task<ApiResponse> RemoveAsync(int id)
        {
            CourseLevel CourseLevel = await _repository.GetAsync(x => x.Id == id);
            if (CourseLevel is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            CourseLevel.IsDeleted = true;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = CourseLevel
            };
        }

        public async Task<ApiResponse> UpdateAsync(int id, CourseLevelUpdateDto dto)
        {
            CourseLevel CourseLevel = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (CourseLevel is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            if (CourseLevel.Name.ToLower() != dto.Name.ToLower())
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
           
            CourseLevel.UpdatedAt = DateTime.UtcNow.AddHours(4);
            CourseLevel.Name = dto.Name;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = CourseLevel
            };
        }
    }
}
