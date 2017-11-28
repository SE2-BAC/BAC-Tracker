using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using SQLite;

namespace BAC_Tracker.Droid.Fragments
{
    public static class DataManager
    {
        //create the database 
        public static string CreateDB()
        {
            var output = "";
            output += "creating database if it doesn't exist";
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Settings.db");
            var db = new SQLiteConnection(dbPath);
            CreateTable();
            InsertPreference();
            output += "\nDatabase created";
            return output;
        }

        //create Model Table
        public static string CreateTable()
        {
            try
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Settings.db");
                var db = new SQLiteConnection(dbPath);
                db.CreateTable<Settings>();
                string result = "Created Table Successfully";
                return result;

            }
            catch (Exception ex)
            {
                return "error" + ex.Message;
            }
        }

        //Store gender and weight to Table
        //This is a default setting, the update method will change the row in the table when the settings are updated.
        public static string InsertPreference()
        {
            try
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Settings.db");
                var db = new SQLiteConnection(dbPath);
                Settings model = new Settings();
                model.IsMale = true;
                model.Weight = 150;
                db.Insert(model);

                return "Gender and weight saved";
            }
            catch (Exception ex)
            {
                return "error" + ex.Message;
            }
        }
        
        //Update gender
        public static void UpdateGender(int id, int ismale)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Settings.db");
            var db = new SQLiteConnection(dbPath);

            var item = db.Get<Settings>(id);
            item.IsMale = ismale;
            db.Update(item); //this should fix the error. I changed it from bool to int.

        }

        //Update weight
        public static void UpdateWeight(int id, double weight)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Settings.db");
            var db = new SQLiteConnection(dbPath);

            var item = db.Get<Settings>(id);
            item.Weight = weight;
            db.Update(item); //returns an error at runtime

        }
        //Returns gender for use of algoritm
        public static int RetrieveGender()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Settings.db");
            var db = new SQLiteConnection(dbPath);

            int output = 1;
            var table = db.Table<Settings>();

            foreach (var item in table)
            {
                output = item.IsMale;
            }
            return output;
        }

        //Returns weight for use of algorithm
        public static double RetrieveWeight()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Settings.db");
            var db = new SQLiteConnection(dbPath);

            double output = 0;
            var table = db.Table<Settings>();

            foreach (var item in table)
            {
                output += item.Weight;
            }
            return output;
        }
    }


}
