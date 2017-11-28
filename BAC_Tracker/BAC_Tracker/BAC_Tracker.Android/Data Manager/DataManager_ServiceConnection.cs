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
using BAC_Tracker.Droid.Activities;

//Code provided by Nicky Marler
namespace BAC_Tracker.Droid.Data_Manager
{
    public class DataManager_ServiceConnection : Java.Lang.Object, IServiceConnection
    {
        public bool IsConnected { get; private set; }
        public DataManager_Binder Binder { get; private set; }

        DrinksActivity drinksActivity;
        public DataManager_ServiceConnection(DrinksActivity activity)
        {
            IsConnected = false;
            Binder = null;
            drinksActivity = activity;
        }

        public void OnServiceConnected(ComponentName name, IBinder service)

        {

            Binder = service as DataManager_Binder;

            IsConnected = this.Binder != null;


            if (IsConnected)
            {
                //Funtction in the MainActivity being called.
                // mainActivity.UpdateUiForBoundService();
            }
            else
            {
                //Funtction in the MainActivity being called.
                // mainActivity.UpdateUiForUnboundService();
            }

            //mainActivity.timestampMessageTextView.Text = message;
        }



        public void OnServiceDisconnected(ComponentName name)
        {

            IsConnected = false;
            Binder = null;
            //Funtction in the MainActivity being called.
            //mainActivity.UpdateUiForUnboundService();
        }


        /*
         * A function that calls a function from the binder
         * 
        public string GetFormattedTimestamp()
        {
            if (!IsConnected)
            {
                return null;
            }
            return Binder?.GetFormattedTimestamp();
        }
        */

    }
}