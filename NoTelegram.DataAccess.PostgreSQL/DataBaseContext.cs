using Microsoft.EntityFrameworkCore;
using NoTelegram.DataAccess.PostgreSQL.Configuractions;
using NoTelegram.DataAccess.PostgreSQL.Entities;

namespace NoTelegram.DataAccess.PostgreSQL
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
            
        }

        public DbSet<UsersEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsersConfiguraction());

            base.OnModelCreating(modelBuilder);
        }
    }
}