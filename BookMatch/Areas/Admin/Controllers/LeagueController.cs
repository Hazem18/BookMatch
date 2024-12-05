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

    public class LeagueController : Controller
    {
        private readonly ILeagueRepository leagueRepository;

        public LeagueController(ILeagueRepository leagueRepository)
        {
            this.leagueRepository = leagueRepository;
        }

        public IActionResult Index()
        {
            var Liga = leagueRepository.Get();
            return View(model: Liga);
        }

        public IActionResult Create()
        {
            LeagueVM leaguevm = new LeagueVM();
            return View(leaguevm);
        }
        [HttpPost]
        public IActionResult Create(LeagueVM leagueVm)
        {
            if (ModelState.IsValid)
            {
                var league = new League
                {
                    Name = leagueVm.Name,
                    Description = leagueVm.Description,
                    LogoUrl = leagueVm.LogoUrl,
                };
                if (leagueVm.Logo != null &&leagueVm.Logo.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(leagueVm.Logo.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\LogoUrl", fileName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                       leagueVm.Logo.CopyTo(stream);
                    }

                    league.LogoUrl = fileName;
                }
               
                leagueRepository.Create(league);
                leagueRepository.Commit();

                return RedirectToAction(nameof(Index));
            }
            return View(model: leagueVm);
        }
        [HttpGet]

        public IActionResult Edit(int leagueId)
        {
            var league = leagueRepository.GetOne(expression: e => e.Id == leagueId);
            if (league != null)
            {
                var leagueVM = new LeagueVMEdit()
                { 
                    Id = league.Id,
                    Name = league.Name,
                    Description = league.Description,
                    LogoUrl = league.LogoUrl,
                };

                return View(model: leagueVM);
            }

            return RedirectToAction("NotFound", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(LeagueVMEdit leagueVM, IFormFile LogoUrl)
        {
            var oldLeague = leagueRepository.GetOne(expression: e => e.Id == leagueVM.Id, tracked:false);
            ModelState.Remove("LogoUrl");
            if (ModelState.IsValid)
            {
                oldLeague.Name = leagueVM.Name;
                oldLeague.Description = leagueVM.Description;
                
                if (LogoUrl != null && LogoUrl.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(LogoUrl.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\LogoUrl", fileName);

                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\LogoUrl", oldLeague.LogoUrl);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        LogoUrl.CopyTo(stream);
                    }

                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }

                    oldLeague.LogoUrl = fileName;
                }
                
                else
                {
                    leagueVM.LogoUrl = oldLeague.LogoUrl;

                }

                leagueRepository.Edit(oldLeague);
                leagueRepository.Commit();

                return RedirectToAction(nameof(Index));
            }

            leagueVM.LogoUrl = oldLeague.LogoUrl;
            return View(leagueVM);
        }

        private string? UploadImg(IFormFile logo)
        {
            if (logo.Length > 0)
            {
                var fileName = logo.FileName;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\LogoUrl", fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    logo.CopyTo(stream);
                }
                return logo.FileName;
            }
            return null;
        }
        public IActionResult Delete(int leagueId)
        {
            var league = leagueRepository.GetOne(expression: e=>e.Id== leagueId);
            var oldFilepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\LogoUrl", league.LogoUrl);

            if (System.IO.File.Exists(oldFilepath))
            {
                System.IO.File.Delete(oldFilepath);
            }
            if (league == null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            leagueRepository.Delete(league);
            leagueRepository.Commit();
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Search(string searchTerm)
        {
            var league = leagueRepository.Get(expression: m => m.Name.Contains(searchTerm));

            return View("Index",league);
        }
    }

}

