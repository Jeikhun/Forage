using AutoMapper;
using Forage.Core.Entities;
using Forage.Core.Repositories;
using Forage.Service.Dtos.Questions;
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
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _repository;
        private readonly IMapper _mapper;

        public QuestionService(IQuestionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ApiResponse> CreateAsync(QuestionPostDto dto)
        {
            if (await _repository.isExsist(x => x.Title.Trim().ToLower() == dto.Title.Trim().ToLower()))
            {
                return new ApiResponse
                {
                    StatusCode = 400,
                    Description = $"{dto.Title} Already exists"
                };
            }
            Question Question = _mapper.Map<Question>(dto);
            await _repository.AddAsync(Question);
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 201,
                items = Question
            };
        }

        public async Task<ApiResponse> GetAllAsync()
        {
            IEnumerable<Question> Questions = await _repository.GetAllAsync(x => !x.IsDeleted);
            return new ApiResponse
            {
                items = Questions,
                StatusCode = 200
            };
        }

        public async Task<ApiResponse> GetAsync(int id)
        {
            Question? Question = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (Question == null)
            {
                return new ApiResponse
                {
                    StatusCode = 404
                };
            }
            return new ApiResponse
            {
                StatusCode = 200,
                items = Question
            };
        }

        public async Task<ApiResponse> RemoveAsync(int id)
        {
            Question Question = await _repository.GetAsync(x => x.Id == id);
            if (Question is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            Question.IsDeleted = true;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = Question
            };
        }

        public async Task<ApiResponse> UpdateAsync(int id, QuestionUpdateDto dto)
        {
            Question Question = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (Question is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            if (Question.Title.ToLower() != dto.Title.ToLower())
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
           
            Question.UpdatedAt = DateTime.UtcNow.AddHours(4);
            Question.Title = dto.Title;
            Question.Text = dto.Text;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = Question
            };
        }
    }
}
