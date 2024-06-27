using Microsoft.AspNetCore.Mvc;
using Logic.Services;
using SQLcon.Models;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            return Ok(_reportService.GetBooksJson());
            
        }

        [HttpGet("book/{id}", Name = "GetBook")]
        public IActionResult Get(int id)
        {
            return Ok(_bookService.GetBook(id));
            
        }
        [HttpPost]
        public IActionResult Create([FromBody] Book newBook)
        {
            if (newBook == null)
            {
                return BadRequest();
            }
            _bookService.AddBookObj(newBook);
            //добавить возврат id добавленного пользователя VT

            return CreatedAtAction(nameof(Get), new { id = newBook.IdBooks }, newBook);
            
        }

        // PUT редактировать описание книги
        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] Book updatedBook)
        {
            if (updatedBook == null)
            {
                return BadRequest();
            }
            _bookService.UpdateBookObj(id, updatedBook);
            //посмотреть правильный код ответа
            return NoContent();
            
        }

        // PUT списать книгу
        [HttpPut("writeoff/{id}")]
        public IActionResult Update(int id, [FromBody] DateOnly? date)
        {
            if (date == null)
            {
                return BadRequest();
            }
            _bookService.WriteOFFBook(id, (DateOnly)date);
            //посмотреть правильный код ответа
            return NoContent();
            
        }

        // DELETE api/items/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            _bookService.DeleteBook(id);

            return NoContent();
            
        }
    }
}
