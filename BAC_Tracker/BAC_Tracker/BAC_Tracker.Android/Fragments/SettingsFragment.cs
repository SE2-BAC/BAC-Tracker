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
    public class SettingsFragment : PreferenceFragment, ISharedPreferencesOnSharedPreferenceChangeListener
    {
        GenderDialogFragment gender_frag = new GenderDialogFragment();
        WeightDialogFragment weight_frag = new WeightDialogFragment();

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Load the preference from the xml resource
            AddPreferencesFromResource(Resource.Xml.preferences);
        }

        public override void OnPause()
        {
            PreferenceManager.GetDefaultSharedPreferences(Context).UnregisterOnSharedPreferenceChangeListener(this);
            base.OnPause();
        }

        public override void OnResume()
        {
            PreferenceManager.GetDefaultSharedPreferences(Context).RegisterOnSharedPreferenceChangeListener(this);
            base.OnResume();
        }

        public override bool OnPreferenceTreeClick(PreferenceScreen preferenceScreen, Preference preference)
        {
            switch (preference.Key) {
                case "pref_gender":
                    gender_frag.Show(FragmentManager, GenderDialogFragment.TAG);
                    break;
                case "pref_weight":
                    weight_frag.Show(FragmentManager, WeightDialogFragment.TAG);
                    break;
            }

            return base.OnPreferenceTreeClick(preferenceScreen, preference);
        }

        public void OnSharedPreferenceChanged(ISharedPreferences sharedPreferences, string key)
        {
            switch (key) {
                case "pref_gender":
                    string gender = sharedPreferences.GetString(key, "Male");
                    if(gender == "Male"){
                        DataManager.UpdateGender(1, 1);
                    }
                    else{
                        DataManager.UpdateGender(1,0);
                    }
                    break;
                case "pref_weight":
                    int weight = sharedPreferences.GetInt(key, 70);
                    DataManager.UpdateWeight(1, weight);
                    break;
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}
