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

        //����� �����
        [HttpGet(Name = "GetRentals")]
        public IActionResult Get()
        {
            return Ok(_reportService.GetRentalsJson());
        }

        //����� �� �������������
        [HttpGet("{id}", Name = "GetUsersRent")]
        public IActionResult Get(int id)
        {
            return Ok(_reportService.GetRentalsByUserJson(id));
        }

        // ������� ������ �� ������ �� ID
        [HttpGet("rental/{id}", Name = "GetRental")]
        public IActionResult GetRental(int id)
        {
            return Ok(_rentService.GetRental(id));
        }

        // ������ � ������
        [HttpPost]
        public IActionResult Create([FromBody] BooksRent newBookRent)
        {
            if (newBookRent == null)
            {
                return BadRequest();
            }
            _rentService.AddRentObj(newBookRent);
            //�������� ������� id ������������ ������������ VT

            return CreatedAtAction(nameof(GetRental), new { id = newBookRent.IdBooksRent }, newBookRent);
        }

        // PUT ������� �����
        [HttpPut("closerent/{id}")]
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

            _rentService.CloseRent(id, refundDate);
            //���������� ���������� ��� ������
            return NoContent();
            
        }
    }
}
