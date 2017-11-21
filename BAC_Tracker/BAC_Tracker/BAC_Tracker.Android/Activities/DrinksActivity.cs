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
        ObservableCollection<Beverage> mDrinks;
        ItemTouchHelper mItemTouchHelper;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Drinks);

            //Set our toolbar
            var mToolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetActionBar(mToolbar);
            ActionBar.SetHomeButtonEnabled(true);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            //ActionBar.SetIcon(Resource.Drawable.Icon);
            ActionBar.Title = "Your Drinks";

            mDrinks = new ObservableCollection<Beverage>();
            mDrinks.Add(new Beverage("Lightbeer", 100, "bottle" ));
            mDrinks.Add(new Beverage("Whiskey", 100, "whiskey"));
            mDrinks.Add(new Beverage("Vodka", 35, "vodka"));

            DrinksAdapter mAdapter = new DrinksAdapter(this, mDrinks);

            RecyclerView mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerViewDrinks);
            mRecyclerView.SetLayoutManager(new LinearLayoutManager(this));
            mRecyclerView.SetAdapter(mAdapter);

            ItemTouchHelper.Callback callback = new ItemTouchHelperCallback(mAdapter);
            mItemTouchHelper = new ItemTouchHelper(callback);
            mItemTouchHelper.AttachToRecyclerView(mRecyclerView);
            
            //mAdapter.ItemClick += OnItemClick;

            FloatingActionButton mFAB = FindViewById<FloatingActionButton>(Resource.Id.addDrinkFAB);
            mFAB.AttachToRecyclerView(mRecyclerView);
            mFAB.Click += (sender, args) =>
            {
                Intent intent = new Intent(this, typeof(DrinkPropsActivity));
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
            mItemTouchHelper.StartDrag(viewHolder);
        }
    }
}