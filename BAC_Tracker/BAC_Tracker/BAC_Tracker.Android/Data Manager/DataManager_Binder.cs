using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

//Code provided by Nicky Marler
namespace BAC_Tracker.Droid.Data_Manager
{
    public class DataManager_Binder : Binder
    {
        public DataManager_Service Service { get; private set; }


        public DataManager_Binder(DataManager_Service service)
        {
            this.Service = service;
        }

        /*
         * A Function that s called by the ServiceConnection. 
         * This Function calls a function from the service.
         * 
        public string GetFormattedTimestamp()
        {
            return Service?.GetFormattedTimestamp();
        }
        */
    }
}