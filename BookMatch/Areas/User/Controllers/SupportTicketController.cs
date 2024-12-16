using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.ViewModels;

namespace BookMatch.Areas.User.Controllers
{
    [Area("User")]
    public class SupportTicketController : Controller
    {
        private readonly ISupportTicketRepository supportTicketRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public SupportTicketController(
            ISupportTicketRepository supportTicketRepository,
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            this.supportTicketRepository=supportTicketRepository;
            this.userManager=userManager;
            this.roleManager=roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
           
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SupportTicketVM supportTicketVM)
        {
            if(ModelState.IsValid)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Account", new { area = "Identity" });
                }
                var userId = userManager.GetUserId(User);
                var supporticket = new 
                    SupportTicket() 
                {
                    Title = supportTicketVM.Title,
                    Description = supportTicketVM.Description,
                    CreatedAt = DateTime.Now,
                    IsResolved = false,
                    UserId = userId,
                    IsViewed = false
                    
                };
                supportTicketRepository.Create(supporticket);
                supportTicketRepository.Commit();
                TempData["success"] = "Your support ticket is submited successfully....";
                return RedirectToAction("Index", "Home", new { area = "user" });
            }
            else
            return View(supportTicketVM);
        }



    }
}
