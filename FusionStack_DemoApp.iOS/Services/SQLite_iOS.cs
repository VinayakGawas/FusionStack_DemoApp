using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Foundation;
using FusionStack_DemoApp.Interface;
using FusionStack_DemoApp.iOS.Services;
using SQLite;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_iOS))]
namespace FusionStack_DemoApp.iOS.Services
{
    public class SQLite_iOS : IDBInterface
    {
        SQLiteConnection conn;
        public SQLiteConnection GetConnection()
        {
            InitializeDB();
            return conn;
        }

        public void InitializeDB()
        {
            string filename = "TestDB.db3";
            var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), filename);
            conn = new SQLite.SQLiteConnection(path);
        }
    }
}