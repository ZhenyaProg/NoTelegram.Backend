using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoTelegram.DataAccess.PostgreSQL.Entities;

namespace NoTelegram.DataAccess.PostgreSQL.Configuractions
{
    public class UsersConfiguraction : IEntityTypeConfiguration<UsersEntity>
    {
        public void Configure(EntityTypeBuilder<UsersEntity> builder)
        {
            builder.HasKey(user => user.SecurityId);

            builder
                .HasMany(user => user.Chats)
                .WithMany(chat => chat.Interlocutors);
        }
    }
}