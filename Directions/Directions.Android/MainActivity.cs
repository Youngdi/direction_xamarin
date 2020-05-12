using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using System.Threading.Tasks;

namespace Directions.Droid
{
    [Activity(Label = "Directions", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        // Set the Database Name to be used
        private const string databaseName = "Directions.db";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            Console.WriteLine("MAc-0010-OC01");
            // Copy the Selected Database and place into memory
            CopyDocuments();
            Console.WriteLine("MAc-0010-OC02");
            // Set the view environment
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public static void CopyDocuments()
        {
            var dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), databaseName);
            Console.WriteLine("MAc-0011-CD01");
            if (!File.Exists(dbPath))
            {
                Console.WriteLine("MAc-0011-FE02");
                using (var br = new BinaryReader(Application.Context.Assets.Open(databaseName)))
                {
                    using (var bw = new BinaryWriter(new FileStream(dbPath, FileMode.Create)))
                    {
                        byte[] buffer = new byte[2048];
                        int length = 0;
                        while ((length = br.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            bw.Write(buffer, 0, length);
                        }
                    }
                }
            }
        }
        protected override void OnSaveInstanceState(Bundle outState)
        {
            // always call the base implementation!
            base.OnSaveInstanceState(outState);

        }
        protected void onRestoreInstanceState(Bundle outstate)
        {
            onRestoreInstanceState(outstate);
        }
    }
}
        