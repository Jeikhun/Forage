using Forage.Service.Dtos.HelpMessageType;
using Forage.Service.Dtos.Questions;
using Forage.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Service.Services.Interfaces
{
    public interface IHelpMessageTypeService
    {
        public Task<ApiResponse> CreateAsync(HelpMessageTypePostDto dto);
        public Task<ApiResponse> GetAsync(int id);
        public Task<ApiResponse> GetAllAsync();
        public Task<ApiResponse> UpdateAsync(int id, HelpMessageTypeUpdateDto dto);
        public Task<ApiResponse> RemoveAsync(int id);
    }
}
