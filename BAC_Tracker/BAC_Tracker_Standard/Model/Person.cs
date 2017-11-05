using System;


namespace BAC_Tracker.Model
{

    public class Person
    {
        //Lower case string is a property. Upcase is a class that we do not need
        //Change properties to get set
        //NM: Am considering changing string Gender to bool isMale since it'll be a switch value.
        public bool Gender { get; set; }
        public double Weight { get; set; }

        //Will put list of drinks in the Event class.

        public Person(bool gender, double weight)
        {
            Gender = gender;
            Weight = weight;

        }


    }
}
