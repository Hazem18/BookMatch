using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utility;

namespace BookMatch.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.AdminRole)]
    public class SupportController : Controller
    {
        private readonly ISupportTicketRepository supportTicketRepository;

        public SupportController(ISupportTicketRepository supportTicketRepository)
        {
            this.supportTicketRepository = supportTicketRepository;
        }
        public IActionResult Index()
        {
            var supportTickets = supportTicketRepository.Get(includeProps: [e => e.User])
                .OrderBy(e=>e.IsViewed).ThenByDescending(e=>e.CreatedAt).ToList();
            return View(supportTickets);
        }
        public IActionResult details(int id)
        {
            var ticket = supportTicketRepository.GetOne(expression:e=>e.Id == id, includeProps: [e => e.User]);
            if (ticket != null)
            {
                ticket.IsViewed = true;
                supportTicketRepository.Commit();
            return View(ticket);
            }
            else
            {
                return RedirectToAction("NotFound", "home");
            }
        }
 
        public IActionResult solved(int id)
        {
            var ticket = supportTicketRepository.GetOne(expression: e => e.Id == id, includeProps: [e => e.User]);
            if (ticket != null)
            {
                ticket.IsViewed = true;
                ticket.IsResolved = true;
                supportTicketRepository.Commit();
                return RedirectToAction("index");
            }
            else
            {
                return RedirectToAction("NotFound", "home");
            }
        }
    }
}
