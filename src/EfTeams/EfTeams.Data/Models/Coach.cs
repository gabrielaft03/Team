using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EfTeams.Data.Models
{
    public class Coach
    {
        [Key]
        public int Id { get; set; }
        public string CoachName { get; set; }
        public ICollection<Team> Teams { get; set; }

    }

}
