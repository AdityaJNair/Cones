using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Cones.Views
{
    class TimelineHistory : ContentPage
    {
        private string userid;

        public TimelineHistory(string userid)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            this.userid= userid;
            BackgroundColor = Color.FromRgb(253, 240, 197);
        }
    }
}
