using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class StadiumVM
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3,ErrorMessage = "Stadium name must be greater than 3 letters")]
        public string? Name { get; set; }
        [Required]
        [Range(100,100000)]
        public int Capacity { get; set; }
        [ValidateNever]
        public string? LocationUrl { get; set; }
        [Required]
        [MinLength(10, ErrorMessage = "Stadium Description must be greater than 10 letters")]
        public string? Description { get; set; }

    }
}
