using InstantBites.Domain.Entites;
using InstantBites.Domain.Entites.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Repositories
{
    public interface IRepository<T> where T : class,BaseEntity
    {
        DbSet<T> Table { get; }
    }
  
}
