using System;
using Xamarin.Forms;

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

            loginButton.Clicked += LoginWithFacebook_Clicked;

            Content = new StackLayout
            {
                Spacing = 0,
                Padding = 10,
                VerticalOptions = LayoutOptions.End,
                Orientation = StackOrientation.Vertical,
                Children =
                {
                    loginButton
                }
            };
        }

        private async void LoginWithFacebook_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FacebookProfileCsPage());
        }
    }
}