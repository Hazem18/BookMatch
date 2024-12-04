using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.ViewModels;
using Utility;

namespace BookMatch.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.AdminRole)]
    public class MatchController : Controller
    {
        private readonly IMatchRepository matchRepository;
        private readonly ITeamRepository teamRepository;
        private readonly ILeagueRepository leagueRepository;
        private readonly ITicketCategoryRepository ticketCategoryRepository;
        private readonly ITeamLeagueRepository teamLeagueRepository;

        public MatchController(IMatchRepository matchRepository, ITeamRepository teamRepository
            , ILeagueRepository leagueRepository, ITicketCategoryRepository ticketCategoryRepository
            ,ITeamLeagueRepository teamLeagueRepository)
        {
            this.matchRepository = matchRepository;
            this.teamRepository = teamRepository;
            this.leagueRepository = leagueRepository;
            this.ticketCategoryRepository = ticketCategoryRepository;
            this.teamLeagueRepository = teamLeagueRepository;
        }
        public IActionResult Index()
        {
            var matches = matchRepository.Get(includeProps: [e => e.League, e => e.Stadium, e => e.Tickets ,e=>e.TeamA , e=>e.TeamB]);
            return View(matches);
        }
        public IActionResult Create()
        {
            var teams = teamRepository.Get(includeProps: [e => e.Stadium, e => e.TeamLeagues]);
            ViewBag.Teams = teams;

            var Leagues = leagueRepository.Get();
            ViewBag.Leagues = Leagues;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MatchVM matchVM)
        {
            ModelState.Remove("Tickets");
            if (ModelState.IsValid)
            {
                var match = new Match();

                if(matchVM.DateTime <= DateTime.Now)
                {
                    ModelState.AddModelError("DateTime", "this date is expired");

                    var teams = teamRepository.Get(includeProps: [e => e.Stadium, e => e.TeamLeagues]);
                    ViewBag.Teams = teams;

                    var Leagues = leagueRepository.Get();
                    ViewBag.Leagues = Leagues;
                    return View(matchVM);
                }

                match.DateTime= matchVM.DateTime;

                if(matchVM.TeamAId == match.TeamBId)
                {
                    ModelState.AddModelError(string.Empty, "you can't chose the same team as team home and team away");

                    var teams = teamRepository.Get(includeProps: [e => e.Stadium, e => e.TeamLeagues]);
                    ViewBag.Teams = teams;

                    var Leagues = leagueRepository.Get();
                    ViewBag.Leagues = Leagues;
                    return View(matchVM);
                }

                match.TeamAId = matchVM.TeamAId;
                match.TeamBId = matchVM.TeamBId;
                match.StadiumId = teamRepository.Get(expression :e=>e.Id ==matchVM.TeamAId, includeProps: [e => e.Stadium])
                    .FirstOrDefault().Stadium.Id;

                var teamALeaue = teamLeagueRepository.Get(expression:e=>e.TeamId==match.TeamAId && e.LeagueId==matchVM.LeagueId).FirstOrDefault();
                var teamBLeaue = teamLeagueRepository.Get(expression:e=>e.TeamId==match.TeamBId && e.LeagueId == matchVM.LeagueId).FirstOrDefault();

                if(teamALeaue == null)
                {
                    ModelState.AddModelError(string.Empty, "team home does not play in this league");

                    var teams = teamRepository.Get(includeProps: [e => e.Stadium, e => e.TeamLeagues]);
                    ViewBag.Teams = teams;

                    var Leagues = leagueRepository.Get();
                    ViewBag.Leagues = Leagues;
                    return View(matchVM);
                }
                if (teamBLeaue == null)
                {
                    ModelState.AddModelError(string.Empty, "team away does not play in this league");

                    var teams = teamRepository.Get(includeProps: [e => e.Stadium, e => e.TeamLeagues]);
                    ViewBag.Teams = teams;

                    var Leagues = leagueRepository.Get();
                    ViewBag.Leagues = Leagues;
                    return View(matchVM);
                }




                match.LeagueId=matchVM.LeagueId;



                match.Status=MatchStatus.Available;

                matchRepository.Create(match);
                matchRepository.Commit();

              return  RedirectToAction("Index");

                
            }
            else
            {

            var teams = teamRepository.Get(includeProps: [e => e.Stadium, e => e.TeamLeagues]);
            ViewBag.Teams = teams;

            var Leagues = leagueRepository.Get();
            ViewBag.Leagues = Leagues;
            return View(matchVM);
            }
        }
    }
}
