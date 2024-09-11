using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoTelegram.DataAccess.PostgreSQL.Entities;

namespace NoTelegram.DataAccess.PostgreSQL.Configuractions
{
    public class ChatsConfiguraction : IEntityTypeConfiguration<ChatsEntity>
    {
        public void Configure(EntityTypeBuilder<ChatsEntity> builder)
        {
            builder.HasKey(chat => chat.ChatId);

            builder
                .HasMany(chat => chat.Interlocutors)
                .WithMany(user => user.Chats);

            builder
                .HasMany(chat => chat.Messages)
                .WithOne(message => message.Chat)
                .HasForeignKey(message => message.ChatId);

            builder
                .Property(chat => chat.ChatAccess)
                .HasConversion<string>()
                .HasMaxLength(10);

            builder
                .Property(chat => chat.Type)
                .HasConversion<string>()
                .HasMaxLength(10);
        }
    }
}