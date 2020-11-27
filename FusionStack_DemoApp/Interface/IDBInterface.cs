using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FusionStack_DemoApp.Interface
{
    public interface IDBInterface
    {
        SQLiteConnection GetConnection();
        void InitializeDB();
    }
}
