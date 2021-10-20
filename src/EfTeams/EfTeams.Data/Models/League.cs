using System.Collections.Generic;

namespace EfTeams.Data.Models
{
    public class League
    {
        public int Id { get; set; }
        public string LeagueName { get; set; }
        //public virtual ICollection<TeamLeague> TeamLeagues { get; set; }
    }
}
