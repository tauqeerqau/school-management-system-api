using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.DTOs;
using SchoolManagement.Application.Interfaces;

namespace SchoolManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents([FromQuery] StudentQueryParameters parameters)
        {
            var response = await _studentService.GetAllStudentsAsync(parameters);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var response = await _studentService.GetStudentByIdAsync(id);

            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent(
            [FromBody] CreateStudentDto dto)
        {
            var response =
                await _studentService.CreateStudentAsync(dto);

            return CreatedAtAction(
                nameof(GetStudentById),
                new { id = response.Data?.Id },
                response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(
            int id,
            [FromBody] UpdateStudentDto dto)
        {
            var response =
                await _studentService.UpdateStudentAsync(id, dto);

            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var response =
                await _studentService.DeleteStudentAsync(id);

            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }
    }
}