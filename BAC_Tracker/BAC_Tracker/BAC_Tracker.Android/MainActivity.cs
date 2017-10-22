using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace BAC_Tracker.Droid
{
	[Activity (Label = "BAC_Tracker.Android", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		int count;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += delegate {
				button.Text = string.Format ("{0} clicks!", count++);
			};
		}

        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutInt("counter", count);
            base.OnSaveInstanceState(outState);
        }
    }

}


