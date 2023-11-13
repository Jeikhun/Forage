using Forage.Service.Dtos.CourseCategories;
using Forage.Service.Dtos.HelpMessageType;
using Forage.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Service.Services.Interfaces
{
    public interface ICourseCategoryService
    {
        public Task<ApiResponse> CreateAsync(CourseCategoryPostDto dto);
        public Task<ApiResponse> GetAsync(int id);
        public Task<ApiResponse> GetAllAsync();
        public Task<ApiResponse> UpdateAsync(int id, CourseCategoryUpdateDto dto);
        public Task<ApiResponse> RemoveAsync(int id);
    }
}
