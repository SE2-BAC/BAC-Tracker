using System;
using System.Collections.Generic;
using System.Text;

namespace BAC_Tracker.Controller
{
    //NM: This might need to be named as the Event_Controller to better reflect itself.
    public class BAC_Controller
    {
        public Model.Person Person { get; set; }
        public Model.Beverage Beverage {get; set; }
        
        // List of Festivities. (Festivity)
        // 

        BAC_Controller()
        {
            //NM: Person and Beverage require arguments for their constructors. 
            //NM: Person will be constructed from saved info of the user. If info does not exsist, construct froma default value. Male 150lbs?

            //NM: Will more than likely not need to construct a beverage when constructing the controller
            //Beverage = new Model.Beverage();
        }
        //NM: Void -> double since having a return value. Consideration, can pass a ref argument and do no return there.
        public double Calculate_BAC(double existingBAC)
        {
            //NM: Changed int -> double to handle female and return
            //NM: genderRate has male as default
            double genderRate = 1 ;
            if (!Person.IsMale)
                //NM: Added brackets. easier to read and predictable results
            {
                genderRate = 1.13;
            }
            //NM: The properites of Person start with a capitol letter. weight -> Weight. 
            //NM: What is this time variable?

            DateTime currentTime = DateTime.Now;
            TimeSpan timeT = Beverage.StartTime - currentTime;
            double timeTotal = timeT.TotalMinutes;
            
            var BAC = (((Beverage.Alcohol_percentage*7.156655998)/Person.Weight)/100)*genderRate*Beverage.Percentage_consumed;
            //Above BAC is instantaneous, the below code accounts for initial alcohol absorbtion and decay over time
             if(timeTotal<120){ //timeTotal is time since consumed in minutes
               BAC = ((BAC/120)*timeTotal)-(timeTotal*.0002);
               if(BAC<0){
                   BAC=0;
               }
              } else {
               BAC-=timeTotal*.0002;
             }
             
            return BAC;
        }

        //With the current Calculate_BAC code, an update function should be unnecessary. Recalling the Calculate_BAC
        //function should be all that is necessary
        /*
        //void means no return.NM update: void -> double return
        //This could be an easy lambda function for where it gets called
        public double Update_BAC(double existingBAC){
            return existingBAC-(0.003);
        }*/
    }
}
