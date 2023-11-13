using Forage.Service.Dtos.Interns;
using Forage.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Service.Services.Interfaces
{
    public interface IInternService
    {
        public Task<ApiResponse> CreateAsync(InternPostDto dto);
        public Task<ApiResponse> GetAsync(int id);
        public Task<ApiResponse> GetAllAsync();
        public Task<ApiResponse> UpdateAsync(int id, InternUpdateDto dto);
        public Task<ApiResponse> RemoveAsync(int id);
    }
}
