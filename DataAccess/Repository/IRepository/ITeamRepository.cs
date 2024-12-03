using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface ITeamRepository : IRepository<Team> 
    {
        public IEnumerable<Team> GetTeams(Expression<Func<Team, bool>>? filter = null,bool includeStadium = false,bool includeLeagues = false,bool tracked = true);
    }
}
