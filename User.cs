using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using Android.Runtime;
using System;
using Android.Content;
using SQLite;

public class Person
{
    [PrimaryKey]
    public int ID { get; set; }
    public string Gender { get; set; }
    public int Weight { get; set; }
    public int Age { get; set; }
    public string Body_Type { get; set; }

}