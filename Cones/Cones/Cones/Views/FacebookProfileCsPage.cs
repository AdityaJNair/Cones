using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacebookLogin.Models;
using FacebookLogin.ViewModels;
using Xamarin.Forms;
using Device = Xamarin.Forms.Device;
using System.Diagnostics;
using Cones.Views;
using Cones.Models;
using Cones;

namespace FacebookLogin.Views
{
    public class FacebookProfileCsPage : ContentPage
    {

        /// <summary>
        /// Make sure to get a new ClientId from:
        /// https://developers.facebook.com/apps/
        /// </summary>
        private string ClientId = "1693968570915552";
        private string userid;

        public FacebookProfileCsPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new FacebookViewModel();
            BackgroundColor = Color.FromRgb(253, 240, 197);

            var apiRequest =
                "https://www.facebook.com/dialog/oauth?client_id="
                + ClientId
                + "&display=popup&response_type=token&redirect_uri=https://www.facebook.com/connect/login_success.html";

            var webView = new WebView
            {
                Source = apiRequest,
                HeightRequest = 1
            };

            webView.Navigated += WebViewOnNavigated;

            Content = webView;
        }

        private void WebViewOnNavigated(object sender, WebNavigatedEventArgs e)
        {

            var accessToken = ExtractAccessTokenFromUrl(e.Url);

            if (accessToken != "")
            {
                try
                {
                    SetPageLoading(accessToken);

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
            }
        }

        private async void SetPageLoading(string accessToken)
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

            //Binds with the view model to get the profile information async
            var vm = BindingContext as FacebookViewModel;
            //gets the facebookprofile info
            await vm.SetFacebookUserProfileAsync(accessToken);
            this.userid = vm.FacebookProfile.Id;

            Users entry = new Users(vm.FacebookProfile.Name.ToString(), vm.FacebookProfile.Gender.ToString(), vm.FacebookProfile.Id.ToString(), vm.FacebookProfile.AgeRange.Min, DateTime.Now);
            await AzureManager.AzureManagerInstance.AddUsers(entry);
            //Sends the accsestoken and facebook profile across
            CreateView(vm.FacebookProfile);
        }

        private void CreateView(FacebookProfile facebookProfile)
        {
            //BackgroundColor = Color.White;
            var scrollpage = new ScrollView();
            Content = scrollpage;

            //stack layout
            var stack = new StackLayout();
            scrollpage.Content = stack;
            stack.Orientation = StackOrientation.Vertical;
            stack.Padding = 10;
            stack.Spacing = 10;
            stack.VerticalOptions = LayoutOptions.CenterAndExpand;

            //fb profile id unique to be used in db
            var facebookprofileId = new Label();
            facebookprofileId.Text = facebookProfile.Id;
            facebookprofileId.TextColor = Color.Black;
            facebookprofileId.FontSize = 22;

            //fb profile account image
            var facebookprofileImage = new Image();
            facebookprofileImage.Source = ImageSource.FromUri(new Uri(facebookProfile.Picture.Data.Url));

            //frame for iamge
            var imageFrame = new Frame();
            imageFrame.Content = facebookprofileImage;
            //imageFrame.OutlineColor = Color.Black;
            imageFrame.VerticalOptions = LayoutOptions.Center;
            imageFrame.HasShadow = true;

            //account
            var accountlabel = new Label();
            accountlabel.Text = "Welcome to Cones " + facebookProfile.FirstName.ToString() + " " + facebookProfile.LastName.ToString();
            accountlabel.TextColor = Color.Black;
            accountlabel.FontSize = 20;
            accountlabel.FontAttributes = FontAttributes.Bold;
            accountlabel.VerticalOptions = LayoutOptions.CenterAndExpand;
            accountlabel.HorizontalOptions = LayoutOptions.CenterAndExpand;

            //welcome label
            var welcomeLabel = new Label();
            welcomeLabel.Text = "Cones";
            welcomeLabel.TextColor = Color.Black;
            welcomeLabel.FontAttributes = FontAttributes.Bold;
            welcomeLabel.FontAttributes = FontAttributes.Italic;
            welcomeLabel.FontSize = 40;
            welcomeLabel.VerticalOptions = LayoutOptions.CenterAndExpand;
            welcomeLabel.HorizontalOptions = LayoutOptions.CenterAndExpand;

            //Check history of orders button
            var checkHistory = new Button();
            checkHistory.Text = "View History";
            checkHistory.Clicked += CheckHistory_Clicked;

            //take photo button
            var takePhoto = new Button();
            takePhoto.Text = "Share your experience with a selfie!";
            takePhoto.Clicked += TakePhoto_Clicked;

            //order icecream button
            var orderIceCream = new Button();
            orderIceCream.Text = "Order online";
            orderIceCream.Clicked += OrderIceCream_Clicked;
            
            //order icecream button
            var maps = new Button();
            maps.Text = "Get Directions";
            maps.Clicked += GetDirections_Clicked;

            //adding children to stack
            //stack.Children.Add(welcomeLabel);
            stack.Children.Add(imageFrame);
            stack.Children.Add(accountlabel);
            stack.Children.Add(checkHistory);
            stack.Children.Add(takePhoto);
            stack.Children.Add(orderIceCream);
            stack.Children.Add(maps);
        }

        private async void CheckHistory_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HistoryView());
        }

        private async void TakePhoto_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Selfie());
        }

        private async void GetDirections_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MapsView());
        }

        private async void OrderIceCream_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OrdersView(this.userid));
        }

        /// <summary>
        /// Getting the access token
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private string ExtractAccessTokenFromUrl(string url)
        {
            if (url.Contains("access_token") && url.Contains("&expires_in="))
            {
                var at = url.Replace("https://www.facebook.com/connect/login_success.html#access_token=", "");

                if (Device.OS == TargetPlatform.WinPhone || Device.OS == TargetPlatform.Windows)
                {
                    at = url.Replace("http://www.facebook.com/connect/login_success.html#access_token=", "");
                }

                var accessToken = at.Remove(at.IndexOf("&expires_in="));

                return accessToken;
            }

            return string.Empty;
        }


    }
}