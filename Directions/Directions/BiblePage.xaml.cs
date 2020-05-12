using System;
using Xamarin.Forms;
using Xamarin;
using Directions;
using BTDBns;
using BTWKns;
using UNDBns;
using SQLite;
using System.IO;
using DBLoadDns;
using System.Collections.Generic;

namespace Directions
{
    public partial class BiblePage : ContentPage
    {
        public BiblePage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Set layout to Stack layout
            var layout = new StackLayout { Padding = new Thickness(5, 10) };

            // Initialize the Language Settings
            var wrkLang1st = "EN";
            var wrkLang2nd = "EN";
            var undbCursor = await App.Database.UNDBsGetAsync();
            foreach (var u in undbCursor)
            {
                wrkLang1st = u.Lang1st;
                wrkLang2nd = u.Lang2nd;
            }
            // Initialize work variables
            var cmpStyleId = 0;
 
            //Initialize the formattingString area
            var formattedString = new FormattedString();

            // Set Search value for QueryAsync

            var btwkcntlCursor = await App.Database.BTWKsGetAsyncSI(cmpStyleId);
            foreach (var s in btwkcntlCursor)
            {
                cmpStyleId = Convert.ToInt32(s.ChapterNo);
            }
            // BTWK loop for bible text 
            var btwkCursor = await App.Database.BTWKsGetAsyncSI(cmpStyleId);

            foreach (var s in btwkCursor)
            {
                // Language 1st processing
                var wrkBookName = s.BookName;
                var wrkChapterNo = Convert.ToInt32(s.ChapterNo);
                var wrkVerseNoFrom = Convert.ToInt32(s.VerseNoFrom);
                var wrkVerseNoTo = Convert.ToInt32(s.VerseNoTo);
                var enbt1Cursor = await App.Database.BTDBsGetAsync(wrkLang1st, wrkBookName, wrkChapterNo, wrkVerseNoFrom, wrkVerseNoTo);
                foreach (var b in enbt1Cursor)
                {
                    formattedString = new FormattedString();
                    formattedString.Spans.Add(new Span { Text = b.BookNameSml + " " + b.ChapterNo + ":" + b.VerseNo, ForegroundColor = Color.Red, FontAttributes = FontAttributes.Bold, FontSize = 18 });
                    layout.Children.Add(new Label { FormattedText = formattedString });
                    layout.Children.Add(new Label { Text = b.VerseText, FontSize = 18 });
                }
                // Language 2nd processing
                if (wrkLang1st != wrkLang2nd)
                {
                    var enbt2Cursor = await App.Database.BTDBsGetAsync(wrkLang2nd, wrkBookName, wrkChapterNo, wrkVerseNoFrom, wrkVerseNoTo);
                    foreach (var b in enbt2Cursor)
                    {
                        formattedString = new FormattedString();
                        formattedString.Spans.Add(new Span { Text = b.BookNameSml + " " + b.ChapterNo + ":" + b.VerseNo, ForegroundColor = Color.Red, FontAttributes = FontAttributes.Bold, FontSize = 18 });
                        layout.Children.Add(new Label { FormattedText = formattedString });
                        layout.Children.Add(new Label { Text = b.VerseText, FontSize = 18 });
                    }
                }
                layout.Children.Add(new Label { Text = "--------------------------------------", FontSize = 18 });

            }
            ScrollView scrollView = new ScrollView();
            scrollView.Content = layout;
            this.Content = scrollView;
        }
    }
}
