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
                Intent intent = new Intent(this, typeof(EditDrinkActivity));
                intent.PutExtra("AddDrink", true);
                StartActivity(intent);
            };
        }

        protected override void OnResume()
        {
            Update();
            //UpdateAfterFetch();
            base.OnResume();
            
        }

        public async void Update() { await AzureBackend.GetBeverages(UpdateAfterFetch); }

        void OnItemClick(object sender, int position)
        {
            AzureBackend.currentBeverage = AzureBackend.currentFestivity.Beverage_List[position];
            Intent intent = new Intent(this, typeof(EditDrinkActivity));
            intent.PutExtra("SaveDrink", true);
            intent.PutExtra("Index", position);
            StartActivity(intent);
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