using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using SQLite;

namespace BAC_Tracker
{
    class DataManager
    {
        /***********************************************Database***************************************/
        //Create Database
        public void CreateDB()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "BAC.db");
            var db = new SQLiteConnection(dbPath);
        }

        /***********************************************Person Table***************************************/
        public void CreatePersonTable()
        {
            try
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "BAC.db");
                var db = new SQLiteConnection(dbPath);
                db.CreateTable<Person>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error creating Person table" + ex.Message);
            }
        }



        //Add record
        public void InsertRecord(string gender, int weight)
        {
            try
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "BAC.db");
                var db = new SQLiteConnection(dbPath);

                Person settings = new Person();
                settings.Gender = gender;
                settings.Weight = weight;
                db.Insert(settings);

            }
            catch (Exception ex)
            {
                Console.WriteLine("error inserting record" + ex.Message);
            }
        }

        //Retrieve Gender 
        public string RetrieveGender()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "BAC.db");
            var db = new SQLiteConnection(dbPath);

            Person person = new Person();

            var item = db.Get<Person>(1);

            return item.Gender;
        }

        //Retrieve Weight
        public double RetreiveWeight()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "BAC.db");
            var db = new SQLiteConnection(dbPath);

            Person person = new Person();

            var item = db.Get<Person>(1);

            return item.Weight;
        }

        //Update settings
        public void UpdateRecord(int id, string gender, int weight)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "BAC.db");
            var db = new SQLiteConnection(dbPath);

            var item = db.Get<Person>(id);
            item.Gender = gender;
            item.Weight = weight;
            db.Update(item);
            
        }

        //Create table
        public void CreateDrinkTable()
        {
            try
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "BAC.db");
                var db = new SQLiteConnection(dbPath);
                db.CreateTable<Beverage>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error creating Drink table" + ex.Message);
            }
        }



        //Add record (10 attributes)
        public void InsertRecord(int id, DateTime startTime, DateTime finishTime, string type, double amount, double completed_percentage, double alcohol_percentage, string make, string model, double volume_percentage_completed)
        {
            try
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "BAC.db");
                var db = new SQLiteConnection(dbPath);

                Beverage drink = new Beverage();
                drink.Id = id;
                drink.startTime = startTime;
                drink.finishTime = finishTime;
                drink.type = type;
                drink.Amount = amount;
                drink.Completed_Percentage = completed_percentage;
                drink.Alcohol_percentage = alcohol_percentage;
                drink.Make = make;
                drink.Model = model;
                drink.Volume_percentage_completed = volume_percentage_completed;

                db.Insert(drink);

            }
            catch (Exception ex)
            {
                Console.WriteLine("error Inserting record into beverage table" + ex.Message);
            }
        }

        //Retrieve all data by event_id (foreign key to Beverage table)
        //Still working on how to get an Event to display with the foreignkey constraint. Will test this this upcoming week.
        public string RetrieveEvent(int event_id)
        {
            string output = "";
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "BAC.db");
            var db = new SQLiteConnection(dbPath);

            var item = db.Get<Beverage>(event_id);
            var query = db.Query<Beverage>("Select event_id, created_at FROM Beverage, Event where Id=eventid");

            foreach (var Event in query)
            {
                output += Event.ToString();
            }
            return output;
        }

    
        //Will ask group if this is needed.
        //Update settings
        public void UpdateRecord(int id, DateTime startTime, DateTime finishTime, string type, double amount, double completed_percentage, double alcohol_percentage, string make, string model, double volume_percentage_completed)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "BAC.db");
            var db = new SQLiteConnection(dbPath);

            var item = db.Get<Beverage>(id);
            
            db.Update(item);

        }

        /******************************************************Event Table**************************************************/
        //Create table
        public void CreateEventTable()
        {
            try
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "BAC.db");
                var db = new SQLiteConnection(dbPath);
                db.CreateTable<Event>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error creating Event table" + ex.Message);
            }
        }



    }






    



}
