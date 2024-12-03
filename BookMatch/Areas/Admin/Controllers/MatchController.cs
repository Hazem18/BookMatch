using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Utility;

namespace BookMatch.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =SD.AdminRole)]
    public class MatchController : Controller
    {
        private readonly IMatchRepository matchRepository;
        private readonly ITeamRepository teamRepository;
        private readonly ILeagueRepository leagueRepository;
        private readonly ITicketCategoryRepository ticketCategoryRepository;

        public MatchController( IMatchRepository matchRepository,ITeamRepository teamRepository
            ,ILeagueRepository leagueRepository, ITicketCategoryRepository ticketCategoryRepository)
        {
            this.matchRepository = matchRepository;
            this.teamRepository = teamRepository;
            this.leagueRepository = leagueRepository;
            this.ticketCategoryRepository = ticketCategoryRepository;
        }
        public IActionResult Index()
        {
            var matches = matchRepository.Get(includeProps: [e => e.League , e => e.Stadium, e => e.Tickets]);
            return View(matches);
        }
        public IActionResult Create() 
        {
            var teams = teamRepository.Get(includeProps: [e=>e.Stadium,e=>e.TeamLeagues]);
            ViewBag.Teams = teams;
            return View();
        }
    }
}
