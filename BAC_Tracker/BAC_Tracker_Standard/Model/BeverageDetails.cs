using System;
namespace BAC_Tracker.Model
{
    public class BeverageDetails
    {
        public double Alcohol_percentage { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }

        //NM: I am thinking that we get rid of Make/Model. Just do Model. ex Light Beer, Vodka, Red Wine...
        public BeverageDetails(string make, string model)
        {
            Make = make;
            Model = model;
            DeterminePercentage(Make);
        }


        public void DeterminePercentage(string make)
        {
            //NM: Consider changing this to a switch-case. Easier to read. 
            //NM: No need to have ToLower if we're controlling the selection from a list.
            if (make.ToLower().Equals("liquor"))
            {
                Alcohol_percentage = 0.45;
            }
            else if (make.ToLower().Equals("beer"))
            {
                Alcohol_percentage = 0.05;

            }
            else if (make.ToLower().Equals("whiskey"))
            {
                Alcohol_percentage = 0.45;
            }
            else if (make.ToLower().Equals("gin"))
            {
                Alcohol_percentage = 0.40;
            }
            else if (make.ToLower().Equals("vodka"))
            {
                Alcohol_percentage = 0.40;
            }


        }



        //NM: Why is a BeverageDetails constructor taking Beverage as an argument?
        public BeverageDetails(Beverage x)
        {

        }

        /*
        public void SetAlcoholPercentage(double x)
        {
            Alcohol_percentage = x;
        }
        */
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
    }    */
    }
}
