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

namespace BAC_Tracker.Droid
{
	[Activity (Label = "BAC_Tracker.Android", Icon = "@drawable/icon")]
	public class MainActivity : Activity
    {
        string[] mData;
        RecyclerView mRecyclerView;
        RecyclerViewAdapter mAdapter;
        RecyclerView.LayoutManager mLayoutManager;
        FloatingActionButton mFAB;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //Set our toolbar
            var mToolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetActionBar(mToolbar);
            //ActionBar.SetIcon(Resource.Drawable.Icon);
            ActionBar.Title = "BAC Tracker";

            mData = new string[] { "Item 1", "Item 2", "Item 3", "Item 4", "Item 5", "Item 6", "Item 7", "Item 8", "Item 9", "Item 10" };

            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerViewEvent);
            mLayoutManager = new LinearLayoutManager(this);
            mRecyclerView.SetLayoutManager(mLayoutManager);
            //mRecyclerView.AddItemDecoration(new DermaClinic.Droid.Fragments.DividerItemDecoration(this)); TODO:<ABUJANDA> Fix this line

            mAdapter = new RecyclerViewAdapter(mData);
            mRecyclerView.SetAdapter(mAdapter);
            mAdapter.ItemClick += OnItemClick;

            mFAB = FindViewById<FloatingActionButton>(Resource.Id.addEventFAB);
            mFAB.AttachToRecyclerView(mRecyclerView);
            mFAB.Click += (sender, args) =>
            {
                Toast.MakeText(this, "FAB Clicked", ToastLength.Short).Show();
                //GenderDialogFragment frag = new GenderDialogFragment();
                //frag.Show(FragmentManager, GenderDialogFragment.TAG);
            };

        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.top_menus, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch(item.ItemId){
                case Resource.Id.menu_settings:
                    Intent intent = new Intent(this, typeof(SettingsActivity));
                    StartActivity(intent);
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        void OnItemClick(object sender, int position)
        {
            int itemNum = position + 1;
            Toast.MakeText(this, "This is item " + itemNum, ToastLength.Short).Show();
        }
    }
}


