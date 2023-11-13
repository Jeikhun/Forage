using AutoMapper;
using Forage.Core.Entities;
using Forage.Core.Repositories;
using Forage.Service.Dtos.Skills;
using Forage.Service.Responses;
using Forage.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Service.Services.Implementations
{
    public class SkillService : ISkillService
    {
        private readonly ISkillRepository _repository;
        private readonly IMapper _mapper;

        public SkillService(ISkillRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ApiResponse> CreateAsync(SkillPostDto dto)
        {
            if (await _repository.isExsist(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower()))
            {
                return new ApiResponse
                {
                    StatusCode = 400,
                    Description = $"{dto.Name} Already exists"
                };
            }
            Skill Skill = _mapper.Map<Skill>(dto);
            await _repository.AddAsync(Skill);
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 201,
                items = Skill
            };
        }

        public async Task<ApiResponse> GetAllAsync()
        {
            IEnumerable<Skill> Skills = await _repository.GetAllAsync(x => !x.IsDeleted);
            return new ApiResponse
            {
                items = Skills,
                StatusCode = 200
            };
        }

        public async Task<ApiResponse> GetAsync(int id)
        {
            Skill? Skill = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (Skill == null)
            {
                return new ApiResponse
                {
                    StatusCode = 404
                };
            }
            return new ApiResponse
            {
                StatusCode = 200,
                items = Skill
            };
        }

        public async Task<ApiResponse> RemoveAsync(int id)
        {
            Skill Skill = await _repository.GetAsync(x => x.Id == id);
            if (Skill is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            Skill.IsDeleted = true;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = Skill
            };
        }

        public async Task<ApiResponse> UpdateAsync(int id, SkillUpdateDto dto)
        {
            Skill Skill = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (Skill is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            if (Skill.Name.ToLower() != dto.Name.ToLower())
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
           
            Skill.UpdatedAt = DateTime.UtcNow.AddHours(4);
            Skill.Name = dto.Name;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = Skill
            };
        }
    }
}
