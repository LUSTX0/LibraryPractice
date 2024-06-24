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

        //����� �����
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

        //����� �� �������������
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

        // ������� ������ �� ������ �� ID
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

        // ������ � ������
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
                //�������� ������� id ������������ ������������ VT

                return CreatedAtAction(nameof(GetRental), new { id = newBookRent.IdBooksRent }, newBookRent);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = ex.Message, details = ex.InnerException?.Message });
            }
        }

        // PUT ������� �����
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
                //���������� ���������� ��� ������
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = ex.Message, details = ex.InnerException?.Message });
            }
        }
    }
}
