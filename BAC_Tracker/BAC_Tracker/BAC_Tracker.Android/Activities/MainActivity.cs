using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Toolbar = Android.Widget.Toolbar;
using Android.OS;
using Android.Support.V7.Widget;
using com.refractored.fab;
using Microsoft.WindowsAzure.MobileServices;
using BAC_Tracker.Droid.Classes;

namespace BAC_Tracker.Droid
{
	[Activity (MainLauncher = true, Label = "BAC_Tracker.Android", Icon = "@drawable/icon")]
	public class MainActivity : Activity
    {
        string[] mData;
        RecyclerView mRecyclerView;
        RecyclerViewAdapter mAdapter;
        RecyclerView.LayoutManager mLayoutManager;
        FloatingActionButton mFAB;


        public static MobileServiceClient MobileService = new MobileServiceClient("https://bac-tracker.azurewebsites.net");
        private IMobileServiceTable<AlcoholTest> alcoholTable;
        List<AlcoholTest> myBooze;

        protected override void OnCreate(Bundle bundle)
        {
            #region Other Stuff
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //Set our toolbar
            var mToolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetActionBar(mToolbar);
            ActionBar.Title = "BAC Tracker";

            mData = new string[] { "Item 1", "Item 2", "Item 3", "Item 4", "Item 5", "Item 6", "Item 7", "Item 8", "Item 9", "Item 10" };

            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
            mLayoutManager = new LinearLayoutManager(this);
            mRecyclerView.SetLayoutManager(mLayoutManager);

            mAdapter = new RecyclerViewAdapter(mData);
            mRecyclerView.SetAdapter(mAdapter);
            mAdapter.ItemClick += OnItemClick;

            mFAB = FindViewById<FloatingActionButton>(Resource.Id.fab);
            mFAB.AttachToRecyclerView(mRecyclerView);
            mFAB.Click += (sender, args) =>
            {
                int arbitraryLimit = 5;
                if(myBooze.Count + 1 <= arbitraryLimit)
                {
                    Toast.MakeText(this, "Adding new drink and fetching updated table...", ToastLength.Short).Show();
                    Random rand = new Random();
                    AlcoholTest newDrink = new AlcoholTest("Test Booze" + rand.Next(0, 1001), (float)rand.NextDouble() * 4.0f, rand.Next(0, 2) == 0 ? false : true);
                    AddNewAndFetchTable(newDrink);
                }
                else
                {
                    FindViewById<TextView>(Resource.Id.textView1).Text = "The table has exceed the arbitrary limit of " + arbitraryLimit + " I set up and is being cleared.";
                    ClearTable();
                }
            };
            #endregion

            CurrentPlatform.Init();
            myBooze = new List<AlcoholTest>();
            GetTable();
        }

        public async void GetTable()
        {
            try
            {
                alcoholTable = MobileService.GetTable<AlcoholTest>();
                myBooze = await alcoholTable.ToListAsync();

                UpdateDisplayTableContents();
            }
            catch(Exception e)
            {
                FindViewById<TextView>(Resource.Id.textView1).Text = e.Message;
            }
        }

        //[Java.Interop.Export()]
        public async void AddAlcohol(AlcoholTest item)
        {
            if (MobileService == null)
                FindViewById<TextView>(Resource.Id.textView1).Text = "The fuck";
            //return;

            try
            {
                await alcoholTable.InsertAsync(item);
            }
            catch (Exception e)
            {
                FindViewById<TextView>(Resource.Id.textView1).Text = e.Message;
            }
        }

        public async void AddNewAndFetchTable(AlcoholTest item)
        {
            if (MobileService == null)
                FindViewById<TextView>(Resource.Id.textView1).Text = "The fuck";

            try
            {
                await alcoholTable.InsertAsync(item);
                alcoholTable = MobileService.GetTable<AlcoholTest>();
                myBooze = await alcoholTable.ToListAsync();
                UpdateDisplayTableContents();
            }
            catch (Exception e)
            {
                FindViewById<TextView>(Resource.Id.textView1).Text = e.Message;
            }
        }

        public void UpdateDisplayTableContents()
        {
            if(myBooze.Count <= 0)
            {
                FindViewById<TextView>(Resource.Id.textView1).Text = "The table is currently empty...";
                return;
            }

            string output = "Items in " + alcoholTable.TableName + ":\n";
            foreach (AlcoholTest item in myBooze)
            {
                output += item.Name + "| Volume: " + item.Volume + " | Finished: " + item.Finished + "\n";
            }
            FindViewById<TextView>(Resource.Id.textView1).Text = output;
        }

        public async void ClearTable()
        {
            try
            {
                foreach(AlcoholTest drink in myBooze)
                {
                    await alcoholTable.DeleteAsync(drink);
                }
                myBooze = new List<AlcoholTest>();
                //Wait 3 seconds before showing the dialog.
                await Task.Delay(3000);
                //await Task.Run(async () =>
                //{
                //    await Task.Delay(3000);
                //});
                GetTable();
            }
            catch(Exception e)
            {
                FindViewById<TextView>(Resource.Id.textView1).Text = e.Message;
            }
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
                    StartActivity(typeof(SettingsActivity));
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


