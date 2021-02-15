using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Repositories.Map
{
    public class MapRoom : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            #region Configuration
            builder.ToTable("Room");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(150).IsRequired();
            builder.Property(x => x.Chairs).IsRequired();
            #endregion Configuration

            #region InitialData
            builder.HasData(
                new Room("Sala 1", 50),
                new Room("Sala 2", 50),
                new Room("Sala 3", 50),
                new Room("Sala 4", 50),
                new Room("Sala 5", 50),
                new Room("Sala 6", 50),
                new Room("Sala 7", 50),
                new Room("Sala 8", 50)
                );
            #endregion InitialData
        }
    }
}
