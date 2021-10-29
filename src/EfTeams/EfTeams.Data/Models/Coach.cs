using System.ComponentModel.DataAnnotations;

namespace EfTeams.Data.Models
{
    public class Coach
    {
        [Key]
        public int Id { get; set; }
        public string CoachName { get; set; }
    }

}
