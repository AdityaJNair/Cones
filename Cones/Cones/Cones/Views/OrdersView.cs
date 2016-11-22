using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Cones.Views
{
    class OrdersView : ContentPage
    {
        public OrdersView()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            //main scroll view
            BackgroundColor = Color.White;
            //var scrollview = new ScrollView();
            //Content = scrollview;

            var mainstack = new StackLayout();
            Content = mainstack;
            //scrollview.Content = mainstack;
            mainstack.Orientation = StackOrientation.Vertical;
            mainstack.Padding = 10;
            mainstack.Spacing = 10;
            //mainstack.VerticalOptions = LayoutOptions.CenterAndExpand;

            //TABLES
            TableView tab = new TableView();
            tab.Intent = TableIntent.Form;
            //tab.VerticalOptions = LayoutOptions.CenterAndExpand;
            tab.Root = new TableRoot();

            var tabsel = new TableSection("Ice Cream Flavours");


            var chocoCell = new ImageCell();
            chocoCell.ImageSource = "Chocolate.jpg";
            chocoCell.Text = "Chocolate";
            chocoCell.TextColor = Color.Black;
            chocoCell.Detail = "Sweet milk chocolate";
            chocoCell.DetailColor = Color.Black;

            var strawCell = new ImageCell();
            strawCell.ImageSource = "strawberry.jpg";
            strawCell.Text = "Strawberry";
            strawCell.TextColor = Color.Black;
            strawCell.Detail = "Authentic strawberries";
            strawCell.DetailColor = Color.Black;

            var vanilCell = new ImageCell();
            vanilCell.ImageSource = "vanilla.jpg";
            vanilCell.Text = "Vanilla";
            vanilCell.TextColor = Color.Black;
            vanilCell.Detail = "Creamy vanilla";
            vanilCell.DetailColor = Color.Black;

            var frenchCell = new ImageCell();
            frenchCell.ImageSource = "French.png";
            frenchCell.Text = "French Vanilla";
            frenchCell.TextColor = Color.Black;
            frenchCell.Detail = "Rich in flavour";
            frenchCell.DetailColor = Color.Black;

            var cookieCell = new ImageCell();
            cookieCell.ImageSource = "cookie.jpg";
            cookieCell.Text = "Cookies n Cream";
            cookieCell.TextColor = Color.Black;
            cookieCell.Detail = "Actual oreo cookies";
            cookieCell.DetailColor = Color.Black;

            var mangoCell = new ImageCell();
            mangoCell.ImageSource = "mango.jpg";
            mangoCell.Text = "Mango";
            mangoCell.TextColor = Color.Black;
            mangoCell.Detail = "Fresh used mangoes";
            mangoCell.DetailColor = Color.Black;

            var mintCell = new ImageCell();
            mintCell.ImageSource = "mint.jpg";
            mintCell.Text = "Mint Chocolate";
            mintCell.TextColor = Color.Black;
            mintCell.Detail = "Voted top 3 in world";
            mintCell.DetailColor = Color.Black;

            var rasberryCell = new ImageCell();
            rasberryCell.ImageSource = "rasberry.jpg";
            rasberryCell.Text = "Rasberry";
            rasberryCell.TextColor = Color.Black;
            rasberryCell.Detail = "For the sour lovers";
            rasberryCell.DetailColor = Color.Black;

            var coffeeCell = new ImageCell();
            coffeeCell.ImageSource = "coffee.jpg";
            coffeeCell.Text = "Coffee";
            coffeeCell.TextColor = Color.Black;
            coffeeCell.Detail = "Smooth cappuccino-flavored";
            coffeeCell.DetailColor = Color.Black;

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

            var label = new Label();
            label.Text = "Flavour selected: None";
            label.FontSize = 20;


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
            DatePicker datePicker = new DatePicker();
            datePicker.Format = "D";
            datePicker.VerticalOptions = LayoutOptions.CenterAndExpand;


            var addOrder = new Button();
            addOrder.Text = "Order";


            mainstack.Children.Add(tab);
            mainstack.Children.Add(label);
            mainstack.Children.Add(timePicker);
            mainstack.Children.Add(datePicker);
            mainstack.Children.Add(addOrder);
        }

        private void TappedVanillaCell(object sender, EventArgs e)
        {
           DisplayAlert("Item Selected","ha", "Ok");
        } 
    }
}
