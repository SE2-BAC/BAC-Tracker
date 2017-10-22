using System;

public class BeverageVolume
{
    public double amount;
    public double volume_percentage_completed;
    
    public BeverageVolume(Beverage x)
    {
        this.amount = x.amount;
        this.volume_percentage_completed = x.completed_percentage;

        TotalConsumedAlcohol(x);
    }


    public double TotalConsumedAlcohol(Beverage x)
    {
        

        if (x.type.ToLower().Equals("soft drink"))
        {
            return amount * volume_percentage_completed * 0.05;
        }
        else if (x.type.ToLower().Equals("hard drink"))
        {
            return amount * volume_percentage_completed * 0.45;
        }
        else
        {
            return 0;
        }
    }




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
