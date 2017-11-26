using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BAC_Tracker.Model
{
    public class Festivity
    {
        [JsonProperty(PropertyName = "creationTimeStamp")]
        public DateTime Creation_TimeStamp;
        [JsonProperty(PropertyName = "festivityID")]
        public int FestivityID { get; private set; }
        [JsonProperty(PropertyName = "currentBAC")]
        public double Current_BAC { get; set; }
        [JsonProperty(PropertyName = "maxBAC")]
        public double Max_BAC { get; set; }
        //Required for Azure table internal stuff
        //Uses hexidecimal ID string internally. Can't really change that.
        public string Id;

        //Ignore serialization, constructed in-app.
        [JsonIgnore]
        public List<Beverage> Beverage_List { get; set; } 

        //For exsisting Festivities
        public Festivity(int id, DateTime timestamp, double current_BAC, double max_BAC, List<Beverage> beverage_list)
        {
            FestivityID = id;
            Creation_TimeStamp = timestamp;
            Max_BAC = max_BAC;
            Current_BAC = current_BAC;
            Beverage_List = beverage_list;
            // Or we built the list here from the SQLite table


        }
        //For adding a new Festivity. Assuming new ID is handled by Controller
        public Festivity(int id)
        {
            FestivityID = id;
            Creation_TimeStamp = DateTime.Now;
            Max_BAC = 0;
            Current_BAC = 0;
            Beverage_List = new List<Beverage>();
            //Assign ID by checking against SQLite table for next free int ID
        }

        //Necessary for deserializing the JSon correctly.
        [JsonConstructor]
        public Festivity(int festivityID, double currentBAC, double maxBAC, DateTime creationTimeStamp)
        {
            FestivityID = festivityID;
            Current_BAC = currentBAC;
            Max_BAC = maxBAC;
            Creation_TimeStamp = creationTimeStamp;
        }

    }
}
