using Forage.Service.Dtos.CourseLessonLevels;
using Forage.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Service.Services.Interfaces
{
    public interface ICourseLessonLevelService
    {
        public Task<ApiResponse> CreateAsync(CourseLessonLevelPostDto dto);
        public Task<ApiResponse> GetAsync(int id);
        public Task<ApiResponse> GetAllAsync();
        public Task<ApiResponse> UpdateAsync(int id, CourseLessonLevelUpdateDto dto);
        public Task<ApiResponse> RemoveAsync(int id);
    }
}
