using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Diagnostics;

namespace BookMatch.Areas.Customer.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITicketRepository ticketRepository;
        private readonly IMatchRepository matchRepository;
        private readonly ITicketCategoryRepository ticketCategoryRepository;

        public HomeController(ILogger<HomeController> logger, ITicketRepository ticketRepository,
            IMatchRepository matchRepository , ITicketCategoryRepository ticketCategoryRepository)
        { 
            _logger = logger;
            this.ticketRepository = ticketRepository;
            this.matchRepository = matchRepository;
            this.ticketCategoryRepository = ticketCategoryRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int id) 
        {
            var match = matchRepository.GetOne(expression: e=>e.Id == id ,
                includeProps: [e=>e.Stadium , e => e.TeamA , e => e.TeamB , e => e.League ]);
            if (match == null)
            {
                return RedirectToAction ("Notfound");  
            }
            else
            {
                var ticketcategories = ticketCategoryRepository.Get();
                ViewBag.TicketCategories = ticketcategories;
                return View(match);
            }
        }
        [HttpPost]
        public IActionResult Details(int id, int ticketCategoryId , string seatNumber)
        {
            return View();
        }
            public IActionResult Notfound()
        {

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
