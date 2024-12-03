using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace BookMatch.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TicketCategory : Controller
    {
        private readonly ITicketCategoryRepository ticketCategoryRepository;

        public TicketCategory(ITicketCategoryRepository ticketCategoryRepository)
        {
            this.ticketCategoryRepository = ticketCategoryRepository;
        }

        public IActionResult Index()
        {
            var ticet = ticketCategoryRepository.Get();
            return View(model:ticet);
        }
    }
}
