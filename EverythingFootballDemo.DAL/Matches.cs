using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverythingFootballDemo.DAL
{
    public class Matches
    {
        public int? awayGoals { get; set; }
        public double? currentStateStart { get; set; }
        public object nextState { get; set; }
        public double? start { get; set; }
        public bool? goToExtraTime { get; set; }
        public Season season { get; set; }
        public HomeTeam homeTeam { get; set; }
        public object aggregateScore { get; set; }
        public Venue venue { get; set; }
        public Competition competition { get; set; }
        public bool? limitedCoverage { get; set; }
        public Dismissals dismissals { get; set; }
        public AwayTeam awayTeam { get; set; }
        public int? homeGoals { get; set; }
        public PenaltyShootout penaltyShootout { get; set; }
        public bool? extraTimeHasHappened { get; set; }
        public int? dbid { get; set; }
        public bool? isResult { get; set; }
        public Outcome outcome { get; set; }
        public Round round { get; set; }
        public int? currentState { get; set; }
    }
    public class Season
    {
        public int? dbid { get; set; }
        public string name { get; set; }
    }

    public class HomeTeam
    {
        public string shirtUrl { get; set; }
        public string name { get; set; }
        public int dbid { get; set; }
        public bool isNational { get; set; }
        public string shortName { get; set; }
        public string shortCode { get; set; }
    }

    public class Geolocation
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class Venue
    {
        public int dbid { get; set; }
        public int capacity { get; set; }
        public string name { get; set; }
        public Geolocation geolocation { get; set; }
    }

    public class Competition
    {
        public int ordering { get; set; }
        public int dbid { get; set; }
        public string name { get; set; }
        public string flagUrl { get; set; }
    }

    public class Dismissals
    {
        public int home { get; set; }
        public int away { get; set; }
    }

    public class AwayTeam
    {
        public string shirtUrl { get; set; }
        public string name { get; set; }
        public int dbid { get; set; }
        public bool isNational { get; set; }
        public string shortName { get; set; }
        public string shortCode { get; set; }
    }

    public class PenaltyShootout
    {
    }

    public class Outcome
    {
        public string winner { get; set; }
        public string type { get; set; }
        public bool afterExtraTime { get; set; }
    }

    public class Round
    {
        public int dbid { get; set; }
        public bool hasLeagueTable { get; set; }
        public string name { get; set; }
    }

    //Fixtures URL:
    //https://api.crowdscores.com/v1/matches?from=2018-01-31T12:00:00-03:00&to=2018-02-01T12:00:00-04:00&competition_id=2&competition_id=42&api_key=7e07e4528e1d4949a7baddc98ec4adde
}
