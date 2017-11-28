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
        View itemView;
        public TextView drinkName { get; set; }
        public TextView drinkTime { get; set; }
        public TextView drinkContent { get; set; }
        public TextView drinkAlcoholContent { get; set; }

        public DrinksViewHolder(View itemView, Action<int> listener) : base(itemView)
        {
            // Locate and cache view references:
            this.itemView = itemView;
            drinkName = itemView.FindViewById<TextView>(Resource.Id.drink_name);
            drinkTime = itemView.FindViewById<TextView>(Resource.Id.drink_time);
            drinkContent = itemView.FindViewById<TextView>(Resource.Id.drink_content);
            drinkAlcoholContent = itemView.FindViewById<TextView>(Resource.Id.drink_alcohol_content);

            // Detect user clicks on the item view and report which item
            // was clicked (by layout position) to the listener:
            itemView.Click += (sender, e) => listener(base.LayoutPosition);
        }

        public void OnItemClear(){}

        public void OnItemSelected(){}
    }

    public class DrinksAdapter : RecyclerView.Adapter, IItemTouchHelperAdapter
    {
        // Event handler for item clicks:
        public event EventHandler<int> ItemClick;
        private ObservableCollection<Beverage> drinks;
        private IOnStartDragListener startDragListener;

        public DrinksAdapter(IOnStartDragListener startDragListener, ObservableCollection<Beverage> drinks)
        {
            this.drinks = drinks;
            this.startDragListener = startDragListener;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            // Inflate the RecyclerItem View
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.drink_list_item, parent, false);

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

            vh.drinkName.Text = drinks[position].Model;
            vh.drinkTime.Text = drinks[position].StartTime.ToString("h:mm:ss tt");
            vh.drinkContent.Text = drinks[position].Volume.ToString()+" fl. oz";
            vh.drinkAlcoholContent.Text = (drinks[position].Alcohol_percentage).ToString() + "%";
            vh.drinkName.SetOnTouchListener(new TouchListenerHelper(vh, startDragListener));
        }

        // Return the number of items:
        public override int ItemCount
        {
            get { return drinks.Count; }
        }

        void OnClick(int position)
        {
                ItemClick(this, position);
        }

        public bool OnItemMove(int fromPosition, int toPosition)
        {
            drinks.Move(fromPosition, toPosition);
            NotifyItemMoved(fromPosition, toPosition);
            return true;
        }

        public void OnItemDismiss(int position)
        {
            drinks.Remove(drinks.ElementAt(position));
            NotifyItemRemoved(position);
            AzureBackend.DeleteBeverage(position);
        }
    }
}