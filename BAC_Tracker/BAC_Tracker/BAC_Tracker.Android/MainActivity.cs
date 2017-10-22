﻿using System;

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

<<<<<<< HEAD
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetActionBar(toolbar);
            ActionBar.Title = "BAC Tracker";

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);

            if (bundle != null)
            {
                count = bundle.GetInt("counter", 1);
                button.Text = string.Format("{0} clicks!", count);
            }
            else
            {
                count = 1;
            }

            button.Click += delegate
            {
                button.Text = string.Format("{0} clicks!", count++);
            };
        }
=======
			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += delegate {
				button.Text = string.Format ("{0} clicks!", count++);
			};
		}
>>>>>>> 500991e27b1e9d11068377153f6d0b6c1393b595

        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutInt("counter", count);
            base.OnSaveInstanceState(outState);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.top_menus, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch(item.ItemId){
                case Resource.Id.menu_settings:
                    StartActivity(typeof(SettingsActivity));
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
    }
}


