using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace BAC_Tracker.Droid.Activities
{
    [Activity(Label = "Drinks")]
    public class EditDrinkActivity : AppCompatActivity, SeekBar.IOnSeekBarChangeListener
    {
        NumberPicker drinkGlass;
        NumberPicker drinkModel;
        TextView drinkPercent; //consumed
        Button drinkAdd, drinkCancel;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_drink_info);

            var toolbar = FindViewById<Toolbar>(Resource.Id.app_bar_drink);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = ""; //No title

            drinkGlass = FindViewById<NumberPicker>(Resource.Id.drink_glass);
            drinkModel = FindViewById<NumberPicker>(Resource.Id.drink_model);
            drinkPercent = FindViewById<TextView>(Resource.Id.drink_percent_consumed_text);
            SeekBar seekbar = FindViewById<SeekBar>(Resource.Id.drink_percent_consumed_seekbar);
            drinkAdd = FindViewById<Button>(Resource.Id.app_bar_add);
            drinkCancel = FindViewById<Button>(Resource.Id.app_bar_cancel);

            string[] glasses = Resources.GetStringArray(Resource.Array.glasses);
            string[] models = Resources.GetStringArray(Resource.Array.models);

            //Set Control Properties
            drinkGlass.MinValue = 0;
            drinkGlass.MaxValue = glasses.Length - 1;
            drinkGlass.WrapSelectorWheel = true;
            drinkGlass.SetDisplayedValues(glasses);

            drinkModel.MinValue = 0;
            drinkModel.MaxValue = models.Length - 1;
            drinkModel.WrapSelectorWheel = true;
            drinkModel.SetDisplayedValues(models);

            seekbar.Max = 100;
            seekbar.SetOnSeekBarChangeListener(this);

            drinkPercent.Text = "Percent Consumed: "+ seekbar.Progress.ToString() + "%";

            drinkAdd.Click += delegate { };
            drinkCancel.Click += delegate { Finish(); };
        }

        public void OnProgressChanged(SeekBar seekBar, int progress, bool fromUser)
        {
           drinkPercent.Text = "Percent Consumed: " + progress + "%";
        }

        public void OnStartTrackingTouch(SeekBar seekBar) { }

        public void OnStopTrackingTouch(SeekBar seekBar){}
    }
}