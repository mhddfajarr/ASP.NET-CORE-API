using Latihan.Helper;
using Latihan.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Latihan.Context
{
        public class MyContext : DbContext
        {
            public MyContext(DbContextOptions<MyContext> options) : base(options) { }
            public DbSet<Account> Accounts { get; set; }
            public DbSet<Education> Education { get; set; }
            public DbSet<Employee> Employees { get; set; }
            public DbSet<Profiling> Profilings { get; set; }
            public DbSet<University> Universities { get; set; }
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
                modelBuilder.Entity<Education>()
                    .Property(e => e.Degree)
                    .HasConversion(new EnumToStringConverter<Degree>());
            }
    }
}
