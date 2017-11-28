using System;
using System.Collections.Generic;
using Android.OS;
using Android.App;
using Android.Views;
using Android.Widget;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using BAC_Tracker.Model;

namespace BAC_Tracker.Droid.Classes
{
    public static class AzureBackend
    {
        //For festivity list.
        public static List<Festivity> festivities;
        //For festivity editing and beverage list.
        public static Festivity currentFestivity;
        //For beverage editing.
        public static Beverage currentBeverage;

        private static Activity currentActivity;
        private static MobileServiceClient client;
        private static IMobileServiceTable<Beverage> beverageTable;
        private static IMobileServiceTable<Festivity> festivityTable;

        //Try to init if it hasn't already been setup.
        public static async void Touch(Activity caller, Action followUp = null)
        {
            if(client == null)
            {
                CurrentPlatform.Init();
                client = new MobileServiceClient("https://bac-tracker.azurewebsites.net");
                beverageTable = client.GetTable<Beverage>();
                festivityTable = client.GetTable<Festivity>();

                try
                {
                    await GetFestivities();
                }
                catch (Exception e)
                {
                    festivities = new List<Festivity>();
                    Toast.MakeText(currentActivity, "Touch: " + e.Message, ToastLength.Long).Show();
                }

                //Test Filling.
                //if (festivities.Count <= 0)
                //{
                //    try
                //    {
                //        Random rand = new Random();
                //        await AddFestivity(1);
                //        festivities = await festivityTable.ToListAsync();
                //    }
                //    catch (Exception e)
                //    {
                //        Toast.MakeText(currentActivity, "Touch: " + e.Message, ToastLength.Long).Show();
                //    }
                //}

            }
            currentActivity = caller;

            if(followUp != null)
            {
                followUp.Invoke();
            }
        }

        #region Utility Operations

        public static async Task AddBeverage(Beverage newBeverage, Action followUp = null)
        {
            try
            {
                await beverageTable.InsertAsync(newBeverage);
                await GetBeverages();
            }
            catch (Exception e)
            {
                Toast.MakeText(currentActivity, "AddBeverage: " + e.Message, ToastLength.Long).Show();
            }
            if (followUp != null)
            {
                followUp.Invoke();
            }
        }

        public static async Task GetBeverages(Action followUp = null)
        {
            if (currentFestivity != null)
            {
                try
                {
                    currentFestivity.Beverage_List = new List<Beverage>();
                    currentFestivity.Beverage_List = await beverageTable.Where(bev => bev.FestivityID == currentFestivity.FestivityID).ToListAsync();
                }
                catch (Exception e)
                {
                    Toast.MakeText(currentActivity, "GetBeverages: " + e.Message, ToastLength.Long).Show();
                }
            }
            else
            {
                Toast.MakeText(currentActivity, "No active festivity!", ToastLength.Short).Show();
            }

            if (followUp != null)
            {
                followUp.Invoke();
            }
        }

        public static async Task UpdateBeverage(int index, Action followUp = null)
        {
            if(currentFestivity.Beverage_List.Count > index)
            {
                try
                {
                    await beverageTable.UpdateAsync(currentFestivity.Beverage_List[index]);
                    await GetBeverages();
                }
                catch (Exception e)
                {
                    Toast.MakeText(currentActivity, "UpdateBeverages: " + e.Message, ToastLength.Long).Show();
                }
            }
            if (followUp != null)
            {
                followUp.Invoke();
            }
        }

        public static async Task DeleteBeverage(int index, Action followUp = null)
        {
            if(currentFestivity.Beverage_List.Count > index)
            {
                try
                {
                    await beverageTable.DeleteAsync(currentFestivity.Beverage_List[index]);
                    await GetBeverages();
                }
                catch (Exception e)
                {
                    Toast.MakeText(currentActivity, "DeleteBeverages: " + e.Message, ToastLength.Long).Show();
                }
            }
            if (followUp != null)
            {
                followUp.Invoke();
            }
        }


        public static async Task AddFestivity(int ID = -1, Action followUp = null)
        {
            if(ID == -1)
            {
                ID = festivities.Count;
            }

            try
            {
                await festivityTable.InsertAsync(new Festivity(ID));
                await GetFestivities();
            }
            catch (Exception e)
            {
                Toast.MakeText(currentActivity, e.Message, ToastLength.Long).Show();
            }

            if(followUp != null)
            {
                followUp.Invoke();
            }
        }

        public static async Task GetFestivities(Action followUp = null)
        {
            try
            {
                festivities = new List<Festivity>();
                festivities = await festivityTable.ToListAsync();
            }
            catch(Exception e)
            {
                Toast.MakeText(currentActivity, e.Message, ToastLength.Short).Show();
            }
            if(followUp != null)
            {
                followUp.Invoke();
            }
        }

        public static async Task UpdateFestivitys(int index, Action followUp = null)
        {
            if(festivities.Count > index)
            {
                await festivityTable.UpdateAsync(festivities[index]);
                await GetFestivities();
            }
            //await GetFestivities();
            if(followUp != null)
            {
                followUp.Invoke();
            }
        }

        public static async Task DeleteFestivity(int index, Action followUp = null)
        {
            if(festivities.Count > index)
            {
                Festivity festivityToDelete = festivities[index];
                await festivityTable.DeleteAsync(festivities[index]);
                
                festivityToDelete.Beverage_List = await beverageTable.Where(bev => bev.FestivityID == festivityToDelete.FestivityID).ToListAsync();
                foreach(Beverage bev in festivityToDelete.Beverage_List)
                {
                    await beverageTable.DeleteAsync(bev);
                }

                await GetFestivities();
            }
            if (followUp != null)
            {
                followUp.Invoke();
            }

        }
        #endregion
    }
}