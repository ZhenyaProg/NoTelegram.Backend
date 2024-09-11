using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoTelegram.DataAccess.PostgreSQL.Entities;

namespace NoTelegram.DataAccess.PostgreSQL.Configuractions
{
    public class MessagesConfiguraction : IEntityTypeConfiguration<MessagesEntity>
    {
        public void Configure(EntityTypeBuilder<MessagesEntity> builder)
        {
            builder.HasKey(message => message.MessageId);

            builder
                .HasOne(message => message.Chat)
                .WithMany(chat => chat.Messages)
                .HasForeignKey(message => message.ChatId);
        }
    }
}