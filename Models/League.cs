using System.Text.RegularExpressions;

namespace Models
{
    public class League
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LogoUrl { get; set; }
        public string? Description { get; set; }
        public ICollection<Match> Matches { get; set; } = new List<Match>();
        public ICollection<TeamLeague> TeamLeagues { get; set; } = new List<TeamLeague>();
    }
}
