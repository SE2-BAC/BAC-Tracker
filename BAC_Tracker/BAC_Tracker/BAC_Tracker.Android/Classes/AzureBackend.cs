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

                await GetFestivities();

                //Test Filling.
                if (festivities.Count <= 0)
                {
                    try
                    {
                        await AddFestivity(1);
                        await AddFestivity(2);
                        await AddFestivity(3);
                        Random rand = new Random();
                        for(int i = 0; i < 9; i++)
                        {
                            await AddBeverage(new Beverage("Test" + rand.Next(1, 1000), rand.NextDouble() * 5f, "Pint" + rand.Next(1, 100), (i / 3) + 1));
                        }
                        //await AddBeverage("Test1", 50.0, "Pint", 1);
                        //await AddBeverage("Test2", 52.0, "Shot", 1);
                        //await AddBeverage("Test3", 57.0, "Beer", 1);
                        //await AddBeverage("Test4", 41.0, "Pint", 2);
                        //await AddBeverage("Test5", 62.0, "Shot", 2);
                        //await AddBeverage("Test6", 87.0, "Beer", 2);
                        //await AddBeverage("Test7", 89.0, "Pint", 3);
                        //await AddBeverage("Test8", 12.0, "Shot", 3);
                        //await AddBeverage("Test9", 27.0, "Beer", 3);
                        festivities = await festivityTable.ToListAsync();
                    }
                    catch (Exception e)
                    {
                        Toast.MakeText(currentActivity, e.Message, ToastLength.Long).Show();
                    }
                }

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
                Toast.MakeText(currentActivity, e.Message, ToastLength.Long).Show();
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
                    currentFestivity.Beverage_List = await beverageTable.Where(bev => bev.FestivityID == currentFestivity.FestivityID).ToListAsync();
                }
                catch (Exception e)
                {
                    Toast.MakeText(currentActivity, e.Message, ToastLength.Long).Show();
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

        public static async Task UpdateBeverages(int index, Action followUp = null)
        {
            if(currentFestivity.Beverage_List.Count > index)
            {
                await beverageTable.UpdateAsync(currentFestivity.Beverage_List[index]);
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
                await beverageTable.DeleteAsync(currentFestivity.Beverage_List[index]);
                await GetBeverages();
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
                await festivityTable.DeleteAsync(festivities[index]);
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