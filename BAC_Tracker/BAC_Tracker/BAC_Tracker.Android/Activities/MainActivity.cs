using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Android.App;
using Android.Support.V7.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.OS;
using Android.Support.V7.Widget.Helper;
using Android.Support.V7.Widget;
using com.refractored.fab;

using BAC_Tracker.Droid.Activities;
using BAC_Tracker.Droid.Adapters;
using BAC_Tracker.Droid.Fragments;
using BAC_Tracker.Model;
using BAC_Tracker.Droid.Classes;

namespace BAC_Tracker.Droid
{
	[Activity (Label = "BAC_Tracker.Android")]
	public class MainActivity : AppCompatActivity, Interfaces.IOnStartDragListener
    {
        ObservableCollection<Festivity> festivities;
        FestivityAdapter festivityAdapter;
        ItemTouchHelper itemTouchHelper;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            //Set our toolbar
            var toolbar = FindViewById<Toolbar>(Resource.Id.app_bar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Festivities";

            festivities = new ObservableCollection<Festivity>();
            //festivities.Add(new Festivity(1,DateTime.Now, 0.1,0.25, null));
            //festivities.Add(new Festivity(2, DateTime.Now, 0.3, 0.55, null));

            festivityAdapter = new FestivityAdapter(this, festivities);

            RecyclerView festivitiesRecyclerView = FindViewById<RecyclerView>(Resource.Id.festivities_recycler_view);
            festivitiesRecyclerView.SetLayoutManager(new LinearLayoutManager(this));
            festivitiesRecyclerView.SetAdapter(festivityAdapter);

            ItemTouchHelper.Callback callback = new ItemTouchHelperCallback(festivityAdapter);
            itemTouchHelper = new ItemTouchHelper(callback);
            itemTouchHelper.AttachToRecyclerView(festivitiesRecyclerView);

            festivityAdapter.ItemClick += OnItemClick;

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.add_festivity_fab);
            fab.AttachToRecyclerView(festivitiesRecyclerView);
            fab.Click += (sender, args) =>
            {
                //Intent intent = new Intent(this, typeof(FestivityActivity));
                //StartActivity(intent);
                if(AzureBackend.festivities.Count < 8)
                {
                    AzureBackend.AddFestivity(-1, UpdateFestivityList);
                }
            };

            Button testButton = FindViewById<Button>(Resource.Id.testbutton);
            testButton.Click += delegate
            {
                if (AzureBackend.festivities.Count > 0)
                {
                    AzureBackend.DeleteFestivity(AzureBackend.festivities.Count - 1, UpdateFestivityList);
                }
                //WeightDialogFragment frag = new WeightDialogFragment();
                //frag.Show(FragmentManager, WeightDialogFragment.TAG);
            };

            //Touch, name from unix
            AzureBackend.Touch(this, UpdateFestivityList);
        }

        #region UI Stuff

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
            //int itemNum = position + 1;
            //Toast.MakeText(this, "This is item " + itemNum, ToastLength.Short).Show();

            //Jury rig start next activity with selected festivity saved.
            AzureBackend.currentFestivity = AzureBackend.festivities[position];
            AzureBackend.GetBeverages();
            Intent intent = new Intent(this, typeof(FestivityActivity));
            StartActivity(intent);
        }

        #endregion

        #region Backend
        public void UpdateFestivityList()
        {
            festivities.Clear();
            foreach(Festivity festivity in AzureBackend.festivities)
            {
                festivities.Add(festivity);
            }
            festivityAdapter.NotifyDataSetChanged();
            festivityAdapter.NotifyItemChanged(0);
            //Toast.MakeText(this, "Test text." + festivities.Count, ToastLength.Short).Show();
        }
        #endregion
    }
}


