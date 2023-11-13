using AutoMapper;
using Forage.Core.Entities;
using Forage.Core.Repositories;
using Forage.Service.Dtos.HelpMessageType;
using Forage.Service.Responses;
using Forage.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Service.Services.Implementations
{
    public class HelpMessageTypeService : IHelpMessageTypeService
    {
        private readonly IMapper _mapper;
        private readonly IHelpMessageTypeRepository _repository;

        public HelpMessageTypeService(IMapper mapper, IHelpMessageTypeRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<ApiResponse> CreateAsync(HelpMessageTypePostDto dto)
        {
            if (await _repository.isExsist(x => x.Title.Trim().ToLower() == dto.Title.Trim().ToLower()))
            {
                return new ApiResponse
                {
                    StatusCode = 400,
                    Description = $"{dto.Title} Already exists"
                };
            }
            HelpMessageType HelpMessageType = _mapper.Map<HelpMessageType>(dto);
            await _repository.AddAsync(HelpMessageType);
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 201,
                items = HelpMessageType
            };
        }

        public async Task<ApiResponse> GetAsync(int id)
        {
            HelpMessageType? HelpMessageType = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (HelpMessageType == null)
            {
                return new ApiResponse
                {
                    StatusCode = 404
                };
            }
            return new ApiResponse
            {
                StatusCode = 200,
                items = HelpMessageType
            };
        }

        public async Task<ApiResponse> GetAllAsync()
        {
            IEnumerable<HelpMessageType> HelpMessageTypes = await _repository.GetAllAsync(x => !x.IsDeleted);
            return new ApiResponse
            {
                items = HelpMessageTypes,
                StatusCode = 200
            };
        }

        public async Task<ApiResponse> UpdateAsync(int id, HelpMessageTypeUpdateDto dto)
        {
            HelpMessageType HelpMessageType = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (HelpMessageType is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            if (HelpMessageType.Title.ToLower() != dto.Title.ToLower())
            {
                if (await _repository.isExsist(x => x.Title.Trim().ToLower() == dto.Title.Trim().ToLower()))
                {
                    return new ApiResponse
                    {
                        StatusCode = 400,
                        Description = $"{dto.Title} Already exists"
                    };
                }
            }

            HelpMessageType.UpdatedAt = DateTime.UtcNow.AddHours(4);
            HelpMessageType.Title = dto.Title;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = HelpMessageType
            };
        }

        public async Task<ApiResponse> RemoveAsync(int id)
        {
            HelpMessageType HelpMessageType = await _repository.GetAsync(x => x.Id == id);
            if (HelpMessageType is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            HelpMessageType.IsDeleted = true;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = HelpMessageType
            };
        }
    }
}
