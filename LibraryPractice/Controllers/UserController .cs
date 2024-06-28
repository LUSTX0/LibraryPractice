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

        // отчет по пользователям
        [HttpGet(Name = "GetUsers")]
        public IActionResult Get()
        {
            string result = _reportService.GetUsersJson();
            if (result != "null")
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }        
        }

        // вернуть запись о пользователе по ID
        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult Get(int id)
        {
            string result = _userService.GetUser(id);
            if (result != "null")
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }      
        }

        // создать пользователя
        [HttpPost]
        public IActionResult Create([FromBody] User newUser)
        {
            if (newUser == null)
            {
                return BadRequest();
            }

            _userService.AddUserObj(newUser);
            return CreatedAtAction(nameof(Get), new { id = newUser.IdUsers }, newUser);            
        }

        // PUT изменить информацию пользователя
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] User updatedUser)
        {
            if (updatedUser == null )
            {
                return BadRequest();
            }
            if (!_userService.UpdateUserObj(id, updatedUser))
            {
                return NotFound();
            }

            return Ok(_userService.GetUser(id));            
        }

        // DELETE удалить пользователя
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_userService.DeleteUser(id))
            {
                return NotFound();
            }

            return NoContent();            
        }
    }
}
