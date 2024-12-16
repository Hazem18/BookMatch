using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long NationalId { get; set; }
        public string Address { get; set; }
        public string? ProfilePicture {  get; set; }
        public ICollection<UserTicket> UserTickets { get; set; } = new List<UserTicket>();

        public ICollection<SupportTicket> SupportTickets { get; set; } = new List<SupportTicket>();

    }
}
