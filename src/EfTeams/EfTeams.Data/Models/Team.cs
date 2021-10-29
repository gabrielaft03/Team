using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace EfTeams.Data.Models
{
    public partial class Team
    {
        public int Id { get; set; }
        public string TeamName { get; set; }
        public string Abbreviation { get; set; }
        public int CoachId { get; set; }
        public int CountryId { get; set; }
    }

    public partial class Team
    {
        //Relations coach id 
        public Coach Coach { get; set; }
        //Relations country id 
        public Country Country { get; set; }
        public ICollection<Player> Players { get; set; }
    }

    public class TeamEntityConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.Property(x => x.TeamName).HasColumnType("nvarchar(50)");
            builder.Property(x => x.Abbreviation).HasColumnType("nvarchar(7)");
      
        }
    }
}
