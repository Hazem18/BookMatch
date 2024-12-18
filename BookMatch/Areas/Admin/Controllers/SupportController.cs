using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Utility;

namespace BookMatch.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.AdminRole)]
    public class SupportController : Controller
    {
        private readonly ISupportTicketRepository supportTicketRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public SupportController(ISupportTicketRepository supportTicketRepository, UserManager<ApplicationUser> userManager)
        {
            this.supportTicketRepository = supportTicketRepository;
            this.userManager = userManager;
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
        public async Task<IActionResult> AllUser()
        {
            var user = await userManager.Users.ToListAsync();
                return View(user);
        }
        public async Task<IActionResult> Block(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.IsBlocked = true;
                await userManager.UpdateAsync(user);
                ViewData["Message"] = "User has been blocked successfully.";
            }

            return RedirectToAction("AllUser", "Support", new { area = "Admin" });
        }

        // Unblock user
        public async Task<IActionResult> Unblock(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.IsBlocked = false;
                await userManager.UpdateAsync(user);
                ViewData["Message"] = "User has been unblocked successfully.";
            }

            return RedirectToAction("AllUser", "Support", new { area = "Admin" });
        }

        // Toggle block/unblock status
        [HttpGet("ToggleBlockStatus")]
        public async Task<IActionResult> ToggleBlockStatus(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.IsBlocked = !user.IsBlocked;
                await userManager.UpdateAsync(user);

                TempData["Message"] = user.IsBlocked
                    ? "User has been blocked successfully."
                    : "User has been unblocked successfully.";
            }
            else
            {
                TempData["Message"] = "User not found.";
            }


            return RedirectToAction("AllUser", "Support", new { area = "Admin" });
        }
        [HttpGet]
        public async Task<IActionResult> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return View("user", userManager.Users); 
            }

            var users = await userManager.Users
                .Where(m => m.UserName.Contains(searchTerm) || m.Email.Contains(searchTerm) || m.FirstName.Contains(searchTerm) || m.LastName.Contains(searchTerm))
                .ToListAsync(); 

            return View("user", users);
        }


    }
}
