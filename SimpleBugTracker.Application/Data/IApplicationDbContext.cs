using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleBugTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SimpleBugTracker.Application.Data
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; }

        DbSet<Bug> Bugs { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
