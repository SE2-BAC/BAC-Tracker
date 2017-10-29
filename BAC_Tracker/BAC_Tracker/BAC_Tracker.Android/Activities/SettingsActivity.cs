using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace BAC_Tracker.Droid
{
    [Activity(Label = "Settings")]
    public class SettingsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Settings);

            //Set toolbar
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetActionBar(toolbar);
            ActionBar.Title = "Settings";

            //NumberPicker gender = FindViewById<NumberPicker>(Resource.Id.pickerGender);
            //gender.MinValue = 0;
            //gender.MaxValue = 1;
            //gender.SetDisplayedValues(new string[] {"Male", "Female"});
            //gender.Value = 0;

            //NumberPicker weight = FindViewById<NumberPicker>(Resource.Id.pickerWeight);
            //weight.MinValue = 80;
            //weight.MaxValue = 300;
            //weight.WrapSelectorWheel = false;

            //NumberPicker bodyType = FindViewById<NumberPicker>(Resource.Id.pickerBodyType);
            //bodyType.MinValue = 0;
            //bodyType.MaxValue = 5;
            //bodyType.SetDisplayedValues(new string[] {"Toned", "Average", "Large", "Muscular", "Slim", "Stocky" });
            //bodyType.WrapSelectorWheel = false;

            Button done = FindViewById<Button>(Resource.Id.doneButton);

            done.Click += delegate
            {


            };
        }
    }
}