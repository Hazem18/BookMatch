using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.ViewModels;
using Utility;

namespace BookMatch.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.AdminRole)]

    public class TicketCategoryController : Controller
    {
        private readonly ITicketCategoryRepository ticketCategoryRepository;

        public TicketCategoryController(ITicketCategoryRepository ticketCategoryRepository)
        {
            this.ticketCategoryRepository = ticketCategoryRepository;
        }

        public IActionResult Index()
        {
            var ticket = ticketCategoryRepository.Get();
            return View(model:ticket);
        }

        public IActionResult Create()
        {
            TicketCategoryVM ticketvm = new TicketCategoryVM();
            return View(ticketvm);
        }

        [HttpPost]
        public IActionResult Create(TicketCategoryVM ticketvm)
        {
            if (ModelState.IsValid)
            {
                var ticketCategory = new TicketCategory
                {
                    Name = ticketvm.Name,
                    AvailableTickets = ticketvm.AvailableTickets,
                    Price = ticketvm.Price,
                    SectorImage = ticketvm.SectorImage,

                };
                if (ticketvm.Image != null && ticketvm.Image.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ticketvm.Image.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\LogoUrl\\Ticket", fileName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        ticketvm.Image.CopyTo(stream);
                    }

                    ticketCategory.SectorImage = fileName;  
                }

                ticketCategoryRepository.Create(ticketCategory);
                ticketCategoryRepository.Commit();

                return RedirectToAction(nameof(Index));  
            }
            return View(model: ticketvm);
        }

        [HttpGet]
        public IActionResult Edit(int ticketId)
        {
            var ticketCategory = ticketCategoryRepository.GetOne(expression: e => e.Id == ticketId);
            if (ticketCategory != null)
            {
                var ticketCategoryVM = new TicketCategoryVMEdit()
                {
                    Id = ticketCategory.Id,
                    Name = ticketCategory.Name,
                    AvailableTickets = ticketCategory.AvailableTickets,
                    SectorImage = ticketCategory.SectorImage,
                    Price = ticketCategory.Price,

                };

                 return View(model: ticketCategoryVM);

            }
            return RedirectToAction("NotFound", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TicketCategoryVMEdit ticketVM, IFormFile SectorImage)
        {
            var oldticketCategory = ticketCategoryRepository.GetOne(expression: e => e.Id == ticketVM.Id, tracked: false);
            ModelState.Remove("SectorImage");

            if (ModelState.IsValid)
            {
                oldticketCategory.Name = ticketVM.Name;
                oldticketCategory.AvailableTickets = ticketVM.AvailableTickets;
                oldticketCategory.Price = ticketVM.Price;
                if (SectorImage != null && SectorImage.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(SectorImage.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\LogoUrl\\Ticket", fileName);

                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\LogoUrl\\Ticket", oldticketCategory.SectorImage);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        SectorImage.CopyTo(stream);
                    }

                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }

                    oldticketCategory.SectorImage = fileName;
                }
                else
                {
                    ticketVM.SectorImage = oldticketCategory.SectorImage;
                }

                ticketCategoryRepository.Edit(oldticketCategory);
                ticketCategoryRepository.Commit();

                return RedirectToAction(nameof(Index));


            }
            ticketVM.SectorImage = oldticketCategory.SectorImage;
            return View(ticketVM);
        }

        private string? UploadImg(IFormFile SectorImage)
        {
            if (SectorImage.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(SectorImage.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\LogoUrl\\Ticket", fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    SectorImage.CopyTo(stream);
                }
                return SectorImage.FileName;
            }
            return null;
        }



        public IActionResult Delete(int TicketId)
        {
            var ticket = ticketCategoryRepository.GetOne(expression: e => e.Id == TicketId);
            var oldFilepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\LogoUrl\\Ticket", ticket.SectorImage);

            if (System.IO.File.Exists(oldFilepath))
            {
                System.IO.File.Delete(oldFilepath);
            }
            if (ticket == null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            ticketCategoryRepository.Delete(ticket);
            ticketCategoryRepository.Commit();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Search(string searchTerm)
        {
            var tickets = ticketCategoryRepository.Get(expression: m => m.Name.Contains(searchTerm));

            return View("Index", tickets);
        }


    }


}


    

