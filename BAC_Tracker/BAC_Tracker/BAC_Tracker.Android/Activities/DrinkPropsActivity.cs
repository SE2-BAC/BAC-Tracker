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

namespace BAC_Tracker.Droid.Activities
{
    [Activity(Label = "Drinks", MainLauncher =true)]
    public class DrinkPropsActivity : Activity, SeekBar.IOnSeekBarChangeListener
    {
        NumberPicker mGlassPicker;
        NumberPicker mModelPicker;
        TextView mPercentConsumed_Text;
        Button mCancel, mAdd;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.DrinkProperties);

            var mToolbar = FindViewById<Toolbar>(Resource.Id.toolbar_drink);
            SetActionBar(mToolbar);
            ActionBar.Title = ""; //No title

            mGlassPicker = FindViewById<NumberPicker>(Resource.Id.glassPicker);
            mModelPicker = FindViewById<NumberPicker>(Resource.Id.drinksPicker);
            mPercentConsumed_Text = FindViewById<TextView>(Resource.Id.percentConsumed_Text);
            SeekBar mPercentConsumed_SeekBar = FindViewById<SeekBar>(Resource.Id.percentConsumed_SeekBar);
            mAdd = FindViewById<Button>(Resource.Id.add);
            mCancel = FindViewById<Button>(Resource.Id.cancel);

            //Set Control Properties
            mGlassPicker.MinValue = 0;
            mGlassPicker.MaxValue = 8;
            mGlassPicker.WrapSelectorWheel = false;
            mGlassPicker.SetDisplayedValues(new string[] { "Beer", "Brandy", "Martini", "Whiskey", "Wine", "Vodka", "Tequila", "Liquor", "Bottle" });

            mModelPicker.MinValue = 0;
            mModelPicker.MaxValue = 6;
            mModelPicker.WrapSelectorWheel = false;
            mModelPicker.SetDisplayedValues(new string[] {"Lightbeer", "Liquor", "Whiskey", "Gin", "Vodka", "Red Wine", "White Wine" });

            mPercentConsumed_SeekBar.Max = 100;
            mPercentConsumed_SeekBar.SetOnSeekBarChangeListener(this);

            mPercentConsumed_Text.Text = mPercentConsumed_SeekBar.Progress.ToString() + "%";

            mAdd.Click += delegate { };
            mCancel.Click += delegate { Finish(); };
        }

        public void OnProgressChanged(SeekBar seekBar, int progress, bool fromUser)
        {
            mPercentConsumed_Text.Text = progress + "%";
        }

        public void OnStartTrackingTouch(SeekBar seekBar) { }

        public void OnStopTrackingTouch(SeekBar seekBar){}
    }
}