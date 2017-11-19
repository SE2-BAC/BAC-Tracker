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
    public class DrinkFragment : DialogFragment, SeekBar.IOnSeekBarChangeListener
    {
        public static readonly string TAG = "X:" + typeof(DrinkFragment).Name.ToUpper();

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            AlertDialog.Builder builder = new AlertDialog.Builder(Activity);
            var inflater = Activity.LayoutInflater;

            var dialogView = inflater.Inflate(Resource.Layout.dialog_drinks, null);

            if (dialogView != null)
            {
                NumberPicker drinksPicker = dialogView.FindViewById<NumberPicker>(Resource.Id.drinksPicker);
                drinksPicker.MinValue = 0;
                drinksPicker.MaxValue = 5;
                drinksPicker.WrapSelectorWheel = false;
                drinksPicker.SetDisplayedValues(new string[] { "Beer", "Brandy", "Martini", "Whiskey", "Wine", "Vodka" });

                SeekBar seek = dialogView.FindViewById<SeekBar>(Resource.Id.seekBar1);
                seek.Max = 100;
                seek.SetOnSeekBarChangeListener(this);
            }

            builder.SetView(dialogView);

            builder.SetMessage(Resource.String.drinks)
                   .SetPositiveButton("Add", OnClick_Set)
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
        }

        private void OnClick_Cancel(object sender, DialogClickEventArgs e) {
        }

        //Methods below used to implement Seek Bar listener
        public void OnProgressChanged(SeekBar seekBar, int progress, bool fromUser) { }

        public void OnStartTrackingTouch(SeekBar seekBar) { }

        public void OnStopTrackingTouch(SeekBar seekBar) { }
    }
}