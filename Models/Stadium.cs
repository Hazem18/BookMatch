using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Stadium
    {
        public int Id { get; set; } 
        public string? Name { get; set; }
        public int Capacity { get; set; }
        public string? LocationUrl { get; set; }
        public string? Description { get; set; }
        public ICollection<Team> Teams { get; set; } = new List<Team>();
        public ICollection<Match> Matches { get; set; } = new List<Match>();
    }
}
