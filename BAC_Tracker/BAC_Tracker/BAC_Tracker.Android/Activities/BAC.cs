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
    [Activity(Label = "BAC", MainLauncher =true)]
    public class BAC : Activity
    {
        TextView mMaxBAC;
        TextView mCurrBAC;
        Button mDrinks;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.BAC);

            //Set our toolbar
            var mToolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetActionBar(mToolbar);
            //ActionBar.SetIcon(Resource.Drawable.Icon);
            ActionBar.Title = "Event";

            mMaxBAC = FindViewById<TextView>(Resource.Id.maxBAC);
            mCurrBAC = FindViewById<TextView>(Resource.Id.currBAC);
            mDrinks = FindViewById<Button>(Resource.Id.drinkListButton);


        }
    }
}