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
        TextView drinkPercent; 

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_drink_info);

            var toolbar = FindViewById<Toolbar>(Resource.Id.app_bar_drink);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = ""; //No title

            bool AddDrink = Intent.GetBooleanExtra("AddDrink", false);
            bool SaveDrink = Intent.GetBooleanExtra("SaveDrink", false);

            NumberPicker drinkGlass = FindViewById<NumberPicker>(Resource.Id.drink_glass);
            NumberPicker drinkModel = FindViewById<NumberPicker>(Resource.Id.drink_model);
            drinkPercent = FindViewById<TextView>(Resource.Id.drink_percent_consumed_text);
            SeekBar seekbar = FindViewById<SeekBar>(Resource.Id.drink_percent_consumed_seekbar);
            Button drinkAdd = FindViewById<Button>(Resource.Id.app_bar_add);
            Button drinkCancel = FindViewById<Button>(Resource.Id.app_bar_cancel);
            Button drinkDelete = FindViewById<Button>(Resource.Id.drink_delete);
            LinearLayout drinkDeletePlaceholder = FindViewById<LinearLayout>(Resource.Id.drink_delete_placeholder);

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

            drinkAdd.Click += OnClick_Add;
            drinkCancel.Click += delegate { Finish(); };

            if (SaveDrink)
            {
                drinkAdd.Text = "Save";
                drinkAdd.Click += OnClick_Save;
            }
            else if (AddDrink) {
                drinkDeletePlaceholder.Visibility = ViewStates.Gone;
            }
        }

        public void OnClick_Add(Object sender, EventArgs e) { }

        public void OnClick_Save(Object sender, EventArgs e) { }

        public void OnProgressChanged(SeekBar seekBar, int progress, bool fromUser)
        {
           drinkPercent.Text = "Percent Consumed: " + progress + "%";
        }

        public void OnStartTrackingTouch(SeekBar seekBar) { }

        public void OnStopTrackingTouch(SeekBar seekBar){}
    }
}