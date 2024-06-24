using Microsoft.AspNetCore.Mvc;
using Logic.Services;
using SQLcon.Models;
using System.Net;

namespace LIBRARY2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService  _userService;
        private readonly IReportService _reportService;
        public UserController(IUserService userService, IReportService reportService)
        {
            _userService = userService;
            _reportService = reportService;
        }

        // ����� �� �������������
        [HttpGet(Name = "GetUsers")]
        public IActionResult Get()
        {            
            try
            {
                return Ok(_reportService.GetUsersJson());
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = ex.Message, details = ex.InnerException?.Message });
            }
        }
        // ������� ������ � ������������ �� ID
        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult Get(int id)
        {            
            try
            {
                return Ok(_userService.GetUser(id));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = ex.Message, details = ex.InnerException?.Message });
            }
        }

        // ������� ������������
        [HttpPost]
        public IActionResult Create([FromBody] User newUser)
        {
            if (newUser == null)
            {
                return BadRequest();
            }
            
            try
            {
                _userService.AddUserObj(newUser);
                //�������� ������� id ������������ ������������

                return CreatedAtAction(nameof(Get), new { id = newUser.IdUsers }, newUser);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = ex.Message, details = ex.InnerException?.Message });
            }
        }

        // PUT �������� ���������� ������������
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] User updatedUser)
        {
            if (updatedUser == null )
            {
                return BadRequest();
            }
            
            try
            {
                _userService.UpdateUserObj(id, updatedUser);
                //���������� ���������� ��� ������
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = ex.Message, details = ex.InnerException?.Message });
            }
        }

        // DELETE ������� ������������
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {           
            try
            {
                _userService.DeleteUser(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = ex.Message, details = ex.InnerException?.Message });
            }
        }
    }
}
