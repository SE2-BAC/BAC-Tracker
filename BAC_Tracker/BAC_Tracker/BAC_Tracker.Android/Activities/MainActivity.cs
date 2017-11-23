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
using BAC_Tracker.Droid.Activities;
using BAC_Tracker.Droid.Adapters;

namespace BAC_Tracker.Droid
{
	[Activity (Label = "BAC_Tracker.Android")]
	public class MainActivity : Activity
    {
        string[] mData;
        FestivityAdapter festivityAdapter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            //Set our toolbar
            var mToolbar = FindViewById<Toolbar>(Resource.Id.app_bar);
            SetActionBar(mToolbar);
            ActionBar.Title = "Festivities";

            mData = new string[] { "Item 1", "Item 2", "Item 3", "Item 4", "Item 5", "Item 6", "Item 7", "Item 8", "Item 9", "Item 10" };

            RecyclerView festivitiesRecyclerView = FindViewById<RecyclerView>(Resource.Id.festivities_recycler_view);
            festivitiesRecyclerView.SetLayoutManager(new LinearLayoutManager(this));

            festivityAdapter = new FestivityAdapter(mData);
            festivitiesRecyclerView.SetAdapter(festivityAdapter);
            festivityAdapter.ItemClick += OnItemClick;

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.add_festivity_fab);
            fab.AttachToRecyclerView(festivitiesRecyclerView);
            fab.Click += (sender, args) =>
            {
                Intent intent = new Intent(this, typeof(FestivityActivity));
                StartActivity(intent);
            };

        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
        }

        void OnItemClick(object sender, int position)
        {
            int itemNum = position + 1;
            Toast.MakeText(this, "This is item " + itemNum, ToastLength.Short).Show();
        }
    }
}


