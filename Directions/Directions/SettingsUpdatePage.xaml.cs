using System;
using Xamarin.Forms;
using UNDBns;
using Directions;
using System.IO;

namespace Directions
{
    public partial class SettingsUpdatePage : ContentPage
    {
        public SettingsUpdatePage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            // Select the User 1st language for app processing 
            var cntlCursor = await App.Database.UNDBsGetAsync();
            var undb = (UNDB)BindingContext;
            var wrkId = 0; 
            foreach (var s in cntlCursor)
            {
                wrkId = s.Id;
                UserName.Text = s.UserName;
                Lang1st.Text = s.Lang1st;
                Lang2nd.Text = s.Lang2nd;
                LastPage.Text = s.LastPage;
                FontType.Text = s.FontType;
                ColorText.Text = s.ColorText;
                ColorHex.Text = s.ColorHex;
                FontSize.Text = s.FontSize.ToString();
                break;
            }
        }
            async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var undb = (UNDB)BindingContext;
            undb.UserName = UserName.Text;
            undb.Lang1st = Lang1st.Text;
            undb.Lang2nd = Lang2nd.Text;
            undb.LastPage = LastPage.Text;
            undb.FontType = FontType.Text;
            undb.ColorText = ColorText.Text;
            undb.ColorHex = ColorHex.Text;
            undb.FontSize = Int64.Parse(FontSize.Text);
            await App.Database.UNDBSaveAsync(undb);
            await Navigation.PopAsync();
        }

    }
}