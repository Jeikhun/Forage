using Forage.Service.Dtos.Companies;
using Forage.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Service.Services.Interfaces
{
    public interface ICompanyService
    {
        public Task<ApiResponse> CreateAsync(CompanyPostDto dto);
        public Task<ApiResponse> GetAsync(int id);
        public Task<ApiResponse> GetAllAsync();
        public Task<ApiResponse> UpdateAsync(int id, CompanyUpdateDto dto);
        public Task<ApiResponse> RemoveAsync(int id);
    }
}
