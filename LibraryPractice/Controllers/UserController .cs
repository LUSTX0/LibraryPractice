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
            try
            {
                return Ok(_reportService.GetUsersJson());
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = ex.Message, details = ex.InnerException?.Message });
            }
        }
        // вернуть запись о пользователе по ID
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

        // создать пользователя
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
                //добавить возврат id добавленного пользователя

                return CreatedAtAction(nameof(Get), new { id = newUser.IdUsers }, newUser);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = ex.Message, details = ex.InnerException?.Message });
            }
        }

        // PUT изменить информацию пользователя
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
                //посмотреть правильный код ответа
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = ex.Message, details = ex.InnerException?.Message });
            }
        }

        // DELETE удалить пользователя
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
