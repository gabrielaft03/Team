using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EfTeams.Data.Models
{
    public class League
    {
        [Key]
        public int Id { get; set; }
        public string LeagueName { get; set; }
    }
}
