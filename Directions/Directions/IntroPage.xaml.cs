using System;
using Xamarin.Forms;
using Directions;
using STDBns;
using BTWKns;
using UNDBns;
using System.IO;
using SQLite;

namespace Directions
{
    public partial class IntroPage : ContentPage
    {
        public IntroPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            // Select the User 1st language for app processing 
            var cntlCursor = await App.Database.UNDBsGetAsync();
            var wrkLanguage = "  ";
            foreach (var s in cntlCursor)
            {
                wrkLanguage = s.Lang1st;
                break;
            }
            // Select matching STDB by levelid and language for Title Display 
            var wrkLeveLId = "999";
            var wrkDetailText = " ";
            var btdbCursor = await App.Database.STDBsGetAsync(wrkLanguage, wrkLeveLId);
            foreach (var b in btdbCursor)
            {
                wrkDetailText = b.DetailText;
                break;
            }
            Title = wrkDetailText;
            // Select Intro menu items to display bound
            wrkLeveLId = "0";
            sTDBIntrolistView.ItemsSource = await App.Database.STDBsGetIntroAsync(wrkLanguage, wrkLeveLId);
        }
        async void OnsTDBIntroItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                App.Database.ANWKDropTableAsync();
                App.Database.BTWKDropTableAsync();
                await Navigation.PushAsync(new DetailPage
                {
                    BindingContext = e.SelectedItem as STDB
                });
            }
        }
    }
}
