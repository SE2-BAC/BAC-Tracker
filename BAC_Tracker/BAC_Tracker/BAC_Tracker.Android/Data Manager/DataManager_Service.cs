using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    [Service]
    public class DataManager_Service : Service
    {
        public IBinder Binder { get; private set; }
        public string testString;
        public int TestCounter;

        public ObservableCollection<Model.Beverage> drinks;

        public override void OnCreate()
        {
            // This method is optional to implement
            base.OnCreate();
            //Create my variables to be used throughout the viewmodel. The raw data list and filtered list. plus the adapter for the list    
            testString = "I am awesome";
            TestCounter = 0;
        }

        public override IBinder OnBind(Intent intent)
        {
            // This method must always be implemented
            //
            this.Binder = new DataManager_Binder(this);
            return this.Binder;
        }


        public override bool OnUnbind(Intent intent)
        {
            // This method is optional to implement
            //Something about unbinding everything?
            return base.OnUnbind(intent);
        }

        public string testStrings()
        {
            return testString?.ToString();
        }

        public string TestIntCounter()
        {
            TestCounter++;
            return TestCounter.ToString();
        }

        public async void SearchDB()
        {
            //await Main_List.Fetch_Data();
        }
    }
}