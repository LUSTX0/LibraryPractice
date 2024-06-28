using Microsoft.AspNetCore.Mvc;
using Logic.Services;
using SQLcon.Models;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Globalization;

namespace LIBRARY2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IReportService _reportService;
        public BookController(IBookService bookService, IReportService reportService)
        {
            _bookService = bookService;
            _reportService = reportService;
        }


        [HttpGet(Name = "GetBooks")]
        public IActionResult Get()
        {
            string result = _reportService.GetBooksJson();
            if (result != "null")
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
            
        }

        [HttpGet("{id}", Name = "GetBook")]
        public IActionResult Get(int id)
        {
            string result = _bookService.GetBook(id);
            if (result!= "null")
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }                   
        }

        [HttpPost]
        public IActionResult Create([FromBody] Book newBook)
        {
            if (newBook == null)
            {
                return BadRequest();
            }
            _bookService.AddBookObj(newBook);            

            return CreatedAtAction(nameof(Get), new { id = newBook.IdBooks }, newBook);            
        }

        // PUT редактировать описание книги
        [HttpPut("updating/{id}")]
        public IActionResult Update(int id, [FromBody] Book updatedBook)
        {
            if (updatedBook == null)
            {
                return BadRequest();
            }
            if(! _bookService.UpdateBookObj(id, updatedBook))
            {
                return NotFound();
            }
                        
            return Ok(_bookService.GetBook(id));            
        }

        // PUT списать книгу
        [HttpPut("write-off/{id}")]
        public IActionResult Update(int id, [FromBody] string Date)
        {
            if (Date == null)
            {
                return BadRequest();
            }
            if (!DateOnly.TryParseExact(Date, "yyyy-MM-dd", null, DateTimeStyles.None, out DateOnly date))
            {
                return BadRequest("Invalid date format");
            }
            if(!_bookService.WriteOFFBook(id, date))
            {
                return NotFound();
            }
            
            return Ok(_bookService.GetBook(id));            
        }

        // DELETE api/items/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_bookService.DeleteBook(id))
            {
                return NotFound();
            }           

            return NoContent();            
        }
    }
}
