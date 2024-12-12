using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TicketPurchase
    {
        public int Id { get; set; }  

        public string UserId { get; set; }  
        public string ApplicationName {  get; set; }
        public ApplicationUser User { get; set; }  

        public int TicketId { get; set; }  
        public Ticket Ticket { get; set; } 

        public string SeatNumber { get; set; }  

        public decimal Price { get; set; }  

        public DateTime PurchaseDate { get; set; } = DateTime.Now;

        public string TicketCategoryName { get; set; }  
        public string PaymentStatus { get; set; }

        public string StadiumName { get; set; } = null!;
        public string TeamAName { get; set; } = null!;
        public string TeamBName { get; set; } = null!;

        public DateTime DateMatch {  get; set; } = DateTime.Now;
    }
}
