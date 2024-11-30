using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Models
{
    public class Team
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LogoUrl { get; set; }
        public int StadiumId { get; set; } 
        public Stadium Stadium { get; set; } = null!;
        public ICollection<Match> Matches { get; set; } = new List<Match>();
        public ICollection<TeamLeague> TeamLeagues { get; set; } = new List<TeamLeague>();
    }
}
