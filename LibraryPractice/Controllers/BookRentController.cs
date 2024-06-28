using Microsoft.AspNetCore.Mvc;
using Logic.Services;
using SQLcon.Models;
using System.Net;
using System.Globalization;

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
            string result = _reportService.GetRentalsJson();
            if (result != "null")
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        //отчет по пользователям
        [HttpGet("{id}/books", Name = "GetUsersRent")]
        public IActionResult Get(int id)
        {
            string result = _reportService.GetRentalsByUserJson(id);
            if (result != "null")
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // вернуть запись об аренде по ID
        [HttpGet("{id}", Name = "GetRental")]
        public IActionResult GetRental(int id)
        {
            string result = _rentService.GetRental(id);
            if (result != "null")
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
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
            _rentService.AddRentObj(newBookRent);

            return CreatedAtAction(nameof(GetRental), new { id = newBookRent.IdBooksRent }, newBookRent);
        }

        // PUT возврат книги
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] string Date)
        {
            if (Date == null)
            {
                return BadRequest();
            }
            if(!DateOnly.TryParseExact(Date, "yyyy-MM-dd",null,DateTimeStyles.None,out DateOnly refundDate))
            {
                return BadRequest("Invalid date format");
            }
            if (!_rentService.CloseRent(id, refundDate))
            {
                return NotFound();
            }

            return Ok(_rentService.GetRental(id));            
        }
    }
}
