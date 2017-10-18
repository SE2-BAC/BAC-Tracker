using System;

public class BeverageVolume
{
    public string container;
    public double volume_percentage_completed;
    
    //remaining_stomach_volume?? 

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
}
