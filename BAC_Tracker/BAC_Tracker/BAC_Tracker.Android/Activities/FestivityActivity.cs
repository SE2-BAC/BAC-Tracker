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
using Toolbar = Android.Widget.Toolbar;
using Android.Support.V7.Widget;

namespace BAC_Tracker.Droid.Activities
{
    [Activity(Label = "BAC", Icon = "@drawable/icon")]
    public class FestivityActivity : Activity
    {
        TextView mMaxBAC;
        TextView mCurrBAC;
        Button mDrinks;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Festivity);

            //Set our toolbar
            var mToolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetActionBar(mToolbar);
            //ActionBar.SetIcon(Resource.Drawable.Icon);
            ActionBar.Title = "Festivity";

            mMaxBAC = FindViewById<TextView>(Resource.Id.maxBAC);
            mCurrBAC = FindViewById<TextView>(Resource.Id.currBAC);
            mDrinks = FindViewById<Button>(Resource.Id.drinkListButton);

            mDrinks.Click += delegate
            {
                Intent intent = new Intent(this, typeof(DrinksActivity));
                StartActivity(intent);
            };
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.top_menus, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menu_settings:
                    Intent intent = new Intent(this, typeof(SettingsActivity));
                    StartActivity(intent);
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
    }
}