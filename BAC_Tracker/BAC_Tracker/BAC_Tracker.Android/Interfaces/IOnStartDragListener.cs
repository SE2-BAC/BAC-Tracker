using System;
using Android.Support.V7.Widget;

namespace BAC_Tracker.Droid.Interfaces
{
    public interface IOnStartDragListener
    {
        /// <summary>
        /// Called when a view is requesting a start of a drag.
        /// </summary>
        /// <param name="viewHolder">The holder of the view to drag.</param>
        void OnStartDrag(RecyclerView.ViewHolder viewHolder);
    }
}