using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class TeamVM
    {
        public int Id { get; set; }
        [Required]
        [MinLength(2,ErrorMessage ="Team name must be greater than 2 letters")]
        public string? Name { get; set; }
        [ValidateNever]
        public string? LogoUrl { get; set; }
        [Required]
        [Display(Name= "Stadium")]
        public int StadiumId { get; set; }
        [Required]
        public List<int> SelectedLeagueIds { get; set; } = new List<int>();
    }
}
