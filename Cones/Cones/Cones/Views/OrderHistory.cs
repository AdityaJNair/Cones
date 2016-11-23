using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Cones.Models;
using Cones;

namespace Cones.Views
{
    class OrderHistory : ContentPage
    {
        private string userid;

        public OrderHistory(string userid)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            this.userid = userid;
            BackgroundColor = Color.FromRgb(253, 240, 197);
            createView();

        }

        public async void createView()
        {

            Label header = new Label
            {
                Text = "Previous Orders",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };

            // Define some data.
            List<IceCreamOrders> icecreamorders = await AzureManager.AzureManagerInstance.GetIceCreamOrders(userid);
            icecreamorders.Reverse();
            // Create the ListView.
            ListView listView = new ListView
            {
                // Source of data items.
                ItemsSource = icecreamorders,
                // Define template for displaying each item.
                // (Argument of DataTemplate constructor is called for 
                //      each item; it must return a Cell derivative.)
                ItemTemplate = new DataTemplate(() =>
                {
                    // Create views with bindings for displaying each property.
                    Label flavourlabel = new Label();
                    flavourlabel.SetBinding(Label.TextProperty, "flavour");

                    Label datelabel = new Label();
                    datelabel.SetBinding(Label.TextProperty,
                        new Binding("date", BindingMode.OneWay,
                            null, null, "Ordered at {0:h:mm tt 'on' dd MMMM, yyyy}"));

                    Image flavourimage = new Image();
                    flavourimage.SetBinding(Image.SourceProperty, "filename");


                    // Return an assembled ViewCell.
                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Padding = new Thickness(0, 5),
                            Orientation = StackOrientation.Horizontal,
                            Children =
                                {
                                    flavourimage,
                                    new StackLayout
                                    {
                                        VerticalOptions = LayoutOptions.Center,
                                        Spacing = 0,
                                        Children =
                                        {
                                            flavourlabel,
                                            datelabel
                                        }
                                        }
                                }
                        }
                    };
                })
            };

            // Accomodate iPhone status bar.
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

            // Build the page.

            var stackl = new StackLayout();
            Content = stackl;
            stackl.Children.Add(header);
            stackl.Children.Add(listView);
        }
    }


}
