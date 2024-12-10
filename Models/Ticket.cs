using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public Match Match { get; set; } = null!;
        public int TicketCategoryId { get; set; }
        public TicketCategory TicketCategory { get; set; } = null!;
        public string? SeatNumber { get; set; }  // Unique for each match

        public ICollection<UserTicket> UserTickets { get; set; } = new List<UserTicket>();
        public ICollection<TicketPurchase> TicketPurchases { get; set; } = new List<TicketPurchase>();

    }
}
