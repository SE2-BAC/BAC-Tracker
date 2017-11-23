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
    [Activity(Label = "Festivity", Icon = "@drawable/icon")]
    public class FestivityActivity : Activity, SeekBar.IOnSeekBarChangeListener
    {
        TextView maxBAC;
        TextView currBAC;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_festivity);

            //Set our toolbar
            var toolbar = FindViewById<Toolbar>(Resource.Id.app_bar);
            SetActionBar(toolbar);
            ActionBar.Title = "Festivity";

            maxBAC = FindViewById<TextView>(Resource.Id.text_max_BAC);
            currBAC = FindViewById<TextView>(Resource.Id.text_curr_BAC);
            Button drinksList = FindViewById<Button>(Resource.Id.drinkListButton);

            SeekBar seekbar = FindViewById<SeekBar>(Resource.Id.edit_max_BAC);
            seekbar.Max = 40;
            seekbar.IncrementProgressBy(1);
            seekbar.SetOnSeekBarChangeListener(this);

            drinksList.Click += delegate
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

        public void OnProgressChanged(SeekBar seekBar, int progress, bool fromUser){
            maxBAC.Text = ((double)progress/100).ToString() + "%";
        }

        public void OnStartTrackingTouch(SeekBar seekBar){}

        public void OnStopTrackingTouch(SeekBar seekBar) { }
    }
}