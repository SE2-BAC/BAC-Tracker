using System;
using System.Collections.Generic;
using System.Text;

namespace BAC_Tracker.Controller
{
    public class BAC_Controller
    {
        public Model.Person Person { get; set; }
        public Model.Beverage Beverage {get; set; }
        
        // List of events
        // 

        BAC_Controller()
        {
            Person = new Model.Person();
            Beverage = new Model.Beverage();
        }

        public void Calculate_BAC(double existingBAC)
        {
            int genderRate=1;
            if(Person.Gender=="Female") genderRate = 1.13;
            return ((((Beverage.Completed_percentage*7.156655998)/Person.weight)/100)*genderRate)+existingBAC-((time/15)*.003);
        }

        public void Update_BAC(double existingBAC){
            return existingBAC-(0.003);
        }
    }
}
