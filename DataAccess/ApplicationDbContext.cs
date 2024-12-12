using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Net.Sockets;

namespace DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
       : base(options)
        {
        }
        public DbSet<Match> Matches { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Stadium> Stadiums { get; set; }
        public DbSet<TicketCategory> TicketCategories { get; set; }
        public DbSet<TeamLeague> TeamLeagues { get; set; }
        public DbSet<UserTicket> userTickets { get; set; }
        public DbSet<TicketPurchase> ticketPurchases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Match>()
                    .HasOne(m => m.TeamA)
                    .WithMany()
                    .HasForeignKey(m => m.TeamAId)
                    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Match>()
                   .HasOne(m => m.TeamB)
                   .WithMany()
                   .HasForeignKey(m => m.TeamBId)
                   .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
              .HasIndex(t => new { t.SeatNumber, t.MatchId })
              .IsUnique();

            modelBuilder.Entity<TicketCategory>()
               .Property(t => t.Price)
               .HasColumnType("decimal(10,2)");

            var Admin = new IdentityRole("Admin");
            Admin.NormalizedName = "Admin";

            var User = new IdentityRole("User");
            User.NormalizedName = "User";

            modelBuilder.Entity<IdentityRole>().HasData(Admin,User);

            ///
            modelBuilder.Entity<TeamLeague>()
           .HasKey(tl => new { tl.TeamId, tl.LeagueId }); // Composite primary key

            modelBuilder.Entity<TeamLeague>()
                .HasOne(tl => tl.Team)
                .WithMany(t => t.TeamLeagues) 
                .HasForeignKey(tl => tl.TeamId);

            modelBuilder.Entity<TeamLeague>()
                .HasOne(tl => tl.League)
                .WithMany(l => l.TeamLeagues) 
                .HasForeignKey(tl => tl.LeagueId);
            ///


            ///
            modelBuilder.Entity<UserTicket>()
              .HasKey(ut => new { ut.UserId, ut.TicketId }); // Composite primary key

            modelBuilder.Entity<UserTicket>()
                .HasOne(ut => ut.User)
                .WithMany(u => u.UserTickets)
                .HasForeignKey(ut => ut.UserId);

            modelBuilder.Entity<UserTicket>()
                .HasOne(ut => ut.Ticket)
                .WithMany(t => t.UserTickets)
                .HasForeignKey(ut => ut.TicketId);
            ///
        }
    }
}
