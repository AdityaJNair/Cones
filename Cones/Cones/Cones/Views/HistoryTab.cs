using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Cones.Views
{
    /// <summary>
    /// Tabbed view that shows between the order history and timeline history of the flavors recommended by the application
    /// </summary>
    class HistoryTab : TabbedPage
    {
        private string userid;
        public HistoryTab(string userid)
        {
            this.userid = userid;
            NavigationPage.SetHasNavigationBar(this, false);
            var orderdpage = new NavigationPage(new OrderHistory(userid));
            orderdpage.Title = "Orders";

            var timelinepage = new NavigationPage(new TimelineHistory(userid));
            timelinepage.Title = "Timeline";

            Children.Add(orderdpage);
            Children.Add(timelinepage);
        }
    }
}
