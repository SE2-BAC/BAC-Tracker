using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Toolbar = Android.Widget.Toolbar;
using Android.OS;
using Android.Support.V7.Widget;
using com.refractored.fab;
using BAC_Tracker.Droid.Classes;
using BAC_Tracker.Droid.Fragments;

namespace BAC_Tracker.Droid.Activities
{
    [Activity(Label = "DrinksActivity", MainLauncher = true)]
    public class DrinksActivity : Activity
    {
        string[] mData;
        RecyclerView mRecyclerView;
        RecyclerViewAdapter mAdapter;
        RecyclerView.LayoutManager mLayoutManager;
        FloatingActionButton mFAB;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Drinks);

            //Set our toolbar
            var mToolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetActionBar(mToolbar);
            //ActionBar.SetIcon(Resource.Drawable.Icon);
            ActionBar.Title = "Your Drinks";

            mData = new string[] { "Drink 1", "Drink 2", "Drink 3", "Drink 4", "Drink 5", "Drink 6", "Drink 7", "Drink 8", "Drink 9", "Drink 10" };

            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerViewDrinks);
            mLayoutManager = new LinearLayoutManager(this);
            mRecyclerView.SetLayoutManager(mLayoutManager);
            //mRecyclerView.AddItemDecoration(new DermaClinic.Droid.Fragments.DividerItemDecoration(this)); TODO:<ABUJANDA> Fix this line

            mAdapter = new RecyclerViewAdapter(mData);
            mRecyclerView.SetAdapter(mAdapter);
            mAdapter.ItemClick += OnItemClick;

            mFAB = FindViewById<FloatingActionButton>(Resource.Id.addDrinkFAB);
            mFAB.AttachToRecyclerView(mRecyclerView);
            mFAB.Click += (sender, args) =>
            {
                Toast.MakeText(this, "FAB Clicked", ToastLength.Short).Show();
                //GenderDialogFragment frag = new GenderDialogFragment();
                //frag.Show(FragmentManager, GenderDialogFragment.TAG);
            };

            void OnItemClick(object sender, int position)
            {
                int itemNum = position + 1;
                Toast.MakeText(this, "This is item " + itemNum, ToastLength.Short).Show();
            }
        }
    }
}