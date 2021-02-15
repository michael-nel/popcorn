using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Repositories.Map
{
    public class MapSession : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable("Session");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.SessionStart).IsRequired();
            builder.Property(x => x.SessionEnd).IsRequired();
            builder.Property(x => x.TicketValue).IsRequired();
            builder.Property(x => x.Animation).IsRequired();
            builder.Property(x => x.Audio).IsRequired();
            builder.Property(u => u.MovieId).IsRequired();
            builder.Property(u => u.RoomId).IsRequired();
        }
    }
}
