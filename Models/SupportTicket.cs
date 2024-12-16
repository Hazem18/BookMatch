﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class SupportTicket
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsResolved { get; set; } = false;

        public string UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;
    }
}
