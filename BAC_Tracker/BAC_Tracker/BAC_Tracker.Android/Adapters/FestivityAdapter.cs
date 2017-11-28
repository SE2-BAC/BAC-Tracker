using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using BAC_Tracker.Droid.Classes;
using BAC_Tracker.Droid.Interfaces;
using BAC_Tracker.Model;

namespace BAC_Tracker.Droid.Adapters
{
    public class FestivityViewHolder : RecyclerView.ViewHolder, IItemTouchHelperViewHolder
    {
        View itemView;
        public TextView festivityDate { get; set; }
        public TextView festivityMaxBAC { get; set; }
        public TextView festivityBAC { get; set; }

        public FestivityViewHolder(View itemView, Action<int> listener): base(itemView)
        {
            // Locate and cache view references:
            this.itemView = itemView;
            festivityDate = itemView.FindViewById<TextView>(Resource.Id.festivity_date);
            festivityMaxBAC = itemView.FindViewById<TextView>(Resource.Id.festivity_max_BAC);
            festivityBAC = itemView.FindViewById<TextView>(Resource.Id.festivity_BAC);

            // Detect user clicks on the item view and report which item
            // was clicked (by layout position) to the listener:
            itemView.Click += (sender, e) => listener(base.LayoutPosition);
        }

        public void OnItemSelected()
        {
            
        }

        public void OnItemClear()
        {
            
        }
    }

    public class FestivityAdapter : RecyclerView.Adapter, IItemTouchHelperAdapter
    {
        // Event handler for item clicks:
        public event EventHandler<int> ItemClick;
        private ObservableCollection<Festivity> festivities;
        private IOnStartDragListener startDragListener;

        public FestivityAdapter(IOnStartDragListener startDragListener, ObservableCollection<Festivity> festivities)
        {
            this.festivities = festivities;
            this.startDragListener = startDragListener;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            // Inflate the RecyclerItem View
            View itemView = LayoutInflater.From(parent.Context).
            Inflate(Resource.Layout.festivity_list_item, parent, false);

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

            vh.festivityDate.Text = DateTime.Now.ToLongDateString();
            vh.festivityMaxBAC.Text = "Maximum BAC: " + festivities[position].Max_BAC + "%";
            vh.festivityBAC.Text = "Current BAC: "+ festivities[position].Current_BAC + "%";
            vh.festivityDate.SetOnTouchListener(new TouchListenerHelper(vh, startDragListener));
        }

        // Return the number of items:
        public override int ItemCount
        {
            get { return festivities.Count; }
        }

        // Raise an event when the itemclick takes place:
        void OnClick(int position)
        {
                ItemClick(this, position);
        }

        public bool OnItemMove(int fromPosition, int toPosition)
        {
            festivities.Move(fromPosition, toPosition);
            NotifyItemMoved(fromPosition, toPosition);
            return true;
        }

        public void OnItemDismiss(int position)
        {
            festivities.Remove(festivities.ElementAt(position));
            NotifyItemRemoved(position);
            AzureBackend.DeleteFestivity(position);
        }
    }
}