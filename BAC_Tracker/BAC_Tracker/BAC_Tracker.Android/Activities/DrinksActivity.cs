﻿using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Toolbar = Android.Widget.Toolbar;
using Android.OS;
using Android.Support.V7.Widget;
using com.refractored.fab;
using BAC_Tracker.Droid.Fragments;
using BAC_Tracker.Droid.Adapters;
using BAC_Tracker.Model;
using System.Collections.Generic;

namespace BAC_Tracker.Droid.Activities
{
    [Activity(Label = "DrinksActivity")]
    public class DrinksActivity : Activity
    {
        List<Beverage> mDrinks;
        RecyclerView mRecyclerView;
        DrinksAdapter mAdapter;
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
            ActionBar.SetHomeButtonEnabled(true);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            //ActionBar.SetIcon(Resource.Drawable.Icon);
            ActionBar.Title = "Your Drinks";

            mDrinks = new List<Beverage>();
            mDrinks.Add(new Beverage("Lightbeer", 35, "can/bottle" ));
            mDrinks.Add(new Beverage("Whiskey", 100, "whiskey"));
            mDrinks.Add(new Beverage("Vodka", 100, "shot"));

            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerViewDrinks);
            mLayoutManager = new LinearLayoutManager(this);
            mRecyclerView.SetLayoutManager(mLayoutManager);
            //mRecyclerView.AddItemDecoration(new DermaClinic.Droid.Fragments.DividerItemDecoration(this)); TODO:<ABUJANDA> Fix this line

            mAdapter = new DrinksAdapter(mDrinks);
            mRecyclerView.SetAdapter(mAdapter);
            mAdapter.ItemClick += OnItemClick;

            mFAB = FindViewById<FloatingActionButton>(Resource.Id.addDrinkFAB);
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

        protected override void OnResume()
        {
            base.OnResume();
            mAdapter.NotifyDataSetChanged();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Finish();
            return true;
        }
    }
}