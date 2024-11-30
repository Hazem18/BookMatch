using DataAccess.Repository.IRepository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class TeamLeagueRepository : Repository<TeamLeague>, ITeamLeagueRepository
    {
        private readonly ApplicationDbContext dbContext;

        public TeamLeagueRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext=dbContext;
        }
    }
}
