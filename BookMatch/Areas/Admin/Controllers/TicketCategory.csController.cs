using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.ViewModels;

namespace BookMatch.Areas.Admin.Controllers
{
    [Area("Admin")]
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

                 return View(model: ticketCategory);

            }
            return RedirectToAction("NotFound", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TicketCategory ticket, IFormFile SectorImage)
        {
            var oldticketCategory = ticketCategoryRepository.GetOne(expression: e => e.Id == ticket.Id, tracked: false);

            if (ModelState.IsValid)
            {
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

                    ticket.SectorImage = fileName;
                }
                else
                {
                    ticket.SectorImage = oldticketCategory.SectorImage;
                }

                ticketCategoryRepository.Edit(ticket);
                ticketCategoryRepository.Commit();

                return RedirectToAction(nameof(Index));


            }
            ticket.SectorImage = oldticketCategory.SectorImage;
            return View(ticket);
        }

        private string? UploadImg(IFormFile Imag)
        {
            if (Imag.Length > 0)
            {
                var fileName = Imag.FileName;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\LogoUrl\\Ticket", fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    Imag.CopyTo(stream);
                }
                return Imag.FileName;
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


    

