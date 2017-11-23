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

namespace BAC_Tracker.Droid.Adapters
{
    public class FestivityViewHolder : RecyclerView.ViewHolder
    {
        public TextView textView { get; set; }

        public FestivityViewHolder(View itemView, Action<int> listener): base(itemView)
        {
            // Locate and cache view references:
            textView = itemView.FindViewById<TextView>(Resource.Id.txtView);

            // Detect user clicks on the item view and report which item
            // was clicked (by layout position) to the listener:
            itemView.Click += (sender, e) => listener(base.LayoutPosition);
        }
    }

    public class FestivityAdapter : RecyclerView.Adapter
    {
        // Event handler for item clicks:
        public event EventHandler<int> ItemClick;

        private string[] data;

        public FestivityAdapter(string[] data)
        {
            this.data = data;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            // Inflate the ListItem View
            View itemView = LayoutInflater.From(parent.Context).
            Inflate(Resource.Layout.ListItem, parent, false);

            // Create a ViewHolder to find and hold these view references, and 
            // register OnClick with the view holder:
            FestivityViewHolder vh = new FestivityViewHolder(itemView, OnClick);
            return vh;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            FestivityViewHolder vh = holder as FestivityViewHolder;

            // Set the TextViews in this ViewHolder's ListItem
            // from this position in the data:

            vh.textView.Text = data[position];
        }

        // Return the number of items:
        public override int ItemCount
        {
            get { return data.Length; }
        }

        // Raise an event when the itemclick takes place:
        void OnClick(int position)
        {
            if (ItemClick != null)
                ItemClick(this, position);
        }
    }
}