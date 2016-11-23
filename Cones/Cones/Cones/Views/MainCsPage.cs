using System;
using System.Linq;
using Xamarin.Forms;
using Cones.Views;

namespace FacebookLogin.Views
{
    public class MainCsPage : ContentPage
    {

        public MainCsPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            BackgroundImage = "cones.png";

            var loginButton = new Button
            {
                Text = "Login with Facebook",
                TextColor = Color.White,
                BackgroundColor = Color.FromHex("#01579B"),
            };

            var visitpage = new Button
            {
                Text = "Visit our facebook page",
                TextColor = Color.Black,
                BackgroundColor = Color.FromHex("#99CC99")
            };

            loginButton.Clicked += LoginWithFacebook_Clicked;
            visitpage.Clicked += VisitFacebookPage_Clicked;

            Content = new StackLayout
            {
                Spacing = 0,
                Padding = 10,
                VerticalOptions = LayoutOptions.End,
                Orientation = StackOrientation.Vertical,
                Children =
                {
                    loginButton,
                    visitpage
                }
            };
        }

        private async void VisitFacebookPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FacebookPage());
        }

        private async void LoginWithFacebook_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FacebookProfileCsPage());
        }
    }
}