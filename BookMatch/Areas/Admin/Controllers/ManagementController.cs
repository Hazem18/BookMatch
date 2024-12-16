using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utility;

namespace BookMatch.Areas.Admin.Controllers
{

        [Area("Admin")]
        [Authorize(Roles = SD.AdminRole)]
    public class ManagementController : Controller
    {
        private readonly IOldMatchRepository oldMatchRepository;

        public ManagementController(IOldMatchRepository oldMatchRepository)
        {
            this.oldMatchRepository = oldMatchRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult previousMatches()
        {
            var oldMatches = oldMatchRepository.Get().OrderBy(e=>e.MatchDate);
            return View(oldMatches);
        }

    }
}
