using System;
using Android;
using Android.App;
using Android.Support.V7.App;
using Android.Preferences;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace BAC_Tracker.Droid
{
    [Activity(Label = "Settings")]
    public class SettingsActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_settings);

            //Set toolbar
            var toolbar = FindViewById<Toolbar>(Resource.Id.app_bar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Settings";
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            FragmentManager.BeginTransaction().Replace(Resource.Id.maincontent, new Fragments.SettingsFragment()).Commit();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Finish();
            return true;
        }
    }
}