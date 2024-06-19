using Microsoft.AspNetCore.Mvc;
using Logic.Services;

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



        [HttpGet(Name = "GetRentals")]
        public IActionResult Get()
        {
            return Ok(_reportService.GetRentalsJson());
        }

        [HttpGet("{id}", Name = "GetUsersRent")]
        public IActionResult Get(int id)
        {
            return Ok(_reportService.GetRentalsByUserJson(id));
        }
    }
}
