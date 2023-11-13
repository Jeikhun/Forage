using Forage.Service.Dtos.Companies;
using Forage.Service.Dtos.CompanyIndustryFields;
using Forage.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Service.Services.Interfaces
{
    public interface ICompanyIndustryFieldService
    {
        public Task<ApiResponse> CreateAsync(CompanyIndustryFieldPostDto dto);
        public Task<ApiResponse> GetAsync(int id);
        public Task<ApiResponse> GetAllAsync();
        public Task<ApiResponse> UpdateAsync(int id, CompanyIndustryFieldUpdateDto dto);
        public Task<ApiResponse> RemoveAsync(int id);
    }
}
