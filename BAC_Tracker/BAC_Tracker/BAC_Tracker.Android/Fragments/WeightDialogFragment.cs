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

namespace BAC_Tracker.Droid.Fragments
{
    public class WeightDialogFragment : DialogFragment
    {
        public static readonly string TAG = "X:" + typeof(WeightDialogFragment).Name.ToUpper();

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            AlertDialog.Builder builder = new AlertDialog.Builder(Activity);
            var inflater = Activity.LayoutInflater;

            var dialogView = inflater.Inflate(Resource.Layout.dialog_weight, null);

            if (dialogView != null)
            {
                NumberPicker weightPicker = dialogView.FindViewById<NumberPicker>(Resource.Id.weightPicker);
                weightPicker.MinValue = 70;
                weightPicker.MaxValue = 300;
                weightPicker.WrapSelectorWheel = true;
            }

            builder.SetView(dialogView);

            builder.SetMessage(Resource.String.gender)
                   .SetPositiveButton("Set", OnClick_Set)
                   .SetNegativeButton("Cancel", OnClick_Cancel);

            return builder.Create();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            return base.OnCreateView(inflater, container, savedInstanceState);
        }

        private void OnClick_Set(object sender, DialogClickEventArgs e) { }

        private void OnClick_Cancel(object sender, DialogClickEventArgs e) { }

    }
}