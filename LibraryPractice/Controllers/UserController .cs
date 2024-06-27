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
            return Ok(_reportService.GetUsersJson());            
        }

        // ������� ������ � ������������ �� ID
        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult Get(int id)
        {
            return Ok(_userService.GetUser(id));            
        }

        // ������� ������������
        [HttpPost]
        public IActionResult Create([FromBody] User newUser)
        {
            if (newUser == null)
            {
                return BadRequest();
            }

            _userService.AddUserObj(newUser);
            //�������� ������� id ������������ ������������
            return CreatedAtAction(nameof(Get), new { id = newUser.IdUsers }, newUser);
            
        }

        // PUT �������� ���������� ������������
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] User updatedUser)
        {
            if (updatedUser == null )
            {
                return BadRequest();
            }

            _userService.UpdateUserObj(id, updatedUser);
            //���������� ���������� ��� ������
            return NoContent();
            
        }

        // DELETE ������� ������������
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.DeleteUser(id);
            return NoContent();
            
        }
    }
}
