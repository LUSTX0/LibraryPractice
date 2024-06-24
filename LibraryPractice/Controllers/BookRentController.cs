using Microsoft.AspNetCore.Mvc;
using Logic.Services;
using SQLcon.Models;
using System.Net;

namespace LIBRARY2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookRentController : ControllerBase
    {
        private readonly IRentService _rentService;
        private readonly IReportService _reportService;
        public BookRentController(IRentService rentService, IReportService reportService)
        {
            _rentService = rentService;
            _reportService = reportService;
        }

        //общий отчет
        [HttpGet(Name = "GetRentals")]
        public IActionResult Get()
        {            
            try
            {
                return Ok(_reportService.GetRentalsJson());
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = ex.Message, details = ex.InnerException?.Message });
            }
        }

        //отчет по пользователям
        [HttpGet("{id}", Name = "GetUsersRent")]
        public IActionResult Get(int id)
        {           
            try
            {
                return Ok(_reportService.GetRentalsByUserJson(id));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = ex.Message, details = ex.InnerException?.Message });
            }
        }

        // вернуть запись об аренде по ID
        [HttpGet("rental/{id}", Name = "GetRental")]
        public IActionResult GetRental(int id)
        {            
            try
            {
                return Ok(_rentService.GetRental(id));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = ex.Message, details = ex.InnerException?.Message });
            }
        }

        // взятие в аренду
        [HttpPost]
        public IActionResult Create([FromBody] BooksRent newBookRent)
        {
            if (newBookRent == null)
            {
                return BadRequest();
            }
            
            try
            {
                _rentService.AddRentObj(newBookRent);
                //добавить возврат id добавленного пользователя VT

                return CreatedAtAction(nameof(GetRental), new { id = newBookRent.IdBooksRent }, newBookRent);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = ex.Message, details = ex.InnerException?.Message });
            }
        }

        // PUT возврат книги
        [HttpPut("closerent/{id}")]
        public IActionResult Update(int id, [FromBody] DateOnly? refundDate)
        {
            if (refundDate == null)
            {
                return BadRequest();
            }
            
            try
            {
                _rentService.CloseRent(id, refundDate);
                //посмотреть правильный код ответа
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = ex.Message, details = ex.InnerException?.Message });
            }
        }
    }
}
