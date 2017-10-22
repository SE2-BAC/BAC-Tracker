using System;


namespace BAC_Tracker.Model
{

    public class Person
    {
        //Lower case string is a property. Upcase is a class that we do not need
        //Change properties to get set
        public string Gender { get; set; }
        public double Weight { get; set; }

        //Will put list of drinks in the Event class.

        public Person(string gender, double weight)
        {
            Gender = gender;
            Weight = weight;

        }


    }
}
