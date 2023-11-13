using Forage.Service.Dtos.CourseLessonActivities;
using Forage.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Service.Services.Interfaces
{
    public interface ICourseLessonActivityService
    {
        public Task<ApiResponse> CreateAsync(CourseLessonActivityPostDto dto);
        public Task<ApiResponse> GetAsync(int id);
        public Task<ApiResponse> GetAllAsync();
        public Task<ApiResponse> UpdateAsync(int id, CourseLessonActivityUpdateDto dto);
    }
}
