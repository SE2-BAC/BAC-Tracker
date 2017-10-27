using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SQLite;

namespace BAC_Tracker
{
    class DataManager
    {
        /***********************************************Person Database***************************************/
        //Create Database using the Person class
        public void CreatePersonDB()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Person.db");
            var db = new SQLiteConnection(dbPath);
        }

        //Create table
        public void CreatePersonTable()
        {
            try
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Person.db");
                var db = new SQLiteConnection(dbPath);
                db.CreateTable<Person>();
            }
            catch (Exception ex)
            {
                return "error" + ex.Message;
            }
        }



        //Add record
        public void InsertRecord(string gender, int weight)
        {
            try
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Person.db");
                var db = new SQLiteConnection(dbPath);

                Person settings = new Person();
                settings.Gender = gender;
                settings.Weight = weight;
                db.Insert(settings);

            }
            catch (Exception ex)
            {
                return "error" + ex.Message;
            }
        }

        //Retrieve Gender 
        public string RetrieveGender()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Person.db");
            var db = new SQLiteConnection(dbPath);

            Person person = new Person();

            var item = db.Get<Person>(1);

            return item.Gender;
        }

        //Retrieve Weight
        public int RetreiveWeight()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Person.db");
            var db = new SQLiteConnection(dbPath);

            Person person = new Person();

            var item = db.Get<Person>(1);

            return item.Weight;
        }

        //Update settings
        public void UpdateRecord(int id, string gender, int weight)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Person.db");
            var db = new SQLiteConnection(dbPath);

            var item = db.Get<Person>(id);
            item.Gender = gender;
            item.Weight = weight;
            db.Update(item);
            
        }

        /******************************************************Beverage database**********************************************/

        //Create Database using the Beverage
        public void CreateBeverageDB()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Beverage.db");
            var db = new SQLiteConnection(dbPath);
        }

        //Create table
        public void CreateDrinkTable()
        {
            try
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Beverage.db");
                var db = new SQLiteConnection(dbPath);
                db.CreateTable<Beverage>();
            }
            catch (Exception ex)
            {
                return "error" + ex.Message;
            }
        }



        //Add record (10 attributes)
        public void InsertRecord(int id, DateTime startTime, DateTime finishTime, string type, double amount, double completed_percentage, double alcohol_percentage, string make, string model, double volume_percentage_completed)
        {
            try
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Beverage.db");
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
                return "error" + ex.Message;
            }
        }

        //Retrieve all data by event_id (foreign key to Beverage table)
        //Still working on how to get an Event to display with the foreignkey constraint. Will test this this upcoming week.
        public string RetrieveEvent(int event_id)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Beverage.db");
            var db = new SQLiteConnection(dbPath);

            var item = db.Get<Beverage>(event_id);
            var query = db.Query<Beverage>("Select event_id, created_at FROM Beverage, Event where Id=eventid");

            return item;
        }

    
        //Will ask group if this is needed.
        //Update settings
        public void UpdateRecord(int id, DateTime startTime, DateTime finishTime, string type, double amount, double completed_percentage, double alcohol_percentage, string make, string model, double volume_percentage_completed)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Beverage.db");
            var db = new SQLiteConnection(dbPath);

            var item = db.Get<Beverage>(id);
            
            db.Update(item);

        }

        /******************************************************Event Table**************************************************/
        //Create Database using the Event class
        public void CreateEventDB()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Event.db");
            var db = new SQLiteConnection(dbPath);
        }

        //Create table
        public void CreateEventTable()
        {
            try
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Event.db");
                var db = new SQLiteConnection(dbPath);
                db.CreateTable<Event>();
            }
            catch (Exception ex)
            {
                return "error" + ex.Message;
            }
        }



    }






    



}
