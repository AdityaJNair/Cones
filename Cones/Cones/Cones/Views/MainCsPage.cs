using System;
using System.Linq;
using Xamarin.Forms;
using Cones.Views;

namespace FacebookLogin.Views
{
    /// <summary>
    /// Main page that is the first view to the user. Login or view facebookpage
    /// </summary>
    public class MainCsPage : ContentPage
    {

        public MainCsPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            BackgroundImage = "cones2.png";

            //login button
            var loginButton = new Button
            {
                Text = "Login with Facebook",
                TextColor = Color.White,
                BackgroundColor = Color.FromHex("#01579B"),
            };

            //visit facebookpage button
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
                Spacing = 10,
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

        //if view facebook button is pressed
        private async void VisitFacebookPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FacebookPage());
        }

        //if login button is pressed
        private async void LoginWithFacebook_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FacebookProfileCsPage());
        }
    }
}