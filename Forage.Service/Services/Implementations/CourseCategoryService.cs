using AutoMapper;
using Forage.Core.Entities;
using Forage.Core.Repositories;
using Forage.Service.Dtos.CourseCategories;
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
    public class CourseCategoryService : ICourseCategoryService
    {
        private readonly ICourseCategoryRepository _repository;
        private readonly IMapper _mapper;

        public CourseCategoryService(ICourseCategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ApiResponse> CreateAsync(CourseCategoryPostDto dto)
        {
            if (await _repository.isExsist(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower()))
            {
                return new ApiResponse
                {
                    StatusCode = 400,
                    Description = $"{dto.Name} Already exists"
                };
            }
            CourseCategory CourseCategory = _mapper.Map<CourseCategory>(dto);
            await _repository.AddAsync(CourseCategory);
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 201,
                items = CourseCategory
            };
        }

        public async Task<ApiResponse> GetAllAsync()
        {
            IEnumerable<CourseCategory> CourseCategorys = await _repository.GetAllAsync(x => !x.IsDeleted);
            return new ApiResponse
            {
                items = CourseCategorys,
                StatusCode = 200
            };
        }

        public async Task<ApiResponse> GetAsync(int id)
        {
            CourseCategory? CourseCategory = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (CourseCategory == null)
            {
                return new ApiResponse
                {
                    StatusCode = 404
                };
            }
            return new ApiResponse
            {
                StatusCode = 200,
                items = CourseCategory
            };
        }

        public async Task<ApiResponse> RemoveAsync(int id)
        {
            CourseCategory CourseCategory = await _repository.GetAsync(x => x.Id == id);
            if (CourseCategory is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            CourseCategory.IsDeleted = true;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = CourseCategory
            };
        }

        public async Task<ApiResponse> UpdateAsync(int id, CourseCategoryUpdateDto dto)
        {
            CourseCategory CourseCategory = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (CourseCategory is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            if (CourseCategory.Name.ToLower() != dto.Name.ToLower())
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
           
            CourseCategory.UpdatedAt = DateTime.UtcNow.AddHours(4);
            CourseCategory.Name = dto.Name;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = CourseCategory
            };
        }
    }
}
