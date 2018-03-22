using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverythingFootballDemo.DAL
{
    public class BookingFixture
    {
        public string CompetitionName { get; set; }
        public string HomeTeamName { get; set; }
        public string HomeTeamUrl { get; set; }
        public string AwayTeamName { get; set; }
        public string AwayTeamUrl { get; set; }
        public string FixtureDate { get; set; }
        public string venue { get; set; }
        public int HomeTeamID { get; set; }
        public int AwayTeamID { get; set; }
    }
}
