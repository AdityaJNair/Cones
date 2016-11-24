using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Cones.Models;
using Cones;
using System.Collections.ObjectModel;

namespace Cones.Views
{
    class OrderHistory : ContentPage
    {
        private string userid;

        public OrderHistory(string userid)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            this.userid = userid;
            //BackgroundColor = Color.FromRgb(253, 240, 197);
            createView();

        }

        public async void createView()
        {

            Label header = new Label
            {
                Text = "Previous Orders",
                FontAttributes = FontAttributes.Bold,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };

            // Define some data.
            List<IceCreamOrders> icecreamorders = await AzureManager.AzureManagerInstance.GetIceCreamOrders(userid);
            icecreamorders.Reverse();
            ObservableCollection<IceCreamOrders> obicecreamorders = new ObservableCollection<IceCreamOrders>(icecreamorders);
            // Create the ListView.

            ListView listView = new ListView
            {
                // Source of data items.
                ItemsSource = obicecreamorders,
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

            listView.ItemSelected += async (s, e) =>
            {
                if (e.SelectedItem == null)
                {
                    return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
                }

                var index = (listView.ItemsSource as ObservableCollection<IceCreamOrders>).IndexOf(e.SelectedItem as IceCreamOrders);
                IceCreamOrders tmp = obicecreamorders.ElementAt(index);
                if(tmp.date < DateTime.Now)
                {
                    return;
                } else
                {
                    var result = await DisplayActionSheet("Modify this order?", "Cancel", null, "Update", "Delete"); // since we are using async, we should specify the DisplayAlert as awaiting.
                    if (result.Equals("Update")) // if it's equal to Ok
                    {
                        await Navigation.PushAsync(new OrdersView(this.userid, tmp,tmp.flavour));
                        this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 2]);
                    }
                    else if (result.Equals("Delete")) // if it's equal to Cancel
                    {
                        await AzureManager.AzureManagerInstance.DeleteIceCreamOrders(tmp);
                        obicecreamorders.Remove(tmp);
                    }
                    else
                    {
                        return;
                    }
                }

            };
        }
    }


}
