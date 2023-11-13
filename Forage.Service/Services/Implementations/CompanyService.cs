using AutoMapper;
using Forage.Core.Entities;
using Forage.Core.Repositories;
using Forage.Service.Dtos.Companies;
using Forage.Service.Extensions;
using Forage.Service.Responses;
using Forage.Service.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Forage.Service.Services.Implementations
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _repository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _evn;
        private readonly IHttpContextAccessor _http;

        public CompanyService(ICompanyRepository repository, IMapper mapper, IWebHostEnvironment evn, IHttpContextAccessor http)
        {
            _repository = repository;
            _mapper = mapper;
            _evn = evn;
            _http = http;
        }
        public async Task<ApiResponse> CreateAsync(CompanyPostDto dto)
        {
            if (await _repository.isExsist(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower()))
            {
                return new ApiResponse
                {
                    StatusCode = 400,
                    Description = $"{dto.Name} Already exists"
                };
            }
            Company Company = _mapper.Map<Company>(dto);
            Company.Logo = dto.file.CreateImage(_evn.WebRootPath, "Images/Companies");
            Company.LogoUrl = _http.HttpContext?.Request.Scheme + "://" + _http.HttpContext?.Request.Host
                + $"Images/Companies/{Company.Logo}";
            await _repository.AddAsync(Company);
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 201,
                items = Company
            };
        }

        public async Task<ApiResponse> GetAllAsync()
        {
            IEnumerable<Company> Companies = await _repository.GetAllAsync(x => !x.IsDeleted);
            return new ApiResponse
            {
                items = Companies,
                StatusCode = 200
            };
        }

        public async Task<ApiResponse> GetAsync(int id)
        {
            Company? Company = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (Company == null)
            {
                return new ApiResponse
                {
                    StatusCode = 404
                };
            }
            return new ApiResponse
            {
                StatusCode = 200,
                items = Company
            };
        }

        public async Task<ApiResponse> RemoveAsync(int id)
        {
            Company Company = await _repository.GetAsync(x => x.Id == id);
            if (Company is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            Company.IsDeleted = true;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = Company
            };
        }

        public async Task<ApiResponse> UpdateAsync(int id, CompanyUpdateDto dto)
        {
            Company Company = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (Company is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            if (Company.Name.ToLower() != dto.Name.ToLower())
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
            if (dto.file is not null)
            {
                Company.Logo = dto.file.CreateImage(_evn.WebRootPath, "Images/Companies");
                Company.LogoUrl = _http.HttpContext?.Request.Scheme + "://" + _http.HttpContext?.Request.Host
                    + $"Images/Companies/{Company.Logo}";
            }
            Company.UpdatedAt = DateTime.UtcNow.AddHours(4);
            Company.Name = dto.Name;
            Company.Address = dto.Address;
            Company.PhoneNumber = dto.PhoneNumber;
            Company.Voen = dto.Voen;
            Company.CompanyIndustryFieldId = dto.CompanyIndustryFieldId;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = Company
            };
        }
    }
}
