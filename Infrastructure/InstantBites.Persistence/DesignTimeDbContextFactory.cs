using InstantBites.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDBContext>
    {

        public AppDBContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<AppDBContext> optionsBuilder = new();
            optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            return new(optionsBuilder.Options);
        }
    }
}
