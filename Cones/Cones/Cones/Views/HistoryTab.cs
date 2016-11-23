using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Cones.Views
{
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
