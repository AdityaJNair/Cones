using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Cones.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace Cones.Views
{
    class QuoteView : ContentPage
    {
        public QuoteView()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            QuoteGenView();

        }

        private async void QuoteGenView()
        {
            BackgroundColor = Color.FromRgb(253, 240, 197);
            var quotelabel = new Label();
            quotelabel.TextColor = Color.Black;
            quotelabel.FontSize = 30;
            quotelabel.FontAttributes = FontAttributes.Bold;
            quotelabel.HorizontalTextAlignment = TextAlignment.Center;

            var quoteAuthorLabel = new Label();
            quoteAuthorLabel.TextColor = Color.Black;
            quoteAuthorLabel.FontSize = 20;
            quoteAuthorLabel.FontAttributes = FontAttributes.Italic;
            quoteAuthorLabel.HorizontalTextAlignment = TextAlignment.Center;

            var stackmain = new StackLayout();
            Content = stackmain;
            stackmain.VerticalOptions = LayoutOptions.CenterAndExpand;
            stackmain.Spacing = 20;
            stackmain.Padding = 50;


            stackmain.Children.Add(quotelabel);
            stackmain.Children.Add(quoteAuthorLabel);

            Quote q = await GetQuotesAsync();
            Content = stackmain;
            quotelabel.Text = "\""+q.quoteText+"\"";
            quoteAuthorLabel.Text = q.quoteAuthor;
        }

        public async Task<Quote> GetQuotesAsync()
        {
            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Spacing = 20,
                Padding = 50,
                BackgroundColor = Color.FromRgb(253, 240, 197),
                Children =
                {
                    new ActivityIndicator
                    {
                        Color = Color.Red,
                        IsRunning = true
                    }
                }
            };
            var requestUrl =
                "http://api.forismatic.com/api/1.0/?method=getQuote&key=457653&format=json&lang=en";

            var httpClient = new HttpClient();

            var userJson = await httpClient.GetStringAsync(requestUrl);

            var quote = JsonConvert.DeserializeObject<Quote>(userJson);

            return quote;
        }
    }
}
