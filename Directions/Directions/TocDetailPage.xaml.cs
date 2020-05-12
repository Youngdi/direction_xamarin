using System;
using Xamarin.Forms;
using Directions;
using STDBns;
using BTWKns;
using ANWKns;
using UNDBns;
using System.IO;
using SQLite;
using System.Collections.Generic;

namespace Directions
{
    public partial class TocDetailPage : ContentPage
    {
        public TocDetailPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            // Set Binding record for processing and get Title text passed from TocPage 
            var stdb = (STDB)BindingContext;
            var wrkLanguage = stdb.Language;
            var wrkLeveLKey = stdb.LeveLKey;
            var wrkDetailText  = stdb.DetailText;
            Title = wrkDetailText;
            // Initialize wortk files for the Bible Text and the amswers 

            // Select the Sub Page Level records 
            sTDBTocDetaillistView.ItemsSource = await App.Database.STDBsGetAsync(wrkLanguage, wrkLeveLKey);
        }
        async void OnsTDBTocDetailItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            App.Database.ANWKDropTableAsync();
            App.Database.BTWKDropTableAsync();
            // Initialize wortk files for the Bible Text and the amswers 
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new DetailPage
                {
                    BindingContext = e.SelectedItem as STDB
                });
            }
        }
    }
 }
