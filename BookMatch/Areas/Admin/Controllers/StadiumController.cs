using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.ViewModels;

namespace BookMatch.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StadiumController : Controller
    {
        private readonly IStadiumRepository stadiumRepository;

        public StadiumController(IStadiumRepository stadiumRepository)
        {
            this.stadiumRepository=stadiumRepository;
        }

        public IActionResult Index()
        {
            var stadiums = stadiumRepository.Get();
            return View(stadiums);
        }
        public IActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StadiumVM stadiumVM)
        {
            if (ModelState.IsValid)
            {
                var stadium = new Stadium()
                {
                    Name = stadiumVM.Name,
                    Capacity=stadiumVM.Capacity,
                    LocationUrl=stadiumVM.LocationUrl,
                    Description=stadiumVM.Description
                };
                stadiumRepository.Create(stadium);
                stadiumRepository.Commit();

                return RedirectToAction("Index", "Stadium", new { area = "admin" });
            }
            else
                return View(stadiumVM);
        }

        public IActionResult Edit(int Id)
        {
            var stadium = stadiumRepository.GetOne(expression: e => e.Id == Id);
            if (stadium != null)
            {
                var stadiumVM = new StadiumVM()
                {
                    Id=stadium.Id,
                    Name=stadium.Name,
                    Capacity=stadium.Capacity,
                    LocationUrl=stadium.LocationUrl,
                    Description=stadium.Description
                };
                return View(stadiumVM);
            }
            else
                return RedirectToAction("Notfound", "Home", new { area = "User" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(StadiumVM stadiumVM)
        {
            if (ModelState.IsValid)
            {
                var stadium = new Stadium()
                {
                    Id=stadiumVM.Id,
                    Name = stadiumVM.Name,
                    Capacity=stadiumVM.Capacity,
                    LocationUrl=stadiumVM.LocationUrl,
                    Description=stadiumVM.Description
                };
                stadiumRepository.Edit(stadium);
                stadiumRepository.Commit();
                return RedirectToAction("Index", "Stadium", new { area = "admin" });
            }

            return View(stadiumVM);
        }
        public IActionResult Delete(int Id)
        {
            var stadium = stadiumRepository.GetOne(expression: e => e.Id == Id);
            if (stadium != null)
            {
                stadiumRepository.Delete(stadium);
                stadiumRepository.Commit();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Notfound", "Home", new { area = "User" });
        }

    }
}
