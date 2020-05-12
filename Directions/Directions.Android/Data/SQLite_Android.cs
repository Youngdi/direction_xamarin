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
using Directions.Droid.Data;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_Android))]
namespace Directions.Droid.Data
{
    public class SQLite_Android : ISQLite
    {
        public SQLite_Android() { }
        public SQLite.SQLiteConnection GetConnection()
        {
            var sqliteDatabase = "Directions.db";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, sqliteDatabase);
            var comm = new SQLite.SQLiteConnection(path);

            return comm;
        }
    }
}