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
    public class GenderDialogFragment : DialogFragment
    {
        public static readonly string TAG = "X:" + typeof(GenderDialogFragment).Name.ToUpper();

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            AlertDialog.Builder builder = new AlertDialog.Builder(Activity);
            var inflater = Activity.LayoutInflater;

            var dialogView = inflater.Inflate(Resource.Layout.dialog_gender, null);

            if (dialogView != null) {
                NumberPicker genderPicker = dialogView.FindViewById<NumberPicker>(Resource.Id.genderPicker);
                genderPicker.MinValue = 0;
                genderPicker.MaxValue = 1;
                genderPicker.WrapSelectorWheel = true;
                genderPicker.SetDisplayedValues(new string[] { "Male", "Female"});
            }

            builder.SetView(dialogView);

            builder.SetMessage("Gender")
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