using Forage.Service.Dtos.CourseLevels;
using Forage.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Service.Services.Interfaces
{
    public interface ICourseLevelService
    {
        public Task<ApiResponse> CreateAsync(CourseLevelPostDto dto);
        public Task<ApiResponse> GetAsync(int id);
        public Task<ApiResponse> GetAllAsync();
        public Task<ApiResponse> UpdateAsync(int id, CourseLevelUpdateDto dto);
        public Task<ApiResponse> RemoveAsync(int id);
    }
}
