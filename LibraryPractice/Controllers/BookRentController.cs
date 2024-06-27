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
            return Ok(_reportService.GetRentalsJson());
        }

        //отчет по пользователям
        [HttpGet("{id}", Name = "GetUsersRent")]
        public IActionResult Get(int id)
        {
            return Ok(_reportService.GetRentalsByUserJson(id));
        }

        // вернуть запись об аренде по ID
        [HttpGet("rental/{id}", Name = "GetRental")]
        public IActionResult GetRental(int id)
        {
            return Ok(_rentService.GetRental(id));
        }

        // взятие в аренду
        [HttpPost]
        public IActionResult Create([FromBody] BooksRent newBookRent)
        {
            if (newBookRent == null)
            {
                return BadRequest();
            }
            _rentService.AddRentObj(newBookRent);
            //добавить возврат id добавленного пользователя VT

            return CreatedAtAction(nameof(GetRental), new { id = newBookRent.IdBooksRent }, newBookRent);
        }

        // PUT возврат книги
        [HttpPut("closerent/{id}")]
        public IActionResult Update(int id, [FromBody] DateOnly? refundDate)
        {
            if (refundDate == null)
            {
                return BadRequest();
            }
            _rentService.CloseRent(id, refundDate);
            //посмотреть правильный код ответа
            return NoContent();
            
        }
    }
}
