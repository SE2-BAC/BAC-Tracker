using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using Android.Graphics.Drawables;
using Android.Graphics;

namespace DermaClinic.Droid.Fragments
{
    public class DividerItemDecoration : RecyclerView.ItemDecoration
    {
        private Drawable mDivider;

        public DividerItemDecoration(Context context)
        {
            //mDivider = context.GetDrawable(Resource.Drawable.line_divider); <ABUJANDA>line_divider not in drawable?
        }
        public override void OnDraw(Canvas c, RecyclerView parent, RecyclerView.State state)
        {
            base.OnDraw(c,parent,state);

            int left = parent.PaddingLeft;
            int right = parent.Width - parent.PaddingRight;

            for (int i = 0; i < parent.ChildCount; i++)
            {
                View child = parent.GetChildAt(i);

                RecyclerView.LayoutParams parameters = (RecyclerView.LayoutParams)child.LayoutParameters;

                int top = child.Bottom + parameters.BottomMargin;
                int bottom = top + mDivider.IntrinsicHeight;

                mDivider.SetBounds(left, top, right, bottom);
                mDivider.Draw(c);
            }
        }
    }
}