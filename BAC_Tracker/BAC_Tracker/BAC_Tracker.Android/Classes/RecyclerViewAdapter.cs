using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;

namespace BAC_Tracker.Droid.Classes
{
    public class RecyclerViewHolder : RecyclerView.ViewHolder
    {
        public TextView textView { get; set; }

        public RecyclerViewHolder(View itemView, Action<int> listener) 
            : base(itemView)
        {
            // Locate and cache view references:
            textView = itemView.FindViewById<TextView>(Resource.Id.txtView);

            // Detect user clicks on the item view and report which item
            // was clicked (by layout position) to the listener:
            itemView.Click += (sender, e) => listener(base.LayoutPosition);
        }
    }

    public class RecyclerViewAdapter : RecyclerView.Adapter
    {
        // Event handler for item clicks:
        public event EventHandler<int> ItemClick;

        private string[] data;

        public RecyclerViewAdapter(string[] data) {
            this.data = data;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            // Inflate the ListItem View
            View itemView = LayoutInflater.From(parent.Context).
                        Inflate(Resource.Layout.ListItem, parent, false);

            // Create a ViewHolder to find and hold these view references, and 
            // register OnClick with the view holder:
            RecyclerViewHolder vh = new RecyclerViewHolder(itemView, OnClick);
            return vh;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RecyclerViewHolder vh = holder as RecyclerViewHolder;

            // Set the TextViews in this ViewHolder's ListItem
            // from this position in the data:

            vh.textView.Text = data[position];
        }

        // Return the number of items:
        public override int ItemCount
        {
            get { return data.Length; }
        }

        // Raise an event when the item-click takes place:
        void OnClick(int position)
        {
            if (ItemClick != null)
                ItemClick(this, position);
        }
    }
}