using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TicketCategory
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int AvailableTickets { get; set; }
        public string? SectorImage {  get; set; }
        public decimal Price { get; set; }
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
