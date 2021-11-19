using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EfTeams.Data.Models
{
    public partial class TeamLeague
    {
        public int Id { get; set; }
        // [Key]
        public int TeamId { get; set; }
        // [Key]
        public int LeagueId { get; set; }

    }

    
    public partial class TeamLeague
    {
        public Team Team { get; set; }
        public League League { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class TeamLeagueEntityConfiguration : IEntityTypeConfiguration<TeamLeague>
    {
        public void Configure(EntityTypeBuilder<TeamLeague> builder)
        {

        }
    }
}
