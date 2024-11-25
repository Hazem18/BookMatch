using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Match
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int StadiumId { get; set; }
        public Stadium Stadium { get; set; } = null!;

        public int TeamAId { get; set; }
        public Team TeamA { get; set; } = null!;

        public int TeamBId { get; set; }
        public Team TeamB { get; set; } = null!;

        public int LeagueId { get; set; }
        public League League { get; set; } = null!;
        public MatchStatus Status { get; set; }
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
