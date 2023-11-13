using Forage.Service.Dtos.Skills;
using Forage.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Service.Services.Interfaces
{
    public interface ISkillService
    {
        public Task<ApiResponse> CreateAsync(SkillPostDto dto);
        public Task<ApiResponse> GetAsync(int id);
        public Task<ApiResponse> GetAllAsync();
        public Task<ApiResponse> UpdateAsync(int id, SkillUpdateDto dto);
        public Task<ApiResponse> RemoveAsync(int id);
    }
}
