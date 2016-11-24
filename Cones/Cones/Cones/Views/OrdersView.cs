using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Cones.Models;
using Cones;

namespace Cones.Views
{
    /// <summary>
    /// Class that holds the orders
    /// </summary>
    class OrdersView : ContentPage
    {
        private string userid;
        public OrdersView(string userid,IceCreamOrders modifyorder, string selflavour)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            this.userid = userid;

            //main scroll view
            BackgroundImage = "thin.png";

            var mainstack = new StackLayout();
            Content = mainstack;

            mainstack.Orientation = StackOrientation.Vertical;
            mainstack.Padding = 10;
            mainstack.Spacing = 10;

            //TABLES
            TableView tab = new TableView();
            tab.Intent = TableIntent.Form;
            tab.Root = new TableRoot();

            var tabsel = new TableSection("Ice Cream Flavours");

            //Chocolate iceream cell
            var chocoCell = new ImageCell();
            chocoCell.ImageSource = "Chocolate.jpg";
            chocoCell.Text = "Chocolate $4";
            chocoCell.TextColor = Color.Black;
            chocoCell.Detail = "Sweet milk chocolate";
            chocoCell.DetailColor = Color.Black;

            //strawberry icecream cell
            var strawCell = new ImageCell();
            strawCell.ImageSource = "strawberry.jpg";
            strawCell.Text = "Strawberry $4";
            strawCell.TextColor = Color.Black;
            strawCell.Detail = "Authentic strawberries";
            strawCell.DetailColor = Color.Black;

            //vanilla icecream cell
            var vanilCell = new ImageCell();
            vanilCell.ImageSource = "vanilla.jpg";
            vanilCell.Text = "Vanilla $4";
            vanilCell.TextColor = Color.Black;
            vanilCell.Detail = "Creamy vanilla";
            vanilCell.DetailColor = Color.Black;

            //french vanilla ice cream cell
            var frenchCell = new ImageCell();
            frenchCell.ImageSource = "French.png";
            frenchCell.Text = "French Vanilla $4.50";
            frenchCell.TextColor = Color.Black;
            frenchCell.Detail = "Rich in flavour";
            frenchCell.DetailColor = Color.Black;

            //cookies n cream cell
            var cookieCell = new ImageCell();
            cookieCell.ImageSource = "cookie.jpg";
            cookieCell.Text = "Cookies n Cream $5";
            cookieCell.TextColor = Color.Black;
            cookieCell.Detail = "Actual oreo cookies";
            cookieCell.DetailColor = Color.Black;

            //mango ice cream cell
            var mangoCell = new ImageCell();
            mangoCell.ImageSource = "mango.jpg";
            mangoCell.Text = "Mango $6";
            mangoCell.TextColor = Color.Black;
            mangoCell.Detail = "Fresh used mangoes";
            mangoCell.DetailColor = Color.Black;

            //mint chocolate ice cream cell
            var mintCell = new ImageCell();
            mintCell.ImageSource = "mint.jpg";
            mintCell.Text = "Mint Chocolate $6";
            mintCell.TextColor = Color.Black;
            mintCell.Detail = "Voted top 3 in world";
            mintCell.DetailColor = Color.Black;

            //rasberry ice cream cell
            var rasberryCell = new ImageCell();
            rasberryCell.ImageSource = "rasberry.jpg";
            rasberryCell.Text = "Rasberry $5";
            rasberryCell.TextColor = Color.Black;
            rasberryCell.Detail = "For the sour lovers";
            rasberryCell.DetailColor = Color.Black;

            //coffee ice cream cell
            var coffeeCell = new ImageCell();
            coffeeCell.ImageSource = "coffee.jpg";
            coffeeCell.Text = "Coffee $6";
            coffeeCell.TextColor = Color.Black;
            coffeeCell.Detail = "Smooth cappuccino-flavored";
            coffeeCell.DetailColor = Color.Black;

            //add cells to the table
            tabsel.Add(chocoCell);
            tabsel.Add(strawCell);
            tabsel.Add(vanilCell);
            tabsel.Add(frenchCell);
            tabsel.Add(mintCell);
            tabsel.Add(coffeeCell);
            tabsel.Add(mangoCell);
            tabsel.Add(rasberryCell);
            tabsel.Add(cookieCell);
            tab.Root.Add(tabsel);

            //label for showing which item on table was selected
            var label = new Label();
            if (selflavour != null)
            {
                //if order is modified
                label.Text = "Flavour selected: "+selflavour;
            } else
            {
                label.Text = "Flavour selected: None";
            }
            label.FontSize = 20;
            label.FontAttributes = FontAttributes.Bold;

            //depending on which cell tapped, display a message on the label
            vanilCell.Tapped += (s, e) => {
                label.Text = "Flavour selected: Vanilla";
            };
            chocoCell.Tapped += (s, e) => {
                label.Text = "Flavour selected: Chocolate";
            };
            strawCell.Tapped += (s, e) => {
                label.Text = "Flavour selected: Strawberry";
            };
            frenchCell.Tapped += (s, e) => {
                label.Text = "Flavour selected: French Vanilla";
            };
            mintCell.Tapped += (s, e) => {
                label.Text = "Flavour selected: Mint Chocolate";
            };
            cookieCell.Tapped += (s, e) => {
                label.Text = "Flavour selected: Cookies n Cream";
            };
            coffeeCell.Tapped += (s, e) => {
                label.Text = "Flavour selected: Coffee";
            };
            mangoCell.Tapped += (s, e) => {
                label.Text = "Flavour selected: Mango";
            };
            rasberryCell.Tapped += (s, e) => {
                label.Text = "Flavour selected: Rasberry";
            };

            //Date and time for cart
            TimePicker timePicker = new TimePicker();
            timePicker.Time = DateTime.Now.TimeOfDay;
            DatePicker datePicker = new DatePicker();
            datePicker.Format = "D";
            datePicker.VerticalOptions = LayoutOptions.CenterAndExpand;

            //if date set has already occured show error
            datePicker.DateSelected += (sender, e) =>
            {
                if (e.NewDate < e.OldDate)
                {
                    DisplayAlert("Alert", "Please select a valid date", "OK");
                    datePicker.Date = DateTime.Now;
                }
            };

            //set a time that is in the future or current
            timePicker.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == TimePicker.TimeProperty.PropertyName)
                {
                   if (datePicker.Date.Year.Equals(DateTime.Now.Year) && datePicker.Date.Month.Equals(DateTime.Now.Month) && datePicker.Date.Day.Equals(DateTime.Now.Day))
                    {
                        if(timePicker.Time.Hours < DateTime.Now.Hour)
                        {
                            timePicker.Time = DateTime.Now.TimeOfDay;
                            DisplayAlert("Alert", "Please set a valid time", "OK");
                        } else if(timePicker.Time.Hours.Equals(DateTime.Now.Hour) && timePicker.Time.Minutes < DateTime.Now.Minute)
                        {
                            timePicker.Time = DateTime.Now.TimeOfDay;
                            DisplayAlert("Alert", "Please set a valid time", "OK");
                        }
                    }
                }
            };

            //Order button
            var addOrder = new Button();
            addOrder.Text = "Send order to Cones";
            addOrder.Clicked += async (sender, e) =>
            {
                if (label.Text.Equals("Flavour selected: None"))
                {
                    //if flavour is not selected
                    await DisplayAlert("Alert", "Select a flavour", "OK");
                }
                else
                {
                    //Create a datetime object to be added in the IceCreamOrders object to be sent to the database
                    DateTime temp = new DateTime(datePicker.Date.Year, datePicker.Date.Month, datePicker.Date.Day, timePicker.Time.Hours, timePicker.Time.Minutes, timePicker.Time.Seconds, timePicker.Time.Milliseconds);
                    string flavour = label.Text.Substring(18, label.Text.Length - 18);
                    if(modifyorder == null)
                    {
                        //if new order
                        IceCreamOrders order1 = new IceCreamOrders(userid, temp, flavour, getImageName(flavour));
                        await AzureManager.AzureManagerInstance.AddIceCreamOrder(order1);
                        await DisplayAlert("Completed", "Your order has been sent through.", "OK");
                        await Navigation.PopAsync();
                    } else
                    {
                        //if modifying order
                        modifyorder.date = temp;
                        modifyorder.flavour = flavour;
                        modifyorder.filename = getImageName(flavour);
                        await AzureManager.AzureManagerInstance.UpdateIceCreamOrders(modifyorder);
                        await DisplayAlert("Completed", "Your order has been modified.", "OK");
                        await Navigation.PopAsync();
                    }


                }

            };

            //adding elemetns to the stacklayout
            mainstack.Children.Add(tab);
            mainstack.Children.Add(label);
            mainstack.Children.Add(timePicker);
            mainstack.Children.Add(datePicker);
            mainstack.Children.Add(addOrder);
        }

        //converting flavours to filenames
        public string getImageName(string flavour)
        {
            if (flavour.Equals("Vanilla"))
            {
                return "vanilla.jpg";

            }
            else if (flavour.Equals("Chocolate"))
            {
                return "Chocolate.jpg";
            }
            else if (flavour.Equals("Strawberry"))
            {
                return "strawberry.jpg";
            }
            else if (flavour.Equals("French Vanilla"))
            {
                return "French.png";
            }
            else if (flavour.Equals("Mint Chocolate"))
            {
                return "mint.jpg";
            }
            else if (flavour.Equals("Cookies n Cream"))
            {
                return "cookie.jpg";
            }
            else if (flavour.Equals("Coffee"))
            {
                return "coffee.jpg";
            }
            else if (flavour.Equals("Mango"))
            {
                return "mango.jpg";
            }
            else if (flavour.Equals("Rasberry"))
            {
                return "rasberry.jpg";
            }
            return "";
        }
    }
}
