using System;
using System.Collections.ObjectModel;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Toolbar = Android.Widget.Toolbar;
using Android.OS;
using Android.Support.V7.Widget.Helper;
using Android.Support.V7.Widget;
using com.refractored.fab;
using BAC_Tracker.Droid.Activities;
using BAC_Tracker.Droid.Adapters;

using BAC_Tracker.Droid.Classes;

namespace BAC_Tracker.Droid
{
	[Activity (Label = "BAC_Tracker.Android")]
	public class MainActivity : Activity, Interfaces.IOnStartDragListener
    {
        ObservableCollection<Model.Festivity> festivities;
        ItemTouchHelper itemTouchHelper;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            //Set our toolbar
            var mToolbar = FindViewById<Toolbar>(Resource.Id.app_bar);
            SetActionBar(mToolbar);
            ActionBar.Title = "Festivities";

            festivities = new ObservableCollection<Model.Festivity>();
            festivities.Add(new Model.Festivity(1,DateTime.Now, 0.1,0.25,null));

            FestivityAdapter festivityAdapter = new FestivityAdapter(this, festivities);

            RecyclerView festivitiesRecyclerView = FindViewById<RecyclerView>(Resource.Id.festivities_recycler_view);
            festivitiesRecyclerView.SetLayoutManager(new LinearLayoutManager(this));
            festivitiesRecyclerView.SetAdapter(festivityAdapter);

            ItemTouchHelper.Callback callback = new ItemTouchHelperCallback(festivityAdapter);
            itemTouchHelper = new ItemTouchHelper(callback);
            itemTouchHelper.AttachToRecyclerView(festivitiesRecyclerView);

            //festivityAdapter.ItemClick += OnItemClick;

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.add_festivity_fab);
            fab.AttachToRecyclerView(festivitiesRecyclerView);
            fab.Click += (sender, args) =>
            {
                Intent intent = new Intent(this, typeof(FestivityActivity));
                StartActivity(intent);
            };

        }

        public void OnStartDrag(RecyclerView.ViewHolder viewHolder)
        {
            itemTouchHelper.StartDrag(viewHolder);
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


