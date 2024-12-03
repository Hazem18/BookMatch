using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class MatchVM
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="Date")]
        public DateTime DateTime { get; set; }
        [Required]
        [Display(Name = "Stadium")]
        public int StadiumId { get; set; }
        [Required]
        [Display(Name = "Team Home")]
        public int TeamAId { get; set; }
        [Required]
        [Display(Name = "Team Away")]
        public int TeamBId { get; set; }
        [Required]
        [Display(Name = "League")]
        public int LeagueId { get; set; }
        [ValidateNever]
        public MatchStatus Status { get; set; }
    }
}
