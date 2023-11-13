using AutoMapper;
using Forage.Core.Entities;
using Forage.Core.Repositories;
using Forage.Service.Dtos.CompanyIndustryFields;
using Forage.Service.Responses;
using Forage.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Service.Services.Implementations
{
    public class CompanyIndustryFieldService : ICompanyIndustryFieldService
    {
        private readonly ICompanyIndustryFieldRepository _repository;
        private readonly IMapper _mapper;

        public CompanyIndustryFieldService(IMapper mapper, ICompanyIndustryFieldRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ApiResponse> CreateAsync(CompanyIndustryFieldPostDto dto)
        {
            if (await _repository.isExsist(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower()))
            {
                return new ApiResponse
                {
                    StatusCode = 400,
                    Description = $"{dto.Name} Already exists"
                };
            }
            CompanyIndustryField companyIndustryField = _mapper.Map<CompanyIndustryField>(dto);
            await _repository.AddAsync(companyIndustryField);
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 201,
                items = companyIndustryField
            };
        }

        public async Task<ApiResponse> GetAllAsync()
        {
            IEnumerable<CompanyIndustryField> companyIndustryField = await _repository.GetAllAsync(x => !x.IsDeleted);
            return new ApiResponse
            {
                items = companyIndustryField,
                StatusCode = 200
            };
        }

        public async Task<ApiResponse> GetAsync(int id)
        {
            CompanyIndustryField? companyIndustryField = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (companyIndustryField == null)
            {
                return new ApiResponse
                {
                    StatusCode = 404
                };
            }
            return new ApiResponse
            {
                StatusCode = 200,
                items = companyIndustryField
            };
        }

        public async Task<ApiResponse> RemoveAsync(int id)
        {
            CompanyIndustryField companyIndustryField = await _repository.GetAsync(x => x.Id == id);
            if (companyIndustryField is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            companyIndustryField.IsDeleted = true;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = companyIndustryField
            };
        }

        public async Task<ApiResponse> UpdateAsync(int id, CompanyIndustryFieldUpdateDto dto)
        {
            CompanyIndustryField CompanyIndustryField = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (CompanyIndustryField is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            if (CompanyIndustryField.Name.ToLower() != dto.Name.ToLower())
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

            CompanyIndustryField.UpdatedAt = DateTime.UtcNow.AddHours(4);
            CompanyIndustryField.Name = dto.Name;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = CompanyIndustryField
            };
        }
    }
}
