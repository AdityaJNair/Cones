using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Cones.Views
{
    /// <summary>
    /// Page that opens a webview on the Cones business facebook page
    /// </summary>
    class FacebookPage : ContentPage
    {

        public FacebookPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            var browser = new WebView();
            Content = browser;
            browser.Source = "https://www.facebook.com/Cones-1746859932306902/";
        }
    }
}
