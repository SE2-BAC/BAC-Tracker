using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using BAC_Tracker.Model;

namespace BAC_Tracker.Droid.Adapters
{
    public class DrinksViewHolder : RecyclerView.ViewHolder
    {
        public TextView mTime;
        public TextView mName;
        public TextView mAmount;
        public TextView mAlcoholPercent;

        public DrinksViewHolder(View itemView, Action<int> listener)
            : base(itemView)
        {
            // Locate and cache view references:
            mTime = itemView.FindViewById<TextView>(Resource.Id.textTime);
            mName = itemView.FindViewById<TextView>(Resource.Id.textName);
            mAmount = itemView.FindViewById<TextView>(Resource.Id.textAmount);
            mAlcoholPercent = itemView.FindViewById<TextView>(Resource.Id.textAlcoholPercent);

            // Detect user clicks on the item view and report which item
            // was clicked (by layout position) to the listener:
            itemView.Click += (sender, e) => listener(base.LayoutPosition);
        }
    }

    public class DrinksAdapter : RecyclerView.Adapter
    {
        // Event handler for item clicks:
        public event EventHandler<int> ItemClick;

        public List<BeverageDetails> currDrinks;

        public DrinksAdapter(List<BeverageDetails> currDrinks)
        {
            this.currDrinks = currDrinks;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            // Inflate the ListItem View
            View itemView = LayoutInflater.From(parent.Context).
                        Inflate(Resource.Layout.DrinkListItem, parent, false);

            // Create a ViewHolder to find and hold these view references, and 
            // register OnClick with the view holder:
            DrinksViewHolder vh = new DrinksViewHolder(itemView, OnClick);
            return vh;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            DrinksViewHolder vh = holder as DrinksViewHolder;

            // Set the TextViews in this ViewHolder's ListItem
            // from this position in the data:

            vh.mTime.Text = DateTime.Now.ToString();
            vh.mName.Text = currDrinks[position].Model;
            vh.mAmount.Text = "30 fl. oz";
            vh.mAlcoholPercent.Text = "0.1%";
        }

        // Return the number of items:
        public override int ItemCount
        {
            get { return currDrinks.Count; }
        }

        void OnClick(int position)
        {
            if (ItemClick != null)
                ItemClick(this, position);
        }
    }
}