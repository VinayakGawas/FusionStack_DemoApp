using FusionStack_DemoApp.Interface;
using FusionStack_DemoApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace FusionStack_DemoApp.Helpers
{
    public static class DBHelper
    {

        public static void CreateTables()
        {
            SQLiteConnection dbConn = DependencyService.Get<IDBInterface>().GetConnection();
            var StudentTableInfo = dbConn.GetTableInfo(nameof(StudentInfo));
            if (!StudentTableInfo.Any())
            {
                dbConn.CreateTable<StudentInfo>();
            }

           
            var UserTableInfo = dbConn.GetTableInfo(nameof(User));
            if (!UserTableInfo.Any())
            {
                dbConn.CreateTable<User>();
            }
        }
        public static void DeleteTables()
        {
            SQLiteConnection dbConn = DependencyService.Get<IDBInterface>().GetConnection();
            dbConn.DropTable<StudentInfo>();
            dbConn.DropTable<User>();
        }

    }
}
