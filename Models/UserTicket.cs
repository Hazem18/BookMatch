using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class UserTicket
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;

        public int TicketId { get; set; }
        public Ticket Ticket { get; set; } = null!;

        public DateTime BookingDate { get; set; }
    }
}
