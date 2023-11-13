using Forage.Service.Dtos.Courses;
using Forage.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Service.Services.Interfaces
{
    public interface ICourseService
    {
        public Task<ApiResponse> CreateAsync(CoursePostDto dto);
        public Task<ApiResponse> GetAsync(int id);
        public Task<ApiResponse> GetAllAsync();
        public Task<ApiResponse> UpdateAsync(int id, CourseUpdateDto dto);
        public Task<ApiResponse> RemoveAsync(int id);
    }
}
