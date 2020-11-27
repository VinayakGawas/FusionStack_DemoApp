using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FusionStack_DemoApp.Droid.Services;
using FusionStack_DemoApp.Interface;
using SQLite;
using Xamarin.Forms;
using Environment = System.Environment;
[assembly: Dependency(typeof(SQLite_droid))]
namespace FusionStack_DemoApp.Droid.Services
{
    public class SQLite_droid : IDBInterface
    {
        public SQLiteConnection conn;
        public SQLiteConnection GetConnection()
        {
            InitializeDB();
            return conn;
        }

        public void InitializeDB()
        {
            string filename = "TestDB.db3";
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), filename);
            conn = new SQLite.SQLiteConnection(path);
        }
    }
}