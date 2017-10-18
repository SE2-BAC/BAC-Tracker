using System;

namespace BAC_Tracker.Model{


public class Beverage 
{
  
    public BeverageVolume Volume {get;set;}
     public BeverageDetails Details {get;set;}

 public Beverage(){
        Volume = new BeverageVolume();
        Details = new BeverageDetails();
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
