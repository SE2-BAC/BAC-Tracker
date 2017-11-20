using System;

namespace BAC_Tracker.Model{


    public class Beverage
    {
        string model;
        double volume;

        public double Percentage_consumed { get; set; }

        public string Model{
            get => model;
            set
            {
                model = value;
                DeterminePercentage();
            }
        }
        public double Alcohol_percentage { get; set; }

        public string Container { get; set; }
        public double Volume {
            get => volume;
            set
            {
                volume = value;
                DetermineVolume();
            }
        }  


        
        public Beverage(string model,  double percentage_consumed, string container)
        {
            Model = model;
            Container = container;            
            Percentage_consumed = percentage_consumed;
        }


        public double TotalConsumedAlcohol()
        {
            return Alcohol_percentage * Volume * Percentage_consumed;
        }

        public void DeterminePercentage()
        {

            switch (Model)
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
            switch (Container)
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

