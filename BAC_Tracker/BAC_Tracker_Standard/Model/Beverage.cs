using System;

namespace BAC_Tracker.Model{


    public class Beverage
    {
        //DateTime startTime = new DateTime(0, 0, 0, 0, 0, 0);
        //DateTime finishTime = new DateTime(0, 0, 0, 0, 0, 0);


        //Beverage tempdrink;

        public string Type { get; set; }
        public double Amount { get; set; }
        public double Completed_percentage { get; set; }

        public BeverageVolume Volume { get; set; }
        public BAC_Tracker.Model.BeverageDetails Details { get; set; }

        public Beverage(string type, double amount, double completed_percentage)
        {

            Type = type;
            Amount = amount;
            Completed_percentage = completed_percentage;
            
            Details = new BAC_Tracker.Model.BeverageDetails("lightbeer");

            

            //second parameter is for the size, and third parameter is the percentage the user has already drank.
            //in the future, instead of these random number, It will be set to the input from the user
            //for example for the percentage already drank, "50" will be replaced to the AndroidButton.ConsumedPercentage()
            Volume = new BeverageVolume(Details, 100, 50);


        }





        /*
        //default values
        DateTime startTime = new DateTime(0, 0, 0, 0, 0, 0);
        DateTime finishTime = new DateTime(0, 0, 0, 0, 0, 0);
        public BeverageDetails details;
        public BeverageVolume volume;

        public static double totalconsumedalcohol = 0;



        public Beverage()
        {

            BeverageDetails details = new BeverageDetails();
            BeverageVolume volume = new BeverageVolume();

            totalconsumedalcohol += volume.ConsumedAlcohol();


        }

        //it will set the starting time when customer clicks on the "Started drinking" button
        public void SetStartTime()
        {
            startTime = DateTime.Now;

        }
        //this function will set the finishing time  when customer clicks on the "finished drinking" button
        public void SetFinishTime()
        {
            finishTime = DateTime.Now;

        }


        //measuring the time that customer finishes his drink.
        public TimeSpan TimeElapsed()
        {

            var spannedHour = finishTime.Hour - startTime.Hour;
            var spannedMinutes = finishTime.Minute - startTime.Minute;
            var spannedMilliseconds = finishTime.Millisecond - startTime.Millisecond;
            var timeSpan = new TimeSpan(spannedHour, spannedMinutes, spannedMilliseconds);
            return timeSpan;

        }
        */

    }
}
