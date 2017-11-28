using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Support.V7.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.Widget;
using BAC_Tracker.Droid.Classes;
using BAC_Tracker.Droid.Fragments;

namespace BAC_Tracker.Droid.Activities
{
    [Activity(Label = "Festivity", Icon = "@drawable/icon")]
    public class FestivityActivity : AppCompatActivity, SeekBar.IOnSeekBarChangeListener
    {
        TextView maxBAC;
        TextView currBAC;
        TextView alcoholCons;
        Controller.BAC_Controller BAC_Controller;
        AlertDialogFragment frag = new AlertDialogFragment();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_festivity);

            //Set our toolbar
            var toolbar = FindViewById<Toolbar>(Resource.Id.app_bar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Festivity";

            maxBAC = FindViewById<TextView>(Resource.Id.text_max_BAC);
            currBAC = FindViewById<TextView>(Resource.Id.text_curr_BAC);
            alcoholCons = FindViewById<TextView>(Resource.Id.text_consumption);

            BAC_Controller = new Controller.BAC_Controller();

            SeekBar seekbar = FindViewById<SeekBar>(Resource.Id.edit_max_BAC);
            seekbar.Max = 40;
            seekbar.IncrementProgressBy(1);
            seekbar.SetOnSeekBarChangeListener(this);
        }

        protected override void OnResume()
        {
            Model.Festivity festivity = AzureBackend.currentFestivity;
            if (festivity.Current_BAC > festivity.Max_BAC || festivity.Current_BAC == 0.08) {
                frag.Show(FragmentManager, AlertDialogFragment.TAG);
            }
            double totalAlochol = 0.0;
            if (festivity.Beverage_List != null)
            {
                for (int i = 0; i < festivity.Beverage_List.Count; i++)
                {
                    totalAlochol = totalAlochol + festivity.Beverage_List[i].Alcohol_percentage;

                }
            }
            alcoholCons.Text = totalAlochol + " " + "fl oz pure alcohol";
            base.OnResume();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.top_menus, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Intent intent;
            switch (item.ItemId)
            {
                case Resource.Id.menu_settings:
                    intent = new Intent(this, typeof(SettingsActivity));
                    StartActivity(intent);
                    return true;
                case Resource.Id.menu_drinks:
                    intent = new Intent(this, typeof(DrinksActivity));
                    StartActivity(intent);
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        public void OnProgressChanged(SeekBar seekBar, int progress, bool fromUser){
            maxBAC.Text = ((double)progress/100).ToString() + "%";
            AzureBackend.currentFestivity.Max_BAC = ((double)progress / 100);
        }

        public void OnStartTrackingTouch(SeekBar seekBar){}

        public void OnStopTrackingTouch(SeekBar seekBar) { }
    }
}