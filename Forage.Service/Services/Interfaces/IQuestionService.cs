using Forage.Service.Dtos.Questions;
using Forage.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Service.Services.Interfaces
{
    public interface IQuestionService
    {
        public Task<ApiResponse> CreateAsync(QuestionPostDto dto);
        public Task<ApiResponse> GetAsync(int id);
        public Task<ApiResponse> GetAllAsync();
        public Task<ApiResponse> UpdateAsync(int id, QuestionUpdateDto dto);
        public Task<ApiResponse> RemoveAsync(int id);
    }
}
