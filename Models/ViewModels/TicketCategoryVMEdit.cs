using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class TicketCategoryVMEdit
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The Name Field Is Required")]
        [MinLength(2, ErrorMessage = "Ticket Category Name Must Be Greater Than 2 Letters")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "The AvailableTickets Field Is Required")]
        [Range(1, 1000000)]
        public int AvailableTickets { get; set; }
        // [ValidateNever]
        [Required(ErrorMessage = "The SectorImage Field Is Required")]
        public string? SectorImage { get; set; }
        [Required(ErrorMessage = "The Price Field Is Required")]
        [Range(1, 10000)]
        public decimal Price { get; set; }
    }
}

