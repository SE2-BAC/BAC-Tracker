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
    [Activity(Label = "DrinkPropsActivity")]
    public class DrinkPropsActivity : Activity, SeekBar.IOnSeekBarChangeListener
    {
        NumberPicker mGlassPicker;
        NumberPicker mModelPicker;
        TextView mPercentConsumed_Text;
        SeekBar mPercentConsumed_SeekBar;
        Button mCancel, mAdd;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.DrinkProperties);

            var mToolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetActionBar(mToolbar);
            ActionBar.Title = "Drinks";

            mGlassPicker = FindViewById<NumberPicker>(Resource.Id.glassPicker);
            mModelPicker = FindViewById<NumberPicker>(Resource.Id.drinksPicker);
            mPercentConsumed_Text = FindViewById<TextView>(Resource.Id.percentConsumed_Text);
            mPercentConsumed_SeekBar = FindViewById<SeekBar>(Resource.Id.percentConsumed_SeekBar);
            mAdd = FindViewById<Button>(Resource.Id.addDrink);
            mCancel = FindViewById<Button>(Resource.Id.cancelDrink);

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
            mPercentConsumed_Text.Text = mPercentConsumed_SeekBar.Progress.ToString() + "%";
        }

        public void OnStartTrackingTouch(SeekBar seekBar) { }

        public void OnStopTrackingTouch(SeekBar seekBar)
        {
            mPercentConsumed_Text.Text = mPercentConsumed_SeekBar.Progress.ToString() + "%";
        }
    }
}