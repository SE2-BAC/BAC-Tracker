using System;

public class BeverageDetails
{
    double alcohol_percentage;
    
    

    public BeverageDetails(Beverage x){




}

    public void SetAlcoholPercentage(double x)
    {
        alcohol_percentage = x;
    }

    /*
    public String make;
    public String model;
    public double alcoholpercentage;
    public bool type;


	public BeverageDetails()
	{
        DetermineType();
        DetermineAlcoholPercentage();
        
    }

    //determining alcohol percentage according to its name
    //these values are just default(random), they will change according to the their makes and models once we have the database or 
    //i am just going to implement them over here by hard-coding for each different type of make and model.
    public double DetermineAlcoholPercentage()
    {
        if (type.Equals("soft drink"))
        {
            alcoholpercentage = 5;

        }

        else if (type.Equals("hard drink"))
        {
            alcoholpercentage = 40;
        }

        return alcoholpercentage;
    }


    //setting the name of beverage from the user
    public String SettingMake(String make)
    {
        this.make = make;
        return make;

    }

    //setting the model of beverage from the user
    public String SettingModel(String model)
    {
        this.model = model;
        return model;

    }


    //determining the type of drink (soft drink or hard drink?)
    public String DetermineType()
    {
        // illustrating an example
        if (make.ToLower().Equals("Light Beer"))
        {
            return "soft drink";
        }

        else if (make.ToLower().Equals("Whiskey"))
        {
            return "hard drink";
        }

        else return "its not a drink";

    }
    
    public void Update_Model(string usersends) {
        make = usersends
    }
    */
}
