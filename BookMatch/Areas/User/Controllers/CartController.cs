using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Stripe.Checkout;

namespace BookMatch.Areas.User.Controllers
{
    [Area("User")]
    [AllowAnonymous]
    public class CartController : Controller
    {
        private readonly ITicketRepository ticketRepository;
        private readonly IUserTicketRepository userTicketRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public CartController(ITicketRepository ticketRepository,IUserTicketRepository userTicketRepository, UserManager<ApplicationUser> userManager)
        {
            this.ticketRepository = ticketRepository;
            this.userTicketRepository = userTicketRepository;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Pay()
        {
            var appUser = userManager.GetUserId(User);
            if (appUser == null)
            {
                return RedirectToAction("Login", "Account", new { Area = "identity" });
            }

            var CartDb = userTicketRepository.GetOne(
                includeProps: [T => T.Ticket, u => u.User],
                expression: a => a.UserId == appUser,
                tracked: false);


            if (CartDb == null || CartDb.Ticket == null)
            {
                return View("Error", new ErrorViewModel { Message = "No ticket found or ticket data is incomplete." });
            }

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = $"{Request.Scheme}://{Request.Host}/User/checkout/success",
                CancelUrl = $"{Request.Scheme}://{Request.Host}/User/checkout/cancel",
            };

            var ticket = CartDb.Ticket;
            var ticketCategory = ticket.TicketCategory;

            var lineItem = new SessionLineItemOptions()
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = "egp", 
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = ticketCategory.Name,
                        Metadata = new Dictionary<string, string>
                {
                    { "SeatNumber", ticket.SeatNumber ?? "N/A" } 
                }
                    },
                    UnitAmount = (long)(ticketCategory.Price * 100), 
                },
                Quantity = 1 
            };

            options.LineItems.Add(lineItem);

            var service = new SessionService();
            var session = service.Create(options);

            return Redirect(session.Url);
        }

    }
}
