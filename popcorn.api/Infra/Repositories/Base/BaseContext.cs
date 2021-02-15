using Domain.Entities;
using Infra.Repositories.Map;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using prmToolkit.NotificationPattern;
using System.Configuration;

namespace Infra.Repositories.Base
{
    public partial class BaseContext : DbContext
    {
        public IConfiguration Configuration { get; }
        public BaseContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public DbSet<User> User { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Session> Session { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(Configuration.GetConnectionString("MySql"));
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Ignore<Notification>();

            modelBuilder.ApplyConfiguration(new MapUser());
            modelBuilder.ApplyConfiguration(new MapRoom());
            modelBuilder.ApplyConfiguration(new MapMovie());
            modelBuilder.ApplyConfiguration(new MapSession());
            base.OnModelCreating(modelBuilder);
        }
    }
}
