using FusionStack_DemoApp.Interface;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FusionStack_DemoApp.Repo
{
    public class GenericRepo<T> : IGenericRepo<T> where T : new()
    {
        public GenericRepo()
        {

        }
        public void Insert(T a)
        {
            DependencyService.Get<IDBInterface>().GetConnection().Insert(a);
        }

        public void InsertOrReplace(T a)
        {
            DependencyService.Get<IDBInterface>().GetConnection().InsertOrReplace(a);
        }
        public void InsertAll(IEnumerable<T> a)
        {
            DependencyService.Get<IDBInterface>().GetConnection().InsertAll(a);
        }
        public void Update(T a)
        {
            DependencyService.Get<IDBInterface>().GetConnection().Update(a);
        }
        public TableQuery<T> QueryTable()
        {
            var a = DependencyService.Get<IDBInterface>().GetConnection().Table<T>();
            return a;
        }

        public void Delete(string id)
        {
            DependencyService.Get<IDBInterface>().GetConnection().Delete<T>(id);
        }
    }
}
