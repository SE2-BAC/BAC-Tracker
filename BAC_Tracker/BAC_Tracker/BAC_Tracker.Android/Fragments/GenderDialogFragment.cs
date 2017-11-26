using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace BAC_Tracker.Droid.Fragments
{
    public class GenderDialogFragment : DialogFragment
    {
        public static readonly string TAG = "X:" + typeof(GenderDialogFragment).Name.ToUpper();
        NumberPicker genderPicker;
        static ISharedPreferences preferences;
        string gender;

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            //Get current gender shared preference here
            preferences = PreferenceManager.GetDefaultSharedPreferences(Context);
            gender = preferences.GetString("pref_gender", "Male");
            string[] gender_opts = new string[] { "Male", "Female" };

            // Create your fragment here
            AlertDialog.Builder builder = new AlertDialog.Builder(Activity);
            var inflater = Activity.LayoutInflater;

            var dialogView = inflater.Inflate(Resource.Layout.dialog_gender, null);

            if (dialogView != null) {
                genderPicker = dialogView.FindViewById<NumberPicker>(Resource.Id.genderPicker);
                genderPicker.MinValue = 0;
                genderPicker.MaxValue = gender_opts.Length - 1;
                genderPicker.WrapSelectorWheel = false;
                genderPicker.SetDisplayedValues(gender_opts);
                genderPicker.Value = Array.IndexOf(gender_opts, gender);
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

        private void OnClick_Set(object sender, DialogClickEventArgs e)
        {
            gender = genderPicker.GetDisplayedValues()[genderPicker.Value];
            ISharedPreferencesEditor editor = preferences.Edit();
            editor.PutString("pref_gender", gender);
            editor.Commit();
        }

        private void OnClick_Cancel(object sender, DialogClickEventArgs e) { }

    }
}