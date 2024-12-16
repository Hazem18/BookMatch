using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class OldMatch
    {
        public int Id { get; set; }
        public DateTime MatchDate { get; set; }
        public string StadiumName { get; set; } = null!;
        public string TeamHomeName { get; set; } = null!;
        public string TeamAwayName { get; set; } = null!;
        public string LeagueName { get; set; } = null!;

        public long Users { get; set; }
        public double TolalSales { get; set; }
    }
}
