using AutoMapper;
using Forage.Core.Entities;
using Forage.Core.Repositories;
using Forage.Service.Dtos.HelpMessage;
using Forage.Service.Responses;
using Forage.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Service.Services.Implementations
{
    public class HelpMessageService : IHelpMessageService
    {
        private readonly IMapper _mapper;
        private readonly IHelpMessageRepository _repository;

        public HelpMessageService(IMapper mapper, IHelpMessageRepository repository )
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<ApiResponse> CreateAsync(HelpMessagePostDto dto)
        {
            HelpMessage HelpMessage = _mapper.Map<HelpMessage>(dto);
            await _repository.AddAsync(HelpMessage);
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 201,
                items = HelpMessage
            };
        }

        public async Task<ApiResponse> GetAsync(int id)
        {
            HelpMessage? HelpMessage = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted, "HelpMessageType");
            if (HelpMessage == null)
            {
                return new ApiResponse
                {
                    StatusCode = 404
                };
            }
            return new ApiResponse
            {
                StatusCode = 200,
                items = HelpMessage
            };
        }

        public async Task<ApiResponse> GetAllAsync()
        {
            IEnumerable<HelpMessage> HelpMessages = await _repository.GetAllAsync(x => !x.IsDeleted, "HelpMessageType");
            return new ApiResponse
            {
                items = HelpMessages,
                StatusCode = 200
            };
        }
        public async Task<ApiResponse> RemoveAsync(int id)
        {
            HelpMessage HelpMessage = await _repository.GetAsync(x => x.Id == id);
            if (HelpMessage is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            HelpMessage.IsDeleted = true;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = HelpMessage
            };
        }
    }
}
