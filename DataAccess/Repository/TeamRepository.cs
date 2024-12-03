using DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class TeamRepository : Repository<Team>, ITeamRepository
    {
        private readonly ApplicationDbContext dbContext;
        private DbSet<Team> dbSet;
        public TeamRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext=dbContext;
            dbSet = dbContext.Set<Team>();
        }

        public IEnumerable<Team> GetTeams(Expression<Func<Team, bool>>? filter = null,bool includeStadium = false,bool includeLeagues = false,bool tracked = true)
        {
            IQueryable<Team> query = dbSet;

            // Apply filter if provided
            if (filter != null)
            {
                query = query.Where(filter);
            }

            // Include Stadium navigation property if requested
            if (includeStadium)
            {
                query = query.Include(t => t.Stadium);
            }

            // Include TeamLeagues and Leagues if requested
            if (includeLeagues)
            {
                query = query.Include(t => t.TeamLeagues)
                             .ThenInclude(tl => tl.League);
            }

            // Apply tracking or no-tracking
            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            return query.ToList();
        }

    }
}
