using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using SimpleBugTracker.Domain.Entities;
using SimpleBugTracker.Application.Data;

namespace SimpleBugTracker.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Bug> Bugs { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
