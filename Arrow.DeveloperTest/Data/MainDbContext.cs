using Arrow.DeveloperTest.Types;
using Microsoft.EntityFrameworkCore;

namespace Arrow.DeveloperTest.Data
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
    }
}
