using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Toolbar = Android.Widget.Toolbar;
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
    public class DrinksActivity : Activity, IOnStartDragListener
    {
        ObservableCollection<Beverage> drinks;
        ItemTouchHelper itemTouchHelper;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_drinks);

            //Set our toolbar
            var toolbar = FindViewById<Toolbar>(Resource.Id.app_bar);
            SetActionBar(toolbar);
            ActionBar.SetHomeButtonEnabled(true);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.Title = "Your Drinks";

            drinks = new ObservableCollection<Beverage>();
            drinks.Add(new Beverage("Lightbeer", 100, "bottle" ));
            drinks.Add(new Beverage("Whiskey", 100, "whiskey"));
            drinks.Add(new Beverage("Vodka", 35, "vodka"));

            DrinksAdapter drinksAdapter = new DrinksAdapter(this, drinks);

            RecyclerView drinksRecyclerView = FindViewById<RecyclerView>(Resource.Id.drinks_recycler_view);
            drinksRecyclerView.SetLayoutManager(new LinearLayoutManager(this));
            drinksRecyclerView.SetAdapter(drinksAdapter);

            ItemTouchHelper.Callback callback = new ItemTouchHelperCallback(drinksAdapter);
            itemTouchHelper = new ItemTouchHelper(callback);
            itemTouchHelper.AttachToRecyclerView(drinksRecyclerView);
            
            //mAdapter.ItemClick += OnItemClick;

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.add_drink_fab);
            fab.AttachToRecyclerView(drinksRecyclerView);
            fab.Click += (sender, args) =>
            {
                Intent intent = new Intent(this, typeof(EditDrinkActivity));
                //StartActivityForResult(intent, ADD_DRINK);
                StartActivity(intent);
            };

            void OnItemClick(object sender, int position)
            {
                int itemNum = position + 1;
                Toast.MakeText(this, "This is item " + itemNum, ToastLength.Short).Show();
            }
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
    }
}