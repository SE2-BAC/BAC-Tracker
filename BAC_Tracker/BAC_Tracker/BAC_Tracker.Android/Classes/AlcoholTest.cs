using System;
using Newtonsoft.Json;

namespace BAC_Tracker.Droid.Classes
{
    public class AlcoholTest
    {
        //Necessary for the table?
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "volume")]
        public float Volume { get; set; }
        [JsonProperty(PropertyName = "finished")]
        public bool Finished { get; set; }

        public AlcoholTest(string newName, float newVolume, bool newState)
        {
            Name = newName;
            Volume = newVolume;
            Finished = newState;
        }
    }
}