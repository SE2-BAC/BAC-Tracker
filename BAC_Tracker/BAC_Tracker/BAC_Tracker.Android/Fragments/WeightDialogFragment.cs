using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace BAC_Tracker.Droid.Fragments
{
    public class WeightDialogFragment : DialogFragment
    {
        public static readonly string TAG = "X:" + typeof(WeightDialogFragment).Name.ToUpper();
        NumberPicker weightPicker;
        static ISharedPreferences preferences;
        int weight;

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            preferences = PreferenceManager.GetDefaultSharedPreferences(Context);
            weight = preferences.GetInt("pref_weight", 70);
            string[] weight_opts;
            List<string> temp = new List<string>();

            int x;
            for(x = 70; x < 305; x=x+5){temp.Add((x).ToString());}
            weight_opts = temp.ToArray();

            // Create your fragment here
            AlertDialog.Builder builder = new AlertDialog.Builder(Activity);
            var inflater = Activity.LayoutInflater;

            var dialogView = inflater.Inflate(Resource.Layout.dialog_weight, null);

            if (dialogView != null)
            {
                weightPicker = dialogView.FindViewById<NumberPicker>(Resource.Id.weightPicker);
                weightPicker.MinValue = 0;
                weightPicker.MaxValue = weight_opts.Length - 1;
                weightPicker.WrapSelectorWheel = true;
                weightPicker.SetDisplayedValues(weight_opts);
                weightPicker.Value = Array.IndexOf(weight_opts, Convert.ToString(weight));
            }

            builder.SetView(dialogView);

            builder.SetMessage(Resource.String.weight)
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

        private void OnClick_Set(object sender, DialogClickEventArgs e) {
            weight = Convert.ToInt32(weightPicker.GetDisplayedValues()[weightPicker.Value]);
            ISharedPreferencesEditor editor = preferences.Edit();
            editor.PutInt("pref_weight", weight);
            editor.Commit();
        }

        private void OnClick_Cancel(object sender, DialogClickEventArgs e) { }
    }
}