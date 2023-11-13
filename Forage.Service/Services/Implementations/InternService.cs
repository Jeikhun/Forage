using AutoMapper;
using Forage.Core.Entities;
using Forage.Core.Repositories;
using Forage.Service.Dtos.Interns;
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
    public class InternService : IInternService
    {
        private readonly IInternRepository _repository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _evn;
        private readonly IHttpContextAccessor _http;

        public InternService(IInternRepository repository, IMapper mapper, IWebHostEnvironment evn, IHttpContextAccessor http)
        {
            _repository = repository;
            _mapper = mapper;
            _evn = evn;
            _http = http;
        }
        public async Task<ApiResponse> CreateAsync(InternPostDto dto)
        {
            if (await _repository.isExsist(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower()))
            {
                return new ApiResponse
                {
                    StatusCode = 400,
                    Description = $"{dto.Name} Already exists"
                };
            }
            Intern Intern = _mapper.Map<Intern>(dto);
            Intern.Image = dto.Image.CreateImage(_evn.WebRootPath, "Images/Interns");
            Intern.ImageUrl = _http.HttpContext?.Request.Scheme + "://" + _http.HttpContext?.Request.Host
                + $"Images/Interns/{Intern.Image}";
            await _repository.AddAsync(Intern);
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 201,
                items = Intern
            };
        }

        public async Task<ApiResponse> GetAllAsync()
        {
            IEnumerable<Intern> Companies = await _repository.GetAllAsync(x => !x.IsDeleted);
            return new ApiResponse
            {
                items = Companies,
                StatusCode = 200
            };
        }

        public async Task<ApiResponse> GetAsync(int id)
        {
            Intern? Intern = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (Intern == null)
            {
                return new ApiResponse
                {
                    StatusCode = 404
                };
            }
            return new ApiResponse
            {
                StatusCode = 200,
                items = Intern
            };
        }

        public async Task<ApiResponse> RemoveAsync(int id)
        {
            Intern Intern = await _repository.GetAsync(x => x.Id == id);
            if (Intern is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            Intern.IsDeleted = true;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = Intern
            };
        }

        public async Task<ApiResponse> UpdateAsync(int id, InternUpdateDto dto)
        {
            Intern Intern = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (Intern is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            if (Intern.Name.ToLower() != dto.Name.ToLower())
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
            if (dto.Image is not null)
            {
                Intern.Image = dto.Image.CreateImage(_evn.WebRootPath, "Images/Interns");
                Intern.ImageUrl = _http.HttpContext?.Request.Scheme + "://" + _http.HttpContext?.Request.Host
                    + $"Images/Interns/{Intern.Image}";
            }
            Intern.UpdatedAt = DateTime.UtcNow.AddHours(4);
            Intern.Name = dto.Name;
            Intern.Surname = dto.Surname;
            Intern.BirthYear = dto.BirthYear;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = Intern
            };
        }
    }
}
