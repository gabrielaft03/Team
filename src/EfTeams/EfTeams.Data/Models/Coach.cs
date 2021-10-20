using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace EfTeams.Data.Models
{
    public class Coach
    {
        public int Id { get; set; }
        public string CoachName { get; set; }

        //public int TeamId { get; set; }
        //public Team Team { get; set; }
        //public ICollection<Team> Teams { get; set; }

    }

    //public class CoachEntityConfiguration : IEntityTypeConfiguration<Coach>
    //{
    //    public void Configure(EntityTypeBuilder<Coach> builder)
    //    {
    //        builder.Property(p => p.CoachName).HasColumnType("nvarchar(20)");

    //        builder.HasOne(x => x.Team)
    //               .WithOne(x => x.Coach)
    //               .HasForeignKey<Coach>(x => x.TeamId);
    //    }
    //}
}
