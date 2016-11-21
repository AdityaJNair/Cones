using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FacebookLogin.Views;
using Xamarin.Forms;

namespace Cones
{
    public class App : Application
    {
        public App()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            // The root page of your application
            MainPage = new NavigationPage(new MainCsPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
