using System;
using Android;
using Android.App;
using Android.Preferences;
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
            SetContentView(Resource.Layout.activity_settings);

            //Set toolbar
            var toolbar = FindViewById<Toolbar>(Resource.Id.app_bar);
            SetActionBar(toolbar);
            ActionBar.Title = "Settings";
            ActionBar.SetHomeButtonEnabled(true);
            ActionBar.SetDisplayHomeAsUpEnabled(true);

            FragmentManager.BeginTransaction().Replace(Resource.Id.maincontent, new Fragments.SettingsFragment()).Commit();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Finish();
            return true;
        }
    }
}