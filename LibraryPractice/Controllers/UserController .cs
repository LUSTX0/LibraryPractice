using Microsoft.AspNetCore.Mvc;
using Logic.Services;


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

        

        [HttpGet(Name = "GetUsers")]
        public IActionResult Get()
        {
            return Ok( _reportService.GetUsersJson());
        }

        


        [HttpPost]
        public IActionResult Create([FromBody] string newUser)
        {
            if (newUser == null)
            {
                return BadRequest();
            }
            _userService.AddUserObj(newUser);
            //добавить возврат id добавленного пользователя
            
            return CreatedAtAction(nameof(Get), new { id = "" }, newUser);
        }

        // PUT api/items/1
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] string updatedUser)
        {
            if (updatedUser == null )
            {
                return BadRequest();
            }
            _userService.UpdateUserObj(id, updatedUser);
            //посмотреть правильный код ответа
            return NoContent();
        }

        // DELETE api/items/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.DeleteUser(id);
            
            return NoContent();
        }
    }
}
