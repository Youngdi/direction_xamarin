using System;
using Xamarin.Forms;
using Xamarin;
using Directions;
using STDBns;
using BTWKns;
using ANWKns;
using UNDBns;
using SQLite;
using System.IO;
using DBLoadDns;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Directions
{
    public partial class DetailPage : ContentPage
    {
        public DetailPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            App.Database.ANWKCreateTableAsync();
            App.Database.BTWKCreateTableAsync();

            var cntlCursor = await App.Database.UNDBsGetAsync();
            var wrkLang1st = "EN";
            var wrkLang2nd = "EN";
            foreach (var c in cntlCursor)
            {
                wrkLang1st = c.Lang1st;
                wrkLang2nd = c.Lang2nd;
            }
            int cmpStyleId = 0;
            var tstStyleId = 999;
            var btwkCursor = await App.Database.BTWKsGetAsyncSI(cmpStyleId);
            foreach (var s in btwkCursor)
            {
                tstStyleId = 0;
                break;
            }
            if (tstStyleId == 999)
            {


                var layout = new StackLayout { Padding = new Thickness(5, 10) };

                // Get connection to Async database


                // Initialize work variables
                // Initialize Bible Work variables
                var ImbeddedVerseInd = 0;
                var ImbeddedVerseSid = 0;
                // Initialize Answer Work variables
                var ImbeddedAnswerSid = 0;
                string GlobalText2 = null;

                //Initialize the formattingString area
                var formattedString = new FormattedString();

                // Set Search value for QueryAsync
                var stdb = (STDB)BindingContext;
                var GlobalText1 = stdb.LeveLKey;

                var stdbCursor = await App.Database.STDBsQueryAsyncDT(wrkLang1st, GlobalText1);

                //List<STDB> loop
                foreach (var s in stdbCursor)
                {
                    // Set cursor iteration compare values 
                    var cmplevelkey = s.LeveLKey;
                    var cmpdetailtype = s.DetailType;

                    // Title DetailType 00 processing
                    if (cmpdetailtype == "0")
                    {
                        GlobalText2 = s.DetailText;
                        Title = GlobalText2;
                        GlobalText2 = null;
                    }
                    // Title DetailType 10 processing
                    else if (cmpdetailtype == "10")
                    {
                        GlobalText2 = s.DetailText;
                        layout.Children.Add(new Label { Text = GlobalText2, FontSize = 24, FontAttributes = FontAttributes.Bold });
                        GlobalText2 = null;
                    }
                    // Title DetailType 20 processing
                    else if (cmpdetailtype == "20")
                    {
                        GlobalText2 = s.DetailText;
                        layout.Children.Add(new Label { Text = " ", FontSize = 12 });
                        layout.Children.Add(new Label { Text = GlobalText2, FontSize = 18} );
                        GlobalText2 = null;
                    }
                    // Title DetailType 30 processing
                    else if (cmpdetailtype == "30")
                    {
//                        layout.Children.Add(new Label { Text = " ", FontSize = 12 });
                        GlobalText2 = "*  ";
                        string text2 = $"{GlobalText2} {s.DetailText}";
                        GlobalText2 = text2;
                        layout.Children.Add(new Label { Text = GlobalText2, FontSize = 18, Margin = 24 });
                        GlobalText2 = null;
                    }
                    // Title DetailType 40 processing
                    else if (cmpdetailtype == "40")
                    {
                        formattedString.Spans.Add(new Span { Text = s.DetailText, FontSize = 18 });
                        formattedString.Spans.Add(new Span { Text = " ", FontSize = 18 });
                    }
                    // Title DetailType 41 processing
                    else if (cmpdetailtype == "41")
                    {
                        //initialize
                        var icharWkText = String.Format(s.DetailVerse + "@");
                        var icharLen = 0;

                        var iref = icharWkText;
                        var ibook = " ";
                        var ichapter = 0;
                        var ifromverse = 0;
                        var itoverse = 999;
                        //initialize
                        var icharPosFrom = 0;
                        var iPosMax = icharWkText.Length;
                        //process book name
                        var icharPosCur = icharWkText.IndexOf(" ");
                        var icharPosTo = icharPosCur;
                        var icharWkText1 = icharWkText.Substring(icharPosFrom, icharPosTo);
                        ibook = icharWkText1;
                        //process chapter number
                        icharPosCur = icharWkText.IndexOf(":");
                        if (icharPosCur < 0)
                        {
                            icharPosCur = icharWkText.IndexOf("@");
                            icharPosFrom = icharPosTo + 1;
                            icharPosTo = icharPosCur;
                            icharLen = icharPosTo - icharPosFrom;
                            icharWkText1 = icharWkText.Substring(icharPosFrom, icharLen);
                            ichapter = Int32.Parse(icharWkText1);
                            ifromverse = 0;
                            itoverse = 999;
                        }
                        else
                        {
                            icharPosFrom = icharPosTo + 1;
                            icharPosTo = icharPosCur;
                            icharLen = icharPosTo - icharPosFrom;
                            icharWkText1 = icharWkText.Substring(icharPosFrom, icharLen);
                            ichapter = Int32.Parse(icharWkText1);
                            //process from verse
                            icharPosCur = icharWkText.IndexOf("-");
                            if (icharPosCur < 0)
                            {
                                ifromverse = 0;
                                itoverse = 999;
                                icharPosCur = icharWkText.IndexOf("@");
                                if (icharPosCur < 0)
                                {
                                    ifromverse = 0;
                                    itoverse = 999;
                                }
                                else
                                {
                                    icharPosFrom = icharPosTo + 1;
                                    icharPosTo = icharPosCur;
                                    icharLen = icharPosTo - icharPosFrom;
                                    icharWkText1 = icharWkText.Substring(icharPosFrom, icharLen);
                                    ifromverse = Int32.Parse(icharWkText1);
                                    itoverse = ifromverse;
                                }
                            }
                            else
                            {
                                icharPosFrom = icharPosTo + 1;
                                icharPosTo = icharPosCur;
                                icharLen = icharPosTo - icharPosFrom;
                                icharWkText1 = icharWkText.Substring(icharPosFrom, icharLen);
                                ifromverse = Int32.Parse(icharWkText1);
                                icharPosCur = icharWkText.IndexOf("@");
                                if (icharPosCur < 0)
                                {
                                    itoverse = ifromverse;
                                }
                                else
                                {
                                    icharPosFrom = icharPosTo + 1;
                                    icharPosTo = icharPosCur;
                                    icharLen = icharPosTo - icharPosFrom;
                                    icharWkText1 = icharWkText.Substring(icharPosFrom, icharLen);
                                    itoverse = Int32.Parse(icharWkText1);
                                }
                            }
                        }
                        // add verse logic here     
                        if (ImbeddedVerseInd == 0)
                        {
                            GlobalText2 = s.DetailText;
                            ImbeddedVerseInd = 1;
                            ImbeddedVerseSid = (ImbeddedVerseSid + 1);
                        }
                        else
                        {
                            string text2 = $"{GlobalText2} {s.DetailText}";
                            GlobalText2 = text2;
                        }

                        var btwk = new BTWK();
                        btwk.ButtonId = ImbeddedVerseSid;
                        btwk.BookName = ibook;
                        btwk.ChapterNo = ichapter;
                        btwk.VerseNoFrom = ifromverse;
                        btwk.VerseNoTo = itoverse;

                        await App.Database.BTWKSaveAsync(btwk);

                        formattedString.Spans.Add(new Span { Text = s.DetailText, ForegroundColor = Color.Red, FontAttributes = FontAttributes.Bold, FontSize = 18 });
                        formattedString.Spans.Add(new Span { Text = " ", FontSize = 18 });
                    }
                    // Title DetailType 42 processing
                    else if (cmpdetailtype == "42")
                    {
                        //Set Button Key Text
                        var GlobalText3 = GlobalText2;

                        string text2 = $"{GlobalText2} {s.DetailText}";
                        GlobalText2 = text2;

                        formattedString.Spans.Add(new Span { Text = s.DetailText, FontSize = 18 });
                        layout.Children.Add(new Label { Text = "  ", FontSize = 12 });

                        layout.Children.Add(new Label { FormattedText = formattedString });

                        if (ImbeddedVerseInd == 1)
                        {
                            Button CurrentButton = new Button();
                            StyleId = ImbeddedVerseSid.ToString();
                            string GlobalText4 = "********";
                            CurrentButton.Clicked += BibleButtonClicked;
                            CurrentButton.Text += GlobalText4;
                            CurrentButton.FontSize = 12;
                            CurrentButton.TextColor = Color.FromHex("#FF006400");
                            CurrentButton.HorizontalOptions = LayoutOptions.Start;
                            CurrentButton.VerticalOptions = LayoutOptions.Start;
                            CurrentButton.StyleId = ImbeddedVerseSid.ToString();
                            CurrentButton.CornerRadius = 12;
                            layout.Children.Add(CurrentButton);
                        }
                        else
                        {
                            layout.Children.Add(new Label
                            {
                                Text = "  ",
                                FontSize = 12
                            });
                            formattedString.Spans.Add(new Span
                            {
                                Text = s.DetailText,
                                FontAttributes = FontAttributes.Italic,
                                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))
                            });
                        }
                        ImbeddedVerseInd = 0;
                        GlobalText2 = null;
                        formattedString = new FormattedString();
                    }
                    // Title DetailType 50 processing
                    else if (cmpdetailtype == "50")
                    {
                        GlobalText2 = s.DetailText;
                        layout.Children.Add(new Label { Text = "  ", FontSize = 12 });
                        layout.Children.Add(new Label { Text = GlobalText2, FontSize = 18, FontAttributes = FontAttributes.Bold });
                        GlobalText2 = null;
                    }
                    // Title DetailType 60 processing
                    else if (cmpdetailtype == "60")
                    {
                        formattedString.Spans.Add(new Span { Text = s.DetailText, FontSize = 18, FontAttributes = FontAttributes.Italic });
                        layout.Children.Add(new Label { FormattedText = formattedString });

                        GlobalText2 = null;
                        formattedString = new FormattedString();
                    }
                    // Title DetailType 61 processing
                    else if (cmpdetailtype == "61")
                    {
                        // add verse logic here                 

                        // add verse logic here 
                        formattedString.Spans.Add(new Span { Text = s.DetailText, FontSize = 18 });
                        layout.Children.Add(new Label { FormattedText = formattedString });
                        layout.Children.Add(new Label { Text = "  ", FontSize = 12 });

                        GlobalText2 = null;
                        formattedString = new FormattedString();
                    }
                    // Title DetailType 62 processing
                    else if (cmpdetailtype == "62")
                    {
                        formattedString.Spans.Add(new Span { Text = s.DetailText, FontSize = 18 });
                        layout.Children.Add(new Label { FormattedText = formattedString });

                        layout.Children.Add(new Label { Text = "  ", FontSize = 12 });

                        GlobalText2 = null;
                        formattedString = new FormattedString();
                    }
                    else if (cmpdetailtype == "90")
                    {

                        //changed
                        ImbeddedAnswerSid = ImbeddedAnswerSid + 1;
                        Entry CurrentEntry = new Entry();
                        StyleId = ImbeddedAnswerSid.ToString();
                        GlobalText2 = "Answer=" + StyleId;
                        CurrentEntry.Completed += EntryFieldEntered;
                        CurrentEntry.Text += s.DetailText;
                        CurrentEntry.FontSize = 18;
                        CurrentEntry.TextColor = Color.FromHex("#FF006400");
                        CurrentEntry.StyleId = ImbeddedAnswerSid.ToString();
                        layout.Children.Add(CurrentEntry);
                        GlobalText2 = null;

                        var anwk = new ANWK();
                        anwk.Language = s.Language;
                        anwk.StyleId = ImbeddedAnswerSid;
                        anwk.Sequence = Convert.ToInt32(s.Sequence);

                        await App.Database.ANWKSaveAsync(anwk);
                    }
                    else
                    {
                        GlobalText2 = "Invlaid Code";
                        layout.Children.Add(new Label { Text = "  ", FontSize = 12 });
                        layout.Children.Add(new Label { Text = GlobalText2, FontSize = 18 });
                        GlobalText2 = null;
                    }
                }
                ScrollView scrollView = new ScrollView();
                scrollView.Content = layout;
                this.Content = scrollView;
               }
        }
        async void BibleButtonClicked(object sender, EventArgs e)
        {
            Button ClickedButton = (Button)sender;
            int passbtwkId = Int32.Parse(ClickedButton.StyleId);
            // Get connection to Async database
            var btwk = new BTWK();
            btwk.ButtonId = 0;
            btwk.BookName = " ";
            btwk.ChapterNo = passbtwkId;
            btwk.VerseNoFrom = 0;
            btwk.VerseNoTo = 999;
            await App.Database.BTWKSaveAsync(btwk);

            //Transfer to BiblePage
            await Navigation.PushAsync(new BiblePage
            {
                BindingContext = new BTWK()
            });
        }
        async void EntryFieldEntered(object sender, EventArgs e)
        {

            Entry TextChanged = (Entry)sender;
            int cmpentrySId = Int32.Parse(TextChanged.StyleId);
            string cmpentryText = TextChanged.Text;

            //Control Record loop (get Language)
            var cntlCursor = await App.Database.UNDBsGetAsync();
            var cmpLang1st = "EN";
            foreach (var c in cntlCursor)
            {
                cmpLang1st = c.Lang1st;
            }

            var anwkCursor = await App.Database.ANWKsGetAsyncSI(cmpLang1st, cmpentrySId);
            //Answer Work Loop (get Sequence)
            long cmpSequence = 0;
            foreach (var a in anwkCursor)
            {
                // Set cursor iteration compare values Int32.Parse(icharWkText1);
                cmpSequence = a.Sequence;
            }
            var stdbCursor = await App.Database.STDBsQueryAsyncLS(cmpLang1st, cmpSequence);
            //List<ENST> loop
            foreach (var s in stdbCursor)
            {
                var stdb = (STDB)BindingContext;
                stdb.Sequence = s.Sequence;
                stdb.Language = s.Language;
                stdb.LeveLId = s.LeveLId;
                stdb.LeveLIdSeq = s.LeveLIdSeq;
                stdb.LeveLKey = s.LeveLKey;
                stdb.DetailType = s.DetailType;
                stdb.DetailFormat = s.DetailFormat;
                stdb.DetailVerse = s.DetailVerse;
                stdb.DetailDesc = s.DetailDesc;
                stdb.DetailText = cmpentryText;
                await App.Database.STDBUpdateAsync(stdb);
                break;

            }
        }
    }
}