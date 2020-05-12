using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Foundation;
using UIKit;

namespace Directions.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            CopyDocuments("Directions.db");
            UIApplication.Main(args, null, "AppDelegate");
        }
        public static void CopyDocuments(string databaseName)
        {
              
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }
            string dbPath = Path.Combine(libFolder, databaseName);

            // This is where we copy in the pre-created database
            if (!File.Exists(dbPath))
            {
                var existingDb = NSBundle.MainBundle.PathForResource("Directions", "db");
                File.Copy(existingDb, dbPath);
            }

        }
    }

}
