using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Cones.Views
{
    class HistoryView : ContentPage
    {
        public HistoryView()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            var label = new Label();
            label.Text = "one";
            Content = label;
        }
    }
}
