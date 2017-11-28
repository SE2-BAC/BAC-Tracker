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
    public class EditDrinkActivity : AppCompatActivity, SeekBar.IOnSeekBarChangeListener, NumberPicker.IOnValueChangeListener
    {
        TextView drinkPercent;
        TextView drinkTotalContent;
        TextView drinkAlcoholContent;
        NumberPicker drinkGlass;
        NumberPicker drinkModel;
        SeekBar seekbar;
        Model.Beverage beverage;
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
            Index = Intent.GetIntExtra("Index", AzureBackend.currentFestivity.Beverage_List.Count);

            drinkGlass = FindViewById<NumberPicker>(Resource.Id.drink_glass);
            drinkGlass.SetOnValueChangedListener(this);
            drinkModel = FindViewById<NumberPicker>(Resource.Id.drink_model);
            drinkModel.SetOnValueChangedListener(this);
            drinkPercent = FindViewById<TextView>(Resource.Id.drink_percent_consumed_text);
            drinkAlcoholContent = FindViewById<TextView>(Resource.Id.drink_alcohol_content);
            drinkTotalContent = FindViewById<TextView>(Resource.Id.drink_total_content);
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
            seekbar.Progress = 0;
            seekbar.SetOnSeekBarChangeListener(this);

            drinkAdd.Click += OnClick_Save;
            drinkCancel.Click += delegate { Finish(); };
            drinkDelete.Click += OnClick_Delete;

            if (SaveDrink)
            {
                drinkAdd.Text = "Save";
                statusDrinkTitle.Text = "Edit Drink";
                beverage = AzureBackend.currentBeverage;
                drinkGlass.Value = Array.IndexOf(drinkGlass.GetDisplayedValues(), AzureBackend.currentBeverage.Container);
                drinkModel.Value = Array.IndexOf(drinkModel.GetDisplayedValues(), AzureBackend.currentBeverage.Model);
                seekbar.Progress = (int)(AzureBackend.currentBeverage.Percentage_consumed * 100);
            }
            else if (AddDrink)
            {
                string model = drinkModel.GetDisplayedValues()[drinkModel.Value];
                string container = drinkGlass.GetDisplayedValues()[drinkGlass.Value];
                double perConsumed = 0;
                int festivityID = AzureBackend.currentFestivity.FestivityID;
                drinkAdd.Text = "Add";
                statusDrinkTitle.Text = "Adding Drink";
                drinkDeletePlaceholder.Visibility = ViewStates.Gone;
                beverage = new Model.Beverage(model, perConsumed, container, festivityID);
                AddBeverage();
            }

            drinkAlcoholContent.Text = "Alcohol Content: " + beverage.Alcohol_percentage + "%";
            drinkTotalContent.Text = "Total Content: " + beverage.Volume + " " + "fl oz";
            drinkPercent.Text = "Percent Consumed: " + seekbar.Progress.ToString() + "%";
        }

        public async void AddBeverage() {
            await AzureBackend.AddBeverage(beverage);
            beverage = AzureBackend.currentFestivity.Beverage_List[Index];
        }

        public async void OnClick_Save(Object sender, EventArgs e) {
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
            beverage.Percentage_consumed = ((double)progress / 100);
            drinkPercent.Text = "Percent Consumed: " + progress + "%";
        }

        public void OnStartTrackingTouch(SeekBar seekBar) { }

        public void OnStopTrackingTouch(SeekBar seekBar){}

        public void OnValueChange(NumberPicker picker, int oldVal, int newVal)
        {
            switch (picker.Id) {
                case (Resource.Id.drink_glass):
                    beverage.Container = drinkGlass.GetDisplayedValues()[newVal];
                    drinkTotalContent.Text = "Total Content: " + beverage.Volume + " " + "fl oz";
                    break;
                case (Resource.Id.drink_model):
                    beverage.Model = drinkModel.GetDisplayedValues()[newVal];
                    drinkAlcoholContent.Text = "Alcohol Content: " + beverage.Alcohol_percentage + "%";
                    break;
                default:
                    break;
            }
        }
    }
}