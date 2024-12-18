using DataAccess;
using DataAccess.Repository.IRepository;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.UI.Services;
using Utility;
using Stripe;

namespace BookMatch
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            builder.Services.AddDbContext<ApplicationDbContext>(
                    options => options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection"))
            );


            //.AddDefaultUI().AddDefaultTokenProviders();
            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

           // builder.Services.AddTransient<IEmailSender, EmailSender>();


            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped(typeof(ILeagueRepository), typeof(LeagueRepository));
            builder.Services.AddScoped(typeof(ITeamRepository), typeof(TeamRepository));
            builder.Services.AddScoped(typeof(IMatchRepository), typeof(MatchRepository));
            builder.Services.AddScoped(typeof(ITicketRepository), typeof(TicketRepository));
            builder.Services.AddScoped(typeof(ITicketCategoryRepository), typeof(TicketCategoryRepository));
            builder.Services.AddScoped(typeof(IStadiumRepository), typeof(StadiumRepository));

            builder.Services.AddScoped(typeof(IUserTicketRepository), typeof(UserTicketRepository));
            builder.Services.AddScoped(typeof(ITeamLeagueRepository), typeof(TeamLeagueRepository));
            builder.Services.AddScoped(typeof(ITicketPurchaseRepository), typeof(TicketPurchaseRepository));
            builder.Services.AddScoped(typeof(IOldMatchRepository), typeof(OldMatchRepository));
            builder.Services.AddScoped(typeof(ISupportTicketRepository), typeof(SupportTicketRepository));

            builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
            StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.MapRazorPages();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{area=User}/{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
