using Microsoft.EntityFrameworkCore;

namespace ServerSide
{
    public class AppDbContext:DbContext
    {
       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=STHQ0118-16;Initial Catalog=Task;User ID=admin;Password=admin;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
        public DbSet<User> Users { get; set; }
    }
}
