using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace BAC_Tracker.Droid.Fragments
{
    public class AlertDialogFragment : DialogFragment
    {
        public static readonly string TAG = "X:" + typeof(AlertDialogFragment).Name.ToUpper();

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            AlertDialog.Builder builder = new AlertDialog.Builder(Activity);
            var inflater = Activity.LayoutInflater;

            var dialogView = inflater.Inflate(Resource.Layout.dialog_alert, null);

            if (dialogView != null)
            {
                TextView alertText = dialogView.FindViewById<TextView>(Resource.Id.alertContent);
                alertText.Text = "Your BAC surpasses the maximum BAC and/or federal BAC limit of 0.08%";
            }

            builder.SetView(dialogView);

            builder.SetMessage(Resource.String.warning)
                   .SetPositiveButton("Ok", OnClick_Set)
                   .SetNegativeButton("Cancel", OnClick_Cancel);

            return builder.Create();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            return base.OnCreateView(inflater, container, savedInstanceState);
        }

        private void OnClick_Set(object sender, DialogClickEventArgs e){}

        private void OnClick_Cancel(object sender, DialogClickEventArgs e) { }
    }
}