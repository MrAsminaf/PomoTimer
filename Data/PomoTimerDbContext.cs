using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PomoTimer.Models;

namespace PomoTimer.Data
{
    public class PomoTimerDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<TimeModel> TimeModels { get; set; }

        public PomoTimerDbContext(DbContextOptions<PomoTimerDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}