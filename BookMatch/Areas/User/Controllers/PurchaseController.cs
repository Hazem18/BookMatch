using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace BookMatch.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class PurchaseController : Controller
    {
        private readonly ITicketPurchaseRepository ticketPurchaseRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public PurchaseController(ITicketPurchaseRepository ticketPurchaseRepository, UserManager<ApplicationUser> userManager)
        {
            this.ticketPurchaseRepository = ticketPurchaseRepository;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Login", "Account", new { Area = "Identity" });
            }

            List<TicketPurchase> tickets;

            if (User.IsInRole("Admin"))
            {
                tickets = ticketPurchaseRepository.Get(includeProps: [t=>t.Ticket.Match, t=>t.Ticket.TicketPurchases]).ToList();
            }
            else
            {
                tickets = ticketPurchaseRepository.Get(
                    expression: t => t.UserId == currentUser.Id,
                    includeProps: [t=>t.Ticket.Match,  t=>t.Ticket.TicketPurchases]
                ).ToList();
            }

            return View(tickets);
        }
    }
}
