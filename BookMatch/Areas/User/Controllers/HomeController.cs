using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Diagnostics;
using Utility;

namespace BookMatch.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITicketRepository ticketRepository;
        private readonly IMatchRepository matchRepository;
        private readonly ITicketCategoryRepository ticketCategoryRepository;
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager;
        private readonly Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> roleManager;
        private readonly IUserTicketRepository userTicketRepository;


        public HomeController(ILogger<HomeController> logger, ITicketRepository ticketRepository,
            IMatchRepository matchRepository , ITicketCategoryRepository ticketCategoryRepository,
            UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager ,
           IUserTicketRepository userTicketRepository)
        { 
            _logger = logger;
            this.ticketRepository = ticketRepository;
            this.matchRepository = matchRepository;
            this.ticketCategoryRepository = ticketCategoryRepository;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.userTicketRepository = userTicketRepository;
            
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
            
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }
            else if(!User.IsInRole(SD.UserRole))
            {
                var ticketcategories = ticketCategoryRepository.Get();
                ViewBag.TicketCategories = ticketcategories;
                TempData["NotAllowed"] = "not allowed to book tickets as an admin";
                return View();
            }
            else
            {
                    var userId = userManager.GetUserId(User);
                var bookedTicket = userTicketRepository.GetOne(includeProps: [e=>e.Ticket]  , expression: e => e.UserId == userId && e.Ticket.MatchId==id);
              if (bookedTicket != null)
                {
                    TempData["bought"] = "you already bought a ticket of this match";
                    var ticketcategories = ticketCategoryRepository.Get();
                    ViewBag.TicketCategories = ticketcategories;
                    return View();
                }
                
                
                if (ticketCategoryId != null && seatNumber != null)
                {

                    var ticket = new Ticket()
                    {
                        MatchId=id,
                        TicketCategoryId = ticketCategoryId,
                        SeatNumber = seatNumber
                    };
                    ticketRepository.Create(ticket);
                    ticketRepository.Commit();


                    var userTicket = new UserTicket()
                    {
                        UserId = userId,
                        TicketId = ticket.Id
                    };
                    userTicketRepository.Create(userTicket);
                   userTicketRepository.Commit();

                    TempData["success"] = "ticket add to your cart successfully";

                return RedirectToAction();
                }
                else
                {
                    var ticketcategories = ticketCategoryRepository.Get();
                    ViewBag.TicketCategories = ticketcategories;
                    ModelState.AddModelError(string.Empty, "category requird and seat reaquired");
                    return View();
                }
                
            }
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
