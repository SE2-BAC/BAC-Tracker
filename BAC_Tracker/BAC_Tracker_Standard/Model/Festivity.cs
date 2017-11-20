using System;
using System.Collections.Generic;

namespace BAC_Tracker.Model
{
    public class Festivity
    {
        public DateTime Creation_TimeStamp;
        public int ID { get; }
        public double Current_BAC { get; set; }
        public double Max_BAC { get; set; }

        public List<Beverage> Beverage_List { get; set; } 

        //For exsisting Festivities
        public Festivity(int id, DateTime timestamp, double current_BAC, double max_BAC, List<Beverage> beverage_list)
        {
            ID = id;
            Creation_TimeStamp = timestamp;
            Max_BAC = max_BAC;
            Current_BAC = current_BAC;
            Beverage_List = beverage_list;
            // Or we built the list here from the SQLite table


        }
        //For adding a new Festivity. Assuming new ID is handled by Controller
        public Festivity(int id)
        {
            ID = id;
            Creation_TimeStamp = DateTime.Now;
            Max_BAC = 0;
            Current_BAC = 0;
            Beverage_List = new List<Beverage>();
            //Assign ID by checking against SQLite table for next free int ID
        }

    }
}
