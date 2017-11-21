using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using BAC_Tracker.Model;
using BAC_Tracker.Droid.Interfaces;
using BAC_Tracker.Droid.Classes;
using System.Collections.ObjectModel;

namespace BAC_Tracker.Droid.Adapters
{
    public class DrinksViewHolder : RecyclerView.ViewHolder, IItemTouchHelperViewHolder
    {
        public TextView mTime;
        public TextView mName;
        public TextView mAmount;
        public TextView mPercentCons;
        public View _itemView;

        public DrinksViewHolder(View itemView, Action<int> listener) : base(itemView)
        {
            // Locate and cache view references:
            _itemView = itemView;
            mTime = itemView.FindViewById<TextView>(Resource.Id.textTime);
            mName = itemView.FindViewById<TextView>(Resource.Id.textName);
            mAmount = itemView.FindViewById<TextView>(Resource.Id.textAmount);
            mPercentCons = itemView.FindViewById<TextView>(Resource.Id.textAlcoholPercent);

            // Detect user clicks on the item view and report which item
            // was clicked (by layout position) to the listener:
            itemView.Click += (sender, e) => listener(base.LayoutPosition);
        }

        public void OnItemClear()
        {
            _itemView.SetBackgroundColor(Color.LightGray);
        }

        public void OnItemSelected()
        {
            _itemView.SetBackgroundColor(Color.DarkGray);
        }
    }

    public class DrinksAdapter : RecyclerView.Adapter, IItemTouchHelperAdapter
    {
        // Event handler for item clicks:
        public event EventHandler<int> ItemClick;

        private ObservableCollection<Beverage> mDrinks;

        private IOnStartDragListener mStartDragListener;

        public DrinksAdapter(IOnStartDragListener dragStartListener, ObservableCollection<Beverage> Drinks)
        {
            this.mStartDragListener = dragStartListener;
            this.mDrinks = Drinks;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            // Inflate the ListItem View
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.DrinkListItem, parent, false);

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

            vh.mTime.Text = DateTime.Now.ToString("h:mm:ss tt");
            vh.mName.Text = mDrinks[position].Model;
            vh.mAmount.Text = mDrinks[position].Volume.ToString()+" fl. oz";
            vh.mPercentCons.Text = mDrinks[position].Percentage_consumed.ToString() + "%";
            vh.mName.SetOnTouchListener(new TouchListenerHelper(vh, mStartDragListener));
        }

        // Return the number of items:
        public override int ItemCount
        {
            get { return mDrinks.Count; }
        }

        void OnClick(int position)
        {
                ItemClick(this, position);
        }

        public bool OnItemMove(int fromPosition, int toPosition)
        {
            mDrinks.Move(fromPosition, toPosition);
            NotifyItemMoved(fromPosition, toPosition);
            return true;
        }

        public void OnItemDismiss(int position)
        {
            mDrinks.Remove(mDrinks.ElementAt(position));
            NotifyItemRemoved(position);
        }
    }
}