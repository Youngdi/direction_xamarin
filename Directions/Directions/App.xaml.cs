using System;
using System.IO;
using Xamarin.Forms;
using UNDBns;
using Directions;
using DBLoadDns;


namespace Directions
{
    public partial class App : Application
    {
        static DBLoadD database;

        public static DBLoadD Database
        {
            get
            {
                if (database == null)
                {
                     var path = "";
                     var dbName = "Directions.db";
                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        var docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                        var libFolder = Path.Combine(docFolder, "..", "Library", "Databases");
                        path = Path.Combine(libFolder, dbName);
                        var pathIsExist = File.Exists(path);
                    }
                    else if (Device.RuntimePlatform == Device.Android)
                    {
                       path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), dbName);
                    }
 
                    database = new DBLoadD(path);
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new TocPage())
            // BarBackgroundColor = Color.FromHex("#0E547C"), 
            {
                BarBackgroundColor = Color.Blue,
                Title = "Directions"
            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        public static class GlobalVariables
        {
            public static string GlobalText1 = null;
            const string GblChar000 = "000";
            public static int GblFontSize = 18;
            public static string GblDataBase = null;
            const string GblChar00 = "00";
            const string GblChar10 = "10";
            const string GblChar20 = "20";
            const string GblChar30 = "30";
            const string GblChar40 = "40";
            const string GblChar41 = "41";
            const string GblChar42 = "42";
            const string GblChar50 = "50";
            const string GblChar60 = "60";
            const string GblChar61 = "61";
            const string GblChar62 = "62";
            const string GblChar90 = "90";
         }
    }
//    public class FileAccessHelper
//    {
//        public static string GetLocalFilePath(string filename)
//        {
//            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
//            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

//            if (!Directory.Exists(libFolder))
//            {
//                Directory.CreateDirectory(libFolder);
//            }

//            string dbPath = Path.Combine(libFolder, filename);

//            CopyDatabaseIfNotExists(dbPath);

//            return dbPath;
//        }

//        private static void CopyDatabaseIfNotExists(string dbPath)
//        {
//            if (!File.Exists(dbPath))
//            {
//                var existingDb = NSBundle.MainBundle.PathForResource("people", "db3");
//                File.Copy(existingDb, dbPath);
//            }
//        }
//    }
}