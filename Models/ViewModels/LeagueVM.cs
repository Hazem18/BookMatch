using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class LeagueVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The Name Field Is Required")]
        [MinLength(2,ErrorMessage = "League Name Must Be Greater Than 2 Letters")]
        [Display(Name = "Name")]

        public string? Name { get; set; }
       
        public string? LogoUrl { get; set; }
        [Required(ErrorMessage = "The Logo Field Is Required")]
        public IFormFile? Logo { get; set; } 
        [Required(ErrorMessage = "The Description Field Is Required")]
        [MinLength(10, ErrorMessage = "League Description Must Be Greater Than 10 Letters")]
        [Display(Name = "Description")]

        public string? Description { get; set; }

    }
}
