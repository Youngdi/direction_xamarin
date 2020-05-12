using System;
using Xamarin.Forms;
using Directions;
using STDBns;
using UNDBns;
using ANWKns;
using BTWKns;
using System.Collections.Generic;
using System.IO;
using SQLite;
using DBLoadDns;

namespace Directions
{
    public partial class TocPage : ContentPage
    {
        public TocPage()
        {
            InitializeComponent();
            // Clean up work DBs for answers and Bible text  

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            // Select the User 1st language for app processing 
            var cntlCursor = await App.Database.UNDBsGetAsync();
            var wrkLanguage = " ";
            foreach (var s in cntlCursor)
            {
                wrkLanguage = s.Lang1st;
                break;
            }
            // Select the STDC control by 1st Language and App Translation (999) LeveLId 
            var wrkLeveLId = "999";
            var wrkDetailText = " ";
            var btdbCursor = await App.Database.STDBsGetAsync(wrkLanguage, wrkLeveLId);
            foreach (var b in btdbCursor)
             {
                wrkDetailText = b.DetailText;
                break;
            }
            Title = wrkDetailText;
            // Select the STDB chapter control by 1st language and LevelId (1000) 
            wrkLeveLId = "1000";
            sTDBToclistView.ItemsSource = await App.Database.STDBsGetAsync(wrkLanguage, wrkLeveLId);
        }

        // Transfer to Introduction Page
        async void OnIntroductionClicked(object sender, EventArgs e)
        {
             await Navigation.PushAsync(new IntroPage
            {
                BindingContext = new STDB()
            });
        }
        // Transfer to Settings Page (UNDB)
        async void OnSettingsClicked(object sender, EventArgs e)
        {
             await Navigation.PushAsync(new SettingsUpdatePage
            {
                BindingContext = new UNDB()
            });
        }
        // Method is used to Display the TOC(STDB) portion of the APP
         async void OnsTDBTocViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            BindingContext = e.SelectedItem as STDB;
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new TocDetailPage
                {
                    BindingContext = e.SelectedItem as STDB
                });
            }
        }
     }
}
