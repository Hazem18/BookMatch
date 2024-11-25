﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
        [Required]
        [MinLength(2,ErrorMessage = "League name must be greater than 2 letters")]
        public string? Name { get; set; }
        [ValidateNever]
        public string? LogoUrl { get; set; }
        [Required]
        [MinLength(10, ErrorMessage = "League Description must be greater than 10 letters")]
        public string? Description { get; set; }

    }
}
