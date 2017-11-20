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
        ImageView mGlassImage;
        SeekBar mPercentConsumed_SeekBar;
        Button mCancel, mAdd;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.DrinkProperties);

            var mToolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetActionBar(mToolbar);
            ActionBar.SetHomeButtonEnabled(true);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.Title = "Drinks";

            mGlassPicker = FindViewById<NumberPicker>(Resource.Id.glassPicker);
            mModelPicker = FindViewById<NumberPicker>(Resource.Id.drinksPicker);
            mPercentConsumed_Text = FindViewById<TextView>(Resource.Id.percentConsumed_Text);
            mGlassImage = FindViewById<ImageView>(Resource.Id.glassImage);
            mPercentConsumed_SeekBar = FindViewById<SeekBar>(Resource.Id.percentConsumed_SeekBar);
            mAdd = FindViewById<Button>(Resource.Id.addDrink);
            mCancel = FindViewById<Button>(Resource.Id.cancelDrink);

            //Set Control Properties
            mGlassPicker.MinValue = 0;
            mGlassPicker.MaxValue = 8;
            mGlassPicker.Value = 0;
            mGlassPicker.WrapSelectorWheel = false;
            mGlassPicker.SetDisplayedValues(new string[] { "Beer", "Brandy", "Martini", "Whiskey", "Wine", "Vodka", "Tequila", "Liqueur", "Bottle" });

            mGlassPicker.ValueChanged += OnValueChange_Glass;
        }

        public void OnProgressChanged(SeekBar seekBar, int progress, bool fromUser)
        {
            throw new NotImplementedException();
        }

        public void OnStartTrackingTouch(SeekBar seekBar)
        {
            throw new NotImplementedException();
        }

        public void OnStopTrackingTouch(SeekBar seekBar)
        {
            throw new NotImplementedException();
        }

        public void OnValueChange_Glass(Object sender, EventArgs e)
        {
            string[] values = mGlassPicker.GetDisplayedValues();
            switch (values[mGlassPicker.Value]) {
                case "Beer":
                    mGlassImage.SetImageResource(Resource.Drawable.beer);
                    break;
                case "Brandy":
                    break;
                case "Martini":
                    break;
                case "Whiskey":
                    break;
                case "Wine":
                    break;
                case "Vodka":

                    break;
                case "Tequila":
                    break;
                case "Liqueur":
                    break;
                case "Bottle":
                    break;
            }
        }

        public void OnValueChange_Drink(NumberPicker picker, int oldVal, int newVal)
        {
            throw new NotImplementedException();
        }
    }
}