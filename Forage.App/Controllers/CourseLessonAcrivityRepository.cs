using Forage.Service.Dtos.CourseLessonActivities;
using Forage.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Forage.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseLessonActivityController : ControllerBase
    {
        private readonly ICourseLessonActivityService _service;

        public CourseLessonActivityController(ICourseLessonActivityService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetAsync(id);
            return StatusCode(result.StatusCode, result);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CourseLessonActivityPostDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var result = await _service.RemoveAsync(id);
        //    return StatusCode(result.StatusCode);
        //}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CourseLessonActivityUpdateDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            return StatusCode(result.StatusCode, result);
        }
    }
}
