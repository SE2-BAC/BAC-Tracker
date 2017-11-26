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
            DialogFragment frag;
            switch (preference.Key) {
                case "pref_gender":
                    frag = new GenderDialogFragment();
                    frag.Show(FragmentManager, GenderDialogFragment.TAG);
                    break;
                case "pref_weight":
                    frag = new WeightDialogFragment();
                    frag.Show(FragmentManager, WeightDialogFragment.TAG);
                    break;
            }

            return base.OnPreferenceTreeClick(preferenceScreen, preference);
        }

        public void OnSharedPreferenceChanged(ISharedPreferences sharedPreferences, string key)
        {
            //This is where database storage will be implemented
            switch (key) {
                case "pref_gender":
                    
                    break;
                case "pref_weight":

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