using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using TranslatorService.Speech;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Reflection;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace TranslatorService.Example
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class MainPage : TranslatorService.Example.Common.LayoutAwarePage
    {
        private const string CLIENT_ID = "UBTalker2013";
        private const string CLIENT_SECRET = "NIxPbADlIwuYYPn7xEZ43f64A96tr/h8C/FkGZSiKwY=";

        private SpeechSynthesizer speech;

        public MainPage()
        {
            this.InitializeComponent();

            var _Colors = typeof(Colors)
                .GetRuntimeProperties()
                .Select((x, i) => new
                {
                    Color = (Color) x.GetValue(null),
                    Name = x.Name,
                    Index = i,
                    ColSpan = 1,
                    RowSpan = 1
                });
            this.DataContext = _Colors;

            speech = new SpeechSynthesizer(CLIENT_ID, CLIENT_SECRET);
            speech.AudioFormat = SpeakStreamFormat.MP3;
            speech.AudioQuality = SpeakStreamQuality.MaxQuality;
            speech.AutoDetectLanguage = false;
            speech.AutomaticTranslation = false;

            /*
            // Source: http://social.msdn.microsoft.com/Forums/en-US/winappswithcsharp/thread/17093c72-cfce-40a2-a296-718b741b50da/
            // The ?x and ?y will be replaced with given values using String.Replace
            string template = @"
                <Button Grid.Column='?x' Grid.Row='?y' Content='Text' ></Button>
            ";

            var xStep = 20;//stkPanel.Width / cols;
            var yStep = 20;//stkPanel.Height / rows;
            int rows = 5;
            int cols = 5;

            string gridTemplate = @"
                            <Grid 
                            xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
                            xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
                            xmlns:local='using:NoiZApp'
                            xmlns:d='http://schemas.microsoft.com/expression/blend/2008'
                            xmlns:mc='http://schemas.openxmlformats.org/markup-compatibility/2006'

                            x:Name='NoteCanvas3' Margin='0,0,0,0' Width='600' Height='300'>
            ";

            string tmpString = gridTemplate + " ";

            tmpString += @"<Grid.ColumnDefinitions>";
            for (int row = 0; row < rows; row++)
            {
                tmpString += @"<ColumnDefinition/>";
            }
            tmpString += @"</Grid.ColumnDefinitions>";

            tmpString += @"<Grid.RowDefinitions>";
            for (int col = 0; col < cols; col++)
            {
                tmpString += @"<RowDefinition/>";
            }
            tmpString += @"</Grid.RowDefinitions>";

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    string tmpTemplate = new String(template.ToCharArray());

                    tmpTemplate = tmpTemplate.Replace("?x", col + "");
                    tmpTemplate = tmpTemplate.Replace("?y", row + "");

                    tmpString += tmpTemplate;
                }
            }

            tmpString += "</Grid>";

            var result = Windows.UI.Xaml.Markup.XamlReader.Load(tmpString); */

           
        }

        private object ColSpan(int i)
        {
            return 1;
        }

        private object RowSpan(int i)
        {
            return 1;
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected async override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            if (string.IsNullOrWhiteSpace(CLIENT_ID) || string.IsNullOrWhiteSpace(CLIENT_SECRET))
            {
                MessageDialog dialog = new MessageDialog("You must obtain a Client ID and Secret in order to use this application. Please visit Azure DataMarket at https://datamarket.azure.com/developer/applications to get one.\r\nThen, go to https://datamarket.azure.com/dataset/1899a118-d202-492c-aa16-ba21c33c06cb and subscribe the Microsoft Translator Service.\n", "Translator Service Example");
                await dialog.ShowAsync();
                return;
            }
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        private void SpeakButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SpeechText.Text))
                return;
            Speak_String(SpeechText.Text);
        }

        private async void Speak_String(string text)
        {
            WaitProgressBar.Visibility = Visibility.Visible;

            // Gets the audio stream.
            var stream = await speech.GetSpeakStreamAsync(text, "en-us");

            // Reproduces the audio stream using a MediaElement.
            SpeechMediaElement.SetSource(stream, speech.MimeContentType);

            WaitProgressBar.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Invoked when an item within a group is clicked.
        /// </summary>
        /// <param name="sender">The GridView (or ListView when the application is snapped)
        /// displaying the item clicked.</param>
        /// <param name="e">Event data that describes the item clicked.</param>
        void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Navigate to the appropriate destination page, configuring the new page
            // by passing required information as a navigation parameter
            dynamic _Item = e.ClickedItem;
            Speak_String(_Item.Name);
        }
    }
}
