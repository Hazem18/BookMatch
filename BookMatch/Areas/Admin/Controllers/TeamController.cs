using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Linq.Expressions;

namespace BookMatch.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController : Controller
    {
        private readonly ITeamRepository teamRepository;

        public TeamController(ITeamRepository teamRepository, IStadiumRepository stadiumRepository)
        {
            this.teamRepository=teamRepository;
        }

        public IActionResult Index()
        {
            var teams = teamRepository.GetTeams(
                    includeStadium: true,   // Include the Stadium property
                    includeLeagues: true    // Include related Leagues via TeamLeagues
                                                );
            return View(teams);
        }
    }
}
