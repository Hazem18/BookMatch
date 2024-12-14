using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Stripe.Checkout;
using System.Linq.Expressions;

namespace BookMatch.Areas.User.Controllers
{
    [Area("User")]
    [AllowAnonymous]
    public class CartController : Controller
    {
        private readonly ITicketPurchaseRepository ticketPurchaseRepository;
        private readonly ITicketRepository ticketRepository;
        private readonly IUserTicketRepository userTicketRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public CartController( ITicketPurchaseRepository ticketPurchaseRepository, ITicketRepository ticketRepository,IUserTicketRepository userTicketRepository, UserManager<ApplicationUser> userManager)
        {
            this.ticketPurchaseRepository = ticketPurchaseRepository;
            this.ticketRepository = ticketRepository;
            this.userTicketRepository = userTicketRepository;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var userId = userManager.GetUserId(User);
            var cartTickets = userTicketRepository.Get(includeProps: [e => e.Ticket , e => e.Ticket.Match ,
            e => e.Ticket.Match.TeamA ,e => e.Ticket.Match.TeamB ,e => e.Ticket.TicketCategory ], expression:e=>e.UserId == userId );

            var sum = cartTickets.Sum(e => e.Ticket.TicketCategory.Price);
            ViewBag.TotalPrice = Math.Round(sum, 2);

            return View(cartTickets);
        }
        public IActionResult delete(int id)
        {
            var userId = userManager.GetUserId(User);
            var userticket = userTicketRepository.GetOne(expression:e=>e.TicketId == id &&  userId == userId);

            if (userticket != null)
            {
                userTicketRepository.Delete(userticket);
                userTicketRepository.Commit();

                return RedirectToAction("index");
            }
           
            var cartTickets = userTicketRepository.Get(includeProps: [e => e.Ticket , e => e.Ticket.Match ,
            e => e.Ticket.Match.TeamA ,e => e.Ticket.Match.TeamB ,e => e.Ticket.TicketCategory ], expression: e => e.UserId == userId);

            return View(userticket);
        }
        public IActionResult Pay()
        {

            var appUser = userManager.GetUserId(User);
            if (appUser == null)
            {
                return RedirectToAction("Login", "Account", new { Area = "identity" });
            }

            var CartDb = userTicketRepository.Get(
                includeProps: [T => T.Ticket.TicketCategory, m => m.Ticket.Match, u => u.User],
                expression: a => a.UserId == appUser,
                tracked: false);

            if ( CartDb == null || !CartDb.Any())
            {
                ViewBag.ErrorMessage = "The Cart Is Empty. Please Add Items To Proceed With The Payment.";
                return View("Index", CartDb); 
            }

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = $"{Request.Scheme}://{Request.Host}/User/Cart/CompletePurchase",
                CancelUrl = $"{Request.Scheme}://{Request.Host}/User/checkout/cancel",
            };

            foreach (var cartItem in CartDb)
            {
                var ticket = cartItem.Ticket;
                var ticketCategory = ticket.TicketCategory;
                var match = ticket.Match;

                var lineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "egp",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = $"Ticket Category: {ticketCategory.Name}",
                            Description = $"Match Date: {match.DateTime:yyyy-MM-dd HH:mm}",
                            
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
            }

            var service = new SessionService();
            var session = service.Create(options);

            return Redirect(session.Url);
        }


        public IActionResult CompletePurchase()
        {
            var appUser = userManager.GetUserId(User);
            if (appUser == null)
            {
                return RedirectToAction("Login", "Account", new { Area = "identity" });
            }

            var CartDb = userTicketRepository.Get(includeProps: [
                
            t => t.Ticket.TicketCategory,
            t => t.Ticket.Match,
            t => t.Ticket.Match.Stadium,
            t => t.Ticket.Match.TeamA,
            t => t.Ticket.Match.TeamB,
            t => t.User
            ],expression: a => a.UserId == appUser,
                tracked: true );

            if (CartDb == null || !CartDb.Any())
            {
                return RedirectToAction("Index", "Cart", new { Area = "User" });
            }

            var ticketPurchases = new List<TicketPurchase>();

            foreach (var cartItem in CartDb)
            {
                if (cartItem.Ticket == null || cartItem.Ticket.TicketCategory == null || cartItem.Ticket.Match == null)
                {
                    continue;
                }

                var match = cartItem.Ticket.Match;

                if (match.Stadium == null || match.TeamA == null || match.TeamB == null)
                {
                    continue;
                }

                var ticketPurchase = new TicketPurchase
                {
                    UserId = appUser,
                    ApplicationName = cartItem.User?.UserName,
                    TicketId = cartItem.Ticket.Id,
                    SeatNumber = cartItem.Ticket.SeatNumber,
                    Price = cartItem.Ticket.TicketCategory.Price,
                    PurchaseDate = DateTime.Now,
                    TicketCategoryName = cartItem.Ticket.TicketCategory.Name,
                    PaymentStatus = "Paid",
                    StadiumName = match.Stadium.Name,
                    TeamAName = match.TeamA.Name,
                    TeamBName = match.TeamB.Name,
                    DateMatch = match.DateTime,
                };

                ticketPurchaseRepository.Create(ticketPurchase);
                ticketPurchases.Add(ticketPurchase);
            }

            ticketPurchaseRepository.Commit();

            foreach (var cartItem in CartDb)
            {
                userTicketRepository.Delete(cartItem);
            }

            userTicketRepository.Commit();

            return View(ticketPurchases);
        }

    }
}
