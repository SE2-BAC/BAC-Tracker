using System;
using Newtonsoft.Json;

namespace BAC_Tracker.Model{


    public class Beverage
    {
        string model;
        double volume;
        int festivityID;

        [JsonProperty(PropertyName = "percentageConsumed")]
        public double Percentage_consumed;
        [JsonProperty(PropertyName = "alcoholPercentage")]
        public double Alcohol_percentage;
        [JsonProperty(PropertyName = "container")]
        public string Container;
        //Required for Azure table internal stuff
        public string Id;

        [JsonProperty(PropertyName = "volume")]
        public double Volume {
            get => volume;
            set
            {
                volume = value;
                DetermineVolume();
            }
        }
        [JsonProperty(PropertyName = "model")]
        public string Model
        {
            get => model;
            set
            {
                model = value;
                DeterminePercentage();
            }
        }
        [JsonProperty(PropertyName = "festivityID")]
        public int FestivityID
        {
            get => festivityID;
            private set
            {
                festivityID = value;
            }
        }



        public Beverage(string model,  double percentage_consumed, string container, int festivity)
        {
            Model = model;
            Container = container;
            DetermineVolume();
            Percentage_consumed = percentage_consumed;
            FestivityID = festivity;
        }

        //Necessary to deserialize the JSon correctly.
        [JsonConstructor]
        public Beverage(string model, double percentageConsumed, double alcoholPercentage, string container, double volume, int festivityID)
        {
            Model = model;
            Percentage_consumed = percentageConsumed;
            Alcohol_percentage = alcoholPercentage;
            Container = container;
            Volume = volume;
            FestivityID = festivityID;
        }


        public double TotalConsumedAlcohol() => Alcohol_percentage * Volume * Percentage_consumed;
        

        public void DeterminePercentage()
        {

            switch (Model.ToLower())
            {
                case "lightbeer":
                    Alcohol_percentage = 0.05;
                    break;
                case "liquor":
                    Alcohol_percentage = 0.45;
                    break;
                case "whiskey":
                    Alcohol_percentage = 0.45;
                    break;
                case "gin":
                    Alcohol_percentage = 0.40;
                    break;
                case "vodka":
                    Alcohol_percentage = 0.40;
                    break;
                case "red wine":
                    Alcohol_percentage = 0.14;
                    break;
                case "white wine":
                    Alcohol_percentage = 0.18;
                    break;
            }
        }

        public void DetermineVolume()
        {
            switch (Container.ToLower())
            {
                case "wine":
                    Volume = 5;
                    break;
                case "whiskey":
                    Volume = 6;
                    break;
                case "pint":
                    Volume = 16;
                    break;
                case "shot":
                    Volume = 1.5;
                    break;
                case "can/bottle":
                    Volume = 12;
                    break;

            }  
        }

    }
}

