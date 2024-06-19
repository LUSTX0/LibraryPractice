using Microsoft.AspNetCore.Mvc;
using Logic.Services;

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
            return Ok( _reportService.GetBooksJson());
        }
    }
}
