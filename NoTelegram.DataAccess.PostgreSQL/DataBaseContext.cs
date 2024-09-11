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
        public DbSet<ChatsEntity> Chats { get; set; }
        public DbSet<MessagesEntity> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsersConfiguraction());
            modelBuilder.ApplyConfiguration(new ChatsConfiguraction());
            modelBuilder.ApplyConfiguration(new MessagesConfiguraction());

            base.OnModelCreating(modelBuilder);
        }
    }
}