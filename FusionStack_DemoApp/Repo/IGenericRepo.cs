using System;
using System.Collections.Generic;
using System.Text;

namespace FusionStack_DemoApp.Repo
{
    public interface IGenericRepo<T>
    {
        void Delete(string id);
        void Insert(T a);
        void InsertAll(IEnumerable<T> a);
        void InsertOrReplace(T a);
        SQLite.TableQuery<T> QueryTable();
        void Update(T a);
    }
}
