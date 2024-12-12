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
        private readonly IMatchRepository matchRepository;
        private readonly ITeamRepository teamRepository;
        private readonly ILeagueRepository leagueRepository;
        private readonly IStadiumRepository stadiumRepository;

        public HomeController(ILogger<HomeController> logger , IMatchRepository matchRepository , ITeamRepository teamRepository , ILeagueRepository leagueRepository , IStadiumRepository stadiumRepository)
        {
            _logger = logger;
            this.matchRepository=matchRepository;
            this.teamRepository=teamRepository;
            this.leagueRepository=leagueRepository;
            this.stadiumRepository=stadiumRepository;
        }

        public IActionResult Index()
        {
            var matches = matchRepository.Get
           (includeProps: 
           [e => e.League, 
           e => e.Stadium, 
           e => e.Tickets, 
           e => e.TeamA, 
           e => e.TeamB]);
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
           e => e.TeamB] , expression: e => e.LeagueId == LeagueId);

            // Retrieve the league name and set it in ViewData for the page title
            var leagueName = leagueRepository.
                GetOne(expression: e => e.Id == LeagueId)?.Name;
            ViewData["LeagueName"] = leagueName;
            ViewBag.Leagues = leagueRepository.Get();
            return View(matches);
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
