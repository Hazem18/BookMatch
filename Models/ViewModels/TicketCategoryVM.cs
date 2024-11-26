using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class TicketCategoryVM
    {
        public int Id { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Ticket Category name must be greater than 2 letters")]
        public string? Name { get; set; }
        [Required]
        [Range(1,1000000)]
        public int AvailableTickets { get; set; }
        [ValidateNever]
        public string? SectorImage { get; set; }
        [Required]
        [Range(1,10000)]
        public decimal Price { get; set; }
    }
}
