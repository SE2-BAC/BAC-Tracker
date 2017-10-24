using System;
using System.Collections.Generic;
using System.Text;

namespace BAC_Tracker.Controller
{
    public class BAC_Controller
    {
        public Model.Person Person { get; set; }
        //public Model.Beverage Beverage {get; set; }
        
        // List of events
        // 

        BAC_Controller()
        {
            //Person = new Model.Person();
        }

        public void Business_Logic()
        {
            /*int genderRate;
            if(Person.Gender=="Female"){
                genderRate = 1.13;
            }else{
                genderRate=1;
            }
            return ((((Beverage.completed_content*7.156655998)/weight)/100)*genderRate)+existingBAC-((time/15)*.003);*/
        }
    }
}
