using System;
namespace BAC_Tracker.Model
{

public class BeverageVolume
    {
        public double Amount { get; set; }
        public double Volume_percentage_completed { get; set; }

        //NM: There is no GET or SET. It will infer both of them to be private.
        //NM: Added Get Set
        //NM: Consider taking this object out of this class. Handle the math in Beverage itself.
        public BAC_Tracker.Model.BeverageDetails DetailsForTotalConsumedAlcohol { get; set; }


        public BeverageVolume(BAC_Tracker.Model.BeverageDetails beverage, double amount, double volume_percentage_completed)
        {
            Amount = amount;
            Volume_percentage_completed = volume_percentage_completed;

            //This stat
            DetailsForTotalConsumedAlcohol = beverage;

            //TotalConsumedAlcohol(x);
        }

        //NM: Consider moving this method to the Beverage class
        public double TotalConsumedAlcohol()
        {
           
            return DetailsForTotalConsumedAlcohol.Alcohol_percentage*Amount*Volume_percentage_completed;
        }


        /*
        public double TotalConsumedAlcohol(Beverage x)
        {


            if (x.Type.ToLower().Equals("soft drink"))
            {
                return Amount * Volume_percentage_completed * 0.05;
            }
            else if (x.Type.ToLower().Equals("hard drink"))
            {
                return Amount * Volume_percentage_completed * 0.45;
            }
            else
            {
                return 0;
            }
        }
        */



        /*
        public BeverageVolume()
        {
            SetContainer();
            ConsumedAlcohol();


        }


        public double ConsumedAlcohol()
        {
            return (container * volume_percentage_completed) * details.alcoholpercentage;

        }
        //the value of size will be taken from user 
        //ml or Oz (70ml-50ml-oneshot)
        public double SetContainer()
        {

            return 10;
        }
        */
    }
}
