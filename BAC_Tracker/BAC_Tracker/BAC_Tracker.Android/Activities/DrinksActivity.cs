using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Android.App;
using Android.Support.V7.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.Widget.Helper;
using Android.Support.V7.Widget;
using com.refractored.fab;

using BAC_Tracker.Droid.Adapters;
using BAC_Tracker.Model;
using BAC_Tracker.Droid.Interfaces;
using BAC_Tracker.Droid.Classes;

namespace BAC_Tracker.Droid.Activities
{
    [Activity(Label = "DrinksActivity")]
    public class DrinksActivity : AppCompatActivity, IOnStartDragListener
    {
        ObservableCollection<Beverage> drinks;
        DrinksAdapter drinksAdapter;
        ItemTouchHelper itemTouchHelper;
        Festivity myFestivity;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            AzureBackend.Touch(this);
            myFestivity = AzureBackend.currentFestivity;

            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_drinks);

            //Set our toolbar
            var toolbar = FindViewById<Toolbar>(Resource.Id.app_bar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.Title = "Your Drinks";

            drinks = new ObservableCollection<Beverage>();
            AzureBackend.GetBeverages(UpdateAfterFetch);
            //drinks.Add(new Beverage("Lightbeer", 100, "bottle", 1));
            //drinks.Add(new Beverage("Whiskey", 100, "whiskey", 1));
            //drinks.Add(new Beverage("Vodka", 35, "vodka", 1));

            drinksAdapter = new DrinksAdapter(this, drinks);

            RecyclerView drinksRecyclerView = FindViewById<RecyclerView>(Resource.Id.drinks_recycler_view);
            drinksRecyclerView.SetLayoutManager(new LinearLayoutManager(this));
            drinksRecyclerView.SetAdapter(drinksAdapter);

            ItemTouchHelper.Callback callback = new ItemTouchHelperCallback(drinksAdapter);
            itemTouchHelper = new ItemTouchHelper(callback);
            itemTouchHelper.AttachToRecyclerView(drinksRecyclerView);
            
            drinksAdapter.ItemClick += OnItemClick;

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.add_drink_fab);
            fab.AttachToRecyclerView(drinksRecyclerView);
            fab.Click += (sender, args) =>
            {
                BeveragesButton();
                Intent intent = new Intent(this, typeof(EditDrinkActivity));
                StartActivity(intent);
            };
            AzureBackend.Touch(this, UpdateAfterFetch);
        }

        void OnItemClick(object sender, int position)
        {
            int itemNum = position + 1;
            Toast.MakeText(this, "This is item " + itemNum, ToastLength.Short).Show();

            AzureBackend.currentBeverage = AzureBackend.currentFestivity.Beverage_List[position];
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Finish();
            return true;
        }

        public void OnStartDrag(RecyclerView.ViewHolder viewHolder)
        {
            itemTouchHelper.StartDrag(viewHolder);
        }

        public async void BeveragesButton()
        {
            if (myFestivity.Beverage_List.Count < 8)
            {
                Random rand = new Random();
                await AzureBackend.AddBeverage(new Beverage("Test" + rand.Next(1, 1000), rand.NextDouble() * 5f, "Pint" + rand.Next(1, 100), myFestivity.FestivityID), UpdateAfterFetch);
            }
            else
            {
                while(myFestivity.Beverage_List.Count > 0)
                {
                    await AzureBackend.DeleteBeverage(0);
                }
            }
            UpdateAfterFetch();
        }

        public void UpdateAfterFetch()
        {
            if (AzureBackend.currentFestivity != null)
            {
                drinks.Clear();
                foreach (Beverage bev in AzureBackend.currentFestivity.Beverage_List)
                {
                    drinks.Add(bev);
                }
                drinksAdapter.NotifyDataSetChanged();
            }
            else
            {
                Toast.MakeText(this, "Couldn't find festivity!", ToastLength.Short).Show();
            }
        }

    }
}