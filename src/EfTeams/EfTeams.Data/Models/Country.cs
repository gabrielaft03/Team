using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EfTeams.Data.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        public string CountryName { get; set; }
        public ICollection<Team> Teams { get; set; }

    }
}
