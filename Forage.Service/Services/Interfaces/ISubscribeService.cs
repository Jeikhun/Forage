using Forage.Service.Dtos.Subscribes;
using Forage.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Service.Services.Interfaces
{
    public interface ISubscribeService
    {
        public Task<ApiResponse> CreateAsync(SubscribePostDto dto);
        public Task<ApiResponse> GetAsync(int id);
        public Task<ApiResponse> GetAllAsync();
        public Task<ApiResponse> RemoveAsync(int id);
    }
}
