using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverythingFootballDemo.DAL {
    public class DefaultHomeVenue
    {
        public int dbid { get; set; }
        public int capacity { get; set; }
        public string name { get; set; }
        public Geolocation geolocation { get; set; }
    }

    public class Player
    {
        public string name { get; set; }
        public int? weight { get; set; }
        public string gender { get; set; }
        public int? number { get; set; }
        public int? height { get; set; }
        public double? dateOfBirth { get; set; }
        public int dbid { get; set; }
        public string position { get; set; }
        public string shortName { get; set; }
    }

    public class Team
    {
        public bool showLeagueTables { get; set; }
        public string name { get; set; }
        public DefaultHomeVenue defaultHomeVenue { get; set; }
        public int dbid { get; set; }
        public bool showAssistStats { get; set; }
        public bool showCardStats { get; set; }
        public bool isNational { get; set; }
        public List<Player> players { get; set; }
        public string shirtUrl { get; set; }
        public bool showGoalStats { get; set; }
        public string badgeName { get; set; }
        public string shortName { get; set; }
        public string shortCode { get; set; }
    }
}