using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Diagnostics;
using System.Threading.Tasks.Sources;
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
        private readonly ILeagueRepository leagueRepository;
        private readonly ITicketPurchaseRepository ticketPurchaseRepository;
        private readonly IOldMatchRepository oldMatchRepository;

        public HomeController(ILogger<HomeController> logger,
            ITicketRepository ticketRepository,
            IMatchRepository matchRepository,
            ITicketCategoryRepository ticketCategoryRepository,
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
           IUserTicketRepository userTicketRepository, ILeagueRepository leagueRepository,
           ITicketPurchaseRepository ticketPurchaseRepository , IOldMatchRepository oldMatchRepository)
        {
            _logger = logger;
            this.ticketRepository = ticketRepository;
            this.matchRepository = matchRepository;
            this.ticketCategoryRepository = ticketCategoryRepository;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.userTicketRepository = userTicketRepository;
            this.leagueRepository = leagueRepository;
            this.ticketPurchaseRepository = ticketPurchaseRepository;
            this.oldMatchRepository = oldMatchRepository;
        }

        public IActionResult Index()
        {
            var oldMatches = matchRepository.Get
           (includeProps:
           [e => e.League,
           e => e.Stadium,
           e => e.Tickets,
           e => e.TeamA,
           e => e.TeamB],expression:e=>e.DateTime.Day<DateTime.Now.Day);

            foreach(var item in oldMatches)
            {
                var proMatches = ticketPurchaseRepository.Get(expression:e=>e.MatchId== item.Id 
                    ,tracked:false).Select( e=> new { e.SeatNumber , e.Price} );
                var oldmatch = new OldMatch()
                {
                    MatchDate = item.DateTime,
                    TeamHomeName = item.TeamA.Name,
                    TeamAwayName = item.TeamB.Name,
                    StadiumName =item.Stadium.Name,
                    LeagueName = item.League.Name,
                Users = proMatches.Count(),
                TolalSales = (double)proMatches.Sum(e => e.Price)
                    
                };
                oldMatchRepository.Create(oldmatch);
                oldMatchRepository.Commit();

                matchRepository.Delete(item);
                matchRepository.Commit();

            }




            var matches = matchRepository.Get
           (includeProps:
           [e => e.League,
           e => e.Stadium,
           e => e.Tickets,
           e => e.TeamA,
           e => e.TeamB])
           .OrderBy(e=>e.DateTime);
            ViewBag.Leagues = leagueRepository.Get();
            return View(matches);
        }

        public IActionResult LeagueMatches(int LeagueId)
        {
            var matches = matchRepository.Get
           (includeProps:
           [e => e.League,
           e => e.Stadium,
           e => e.Tickets,
           e => e.TeamA,
           e => e.TeamB], expression: e => e.LeagueId == LeagueId).OrderBy(e => e.DateTime);
            // Retrieve the league name and set it in ViewData for the page title
            var leagueName = leagueRepository.
            GetOne(expression: e => e.Id == LeagueId)?.Name;
            ViewData["LeagueName"] = leagueName;
            ViewBag.Leagues = leagueRepository.Get();
            return View(matches);
        }
        public IActionResult Details(int id)
        {
            var match = matchRepository.GetOne(expression: e => e.Id == id,
                includeProps: [e => e.Stadium, e => e.TeamA, e => e.TeamB, e => e.League]);
            if (match == null)
            {
                return RedirectToAction("Notfound");
            }
            else
            {

                IEnumerable<string> puchasedSeat = ticketPurchaseRepository.Get(expression: e => e.MatchId == id 
                , tracked: false).Select(e => e.SeatNumber);

                IEnumerable<string> timedTicketSeats = userTicketRepository.Get(expression: e => e.Ticket.MatchId == id 
                  && e.BookingDate.AddMinutes(10) > DateTime.Now, includeProps: [e => e.Ticket], tracked: false).Select(e=>e.Ticket.SeatNumber);


                var bookedSeats = puchasedSeat.Union(timedTicketSeats).ToList();


                ViewBag.BookedSeats= bookedSeats;

                var ticketcategories = ticketCategoryRepository.Get();
                ViewBag.TicketCategories = ticketcategories;
                return View(match);
            }
        }
        [HttpPost]
        public IActionResult Details(int id, int ticketCategoryId, string seatNumber)
        {
            if (ModelState.IsValid)
            {

                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Account", new { area = "Identity" });
                }
                else if (!User.IsInRole(SD.UserRole))
                {
                    var ticketcategories = ticketCategoryRepository.Get();
                    ViewBag.TicketCategories = ticketcategories;
                    TempData["NotAllowed"] = "Not Allowed To Book Tickets as An Admin";


                    var match = matchRepository.GetOne(expression: e => e.Id == id,
                      includeProps: [e => e.Stadium, e => e.TeamA, e => e.TeamB, e => e.League]);
                    return View(match);
                }
                else
                {

                    //check if booking this match is expired
                    var expriredMatch = matchRepository.GetOne(expression: e => e.Id == id, tracked: false);
                    if(expriredMatch.DateTime.Day <= DateTime.Now.AddDays(1).Day)
                    {
                        TempData["bought"] = "booking this match is expired";
                        var ticketcategories = ticketCategoryRepository.Get();
                        ViewBag.TicketCategories = ticketcategories;

                        var match = matchRepository.GetOne(expression: e => e.Id == id,
                        includeProps: [e => e.Stadium, e => e.TeamA, e => e.TeamB, e => e.League]);
                        return View(match);
                    }
                    


                    //check if same user book/ bought a ticket of the same match
                    var userId = userManager.GetUserId(User);
                    var bookedTicket = userTicketRepository.GetOne(includeProps: [e => e.Ticket], expression: e => e.UserId == userId && e.Ticket.MatchId == id, tracked: false);
                    var purchasedTicket = ticketPurchaseRepository.GetOne(expression: e => e.UserId == userId &&
                    e.MatchId == id, tracked: false);
                    if (bookedTicket != null || purchasedTicket != null)
                    {
                        TempData["bought"] = "You Already Booked A Ticket Of This Match";
                        var ticketcategories = ticketCategoryRepository.Get();
                        ViewBag.TicketCategories = ticketcategories;

                        var match = matchRepository.GetOne(expression: e => e.Id == id,
                        includeProps: [e => e.Stadium, e => e.TeamA, e => e.TeamB, e => e.League]);
                        return View(match);
                    }

                    //chech if someone else bought the same ticket with the same seat number 

                    var puchasedSeat = ticketPurchaseRepository.GetOne(expression: e=>e.MatchId == id && e.SeatNumber == seatNumber
                     , tracked: false);

                    if (puchasedSeat != null)
                    {
                        TempData["bought"] = "This Seat Has Been Already Bought, Please Choose Other Seat, (Purches)";
                        var ticketcategories = ticketCategoryRepository.Get();
                        ViewBag.TicketCategories = ticketcategories;

                        var match = matchRepository.GetOne(expression: e => e.Id == id,
                        includeProps: [e => e.Stadium, e => e.TeamA, e => e.TeamB, e => e.League]);
                        return View(match);
                    }

                    //chech if someone else booked the ticket in his cart before 10 mins runs out

                    var timedTicket = userTicketRepository.GetOne(expression: e => e.Ticket.MatchId == id && e.Ticket.SeatNumber == seatNumber
                      && e.BookingDate.AddMinutes(10) > DateTime.Now, includeProps: [e => e.Ticket], tracked: false);

                    if (timedTicket != null)
                    {
                        TempData["bought"] = "This Seat Has Been Already Bought, Please Choose Other Seat, (10 mins)";
                        var ticketcategories = ticketCategoryRepository.Get();
                        ViewBag.TicketCategories = ticketcategories;

                        var match = matchRepository.GetOne(expression: e => e.Id == id,
                    includeProps: [e => e.Stadium, e => e.TeamA, e => e.TeamB, e => e.League]);
                        return View(match);
                    }


                    var ticket = ticketRepository.GetOne(expression: e => e.MatchId == id && e.SeatNumber == seatNumber);
                    if (ticket == null)
                    {

                        ticket = new Ticket()
                        {
                            MatchId = id,
                            TicketCategoryId = ticketCategoryId,
                            SeatNumber = seatNumber
                        };
                        ticketRepository.Create(ticket);
                        ticketRepository.Commit();

                    }

                    var userTicket = new UserTicket()
                    {
                        UserId = userId,
                        TicketId = ticket.Id,
                        BookingDate = DateTime.Now

                    };
                    userTicketRepository.Create(userTicket);
                    userTicketRepository.Commit();

                    TempData["success"] = "Ticket Add To Your Cart Successfully";

                    return RedirectToAction();

                }
            }
            else
            {
                var ticketcategories = ticketCategoryRepository.Get();
                ViewBag.TicketCategories = ticketcategories;
                ModelState.AddModelError(string.Empty, "Category Requird And Seat Reaquired ");

                var match = matchRepository.GetOne(expression: e => e.Id == id,
                includeProps: [e => e.Stadium, e => e.TeamA, e => e.TeamB, e => e.League]);
                return View(match);
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
