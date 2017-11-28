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
using BAC_Tracker.Droid.Classes;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace BAC_Tracker.Droid.Activities
{
    [Activity(Label = "Drinks")]
    public class EditDrinkActivity : AppCompatActivity, SeekBar.IOnSeekBarChangeListener
    {
        TextView drinkPercent;
        NumberPicker drinkGlass;
        NumberPicker drinkModel;
        SeekBar seekbar;
        int Index;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            AzureBackend.Touch(this);

            // Create your application here
            SetContentView(Resource.Layout.activity_drink_info);

            var toolbar = FindViewById<Toolbar>(Resource.Id.app_bar_drink);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = ""; //No title

            bool AddDrink = Intent.GetBooleanExtra("AddDrink", false);
            bool SaveDrink = Intent.GetBooleanExtra("SaveDrink", false);
            Index = Intent.GetIntExtra("Index", 0);

            drinkGlass = FindViewById<NumberPicker>(Resource.Id.drink_glass);
            drinkModel = FindViewById<NumberPicker>(Resource.Id.drink_model);
            drinkPercent = FindViewById<TextView>(Resource.Id.drink_percent_consumed_text);
            seekbar = FindViewById<SeekBar>(Resource.Id.drink_percent_consumed_seekbar);
            TextView statusDrinkTitle = FindViewById<TextView>(Resource.Id.drink_status_title);
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

            drinkCancel.Click += delegate { Finish(); };
            drinkDelete.Click += OnClick_Delete;

            if (SaveDrink)
            {
                drinkAdd.Text = "Save";
                statusDrinkTitle.Text = "Edit Drink";
                
                drinkAdd.Click += OnClick_Save;
                drinkGlass.Value = Array.IndexOf(drinkGlass.GetDisplayedValues(), AzureBackend.currentBeverage.Container);
                drinkModel.Value = Array.IndexOf(drinkModel.GetDisplayedValues(), AzureBackend.currentBeverage.Model);
                seekbar.Progress = (int)(AzureBackend.currentBeverage.Percentage_consumed * 100);
            }
            else if (AddDrink)
            {
                drinkAdd.Text = "Add";
                statusDrinkTitle.Text = "Adding Drink";

                drinkAdd.Click += OnClick_Add;
                drinkDeletePlaceholder.Visibility = ViewStates.Gone;
            }
        }

        //
        public async void OnClick_Add(Object sender, EventArgs e) {
            string model = drinkModel.GetDisplayedValues()[drinkModel.Value];
            string container = drinkGlass.GetDisplayedValues()[drinkGlass.Value];
            double perConsumed = ((double)seekbar.Progress/100);
            int festivityID = AzureBackend.currentFestivity.FestivityID;
            Model.Beverage beverage = new Model.Beverage(model,perConsumed,container,festivityID);
            await AzureBackend.AddBeverage(beverage);
            Finish();
        }

        public async void OnClick_Save(Object sender, EventArgs e) {
            Model.Beverage beverage = AzureBackend.currentBeverage;
            beverage.Model = drinkModel.GetDisplayedValues()[drinkModel.Value];
            beverage.Container = drinkGlass.GetDisplayedValues()[drinkGlass.Value];
            beverage.Percentage_consumed = ((double)seekbar.Progress / 100);
            await AzureBackend.UpdateBeverage(Index);
            Finish();
        }

        public async void OnClick_Delete(Object sender, EventArgs e) {
            await AzureBackend.DeleteBeverage(Index);
            Finish();
        }

        public void OnProgressChanged(SeekBar seekBar, int progress, bool fromUser)
        {
           drinkPercent.Text = "Percent Consumed: " + progress + "%";
        }

        public void OnStartTrackingTouch(SeekBar seekBar) { }

        public void OnStopTrackingTouch(SeekBar seekBar){}
    }
}