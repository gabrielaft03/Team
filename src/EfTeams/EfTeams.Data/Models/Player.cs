using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfTeams.Data.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string PlayerName { get; set; }
        public string Position { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }
    }

    public class PlayerEntityConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            //builder.HasOne(x => x.Team)
            //       .WithMany(x => x.Players)
            //       .HasForeignKey(x => x.TeamId);

            //nuevo
            builder.HasOne(x => x.Team)
                     .WithMany(x => x.Players)
                     .HasForeignKey(x => x.TeamId);
        }
    }
}
