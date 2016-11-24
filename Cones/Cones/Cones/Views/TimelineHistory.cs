using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Cones.Models;

namespace Cones.Views
{
    class TimelineHistory : ContentPage
    {
        private string userid;

        public TimelineHistory(string userid)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            this.userid= userid;
            //BackgroundColor = Color.FromRgb(253, 240, 197);
            createView();
        }

        public async void createView()
        {

            Label header = new Label
            {
                Text = "Photo recommendations",
                FontAttributes = FontAttributes.Bold,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };

            // Define some data.
            List<Timeline> timelinehistory = await AzureManager.AzureManagerInstance.GetTimelines(userid);
            timelinehistory.Reverse();
            // Create the ListView.
            ListView listView = new ListView
            {
                // Source of data items.
                ItemsSource = timelinehistory,
                // Define template for displaying each item.
                // (Argument of DataTemplate constructor is called for 
                //      each item; it must return a Cell derivative.)
                ItemTemplate = new DataTemplate(() =>
                {

                    Label feeling = new Label();
                    feeling.SetBinding(Label.TextProperty, "EmotionDate");

                    // Create views with bindings for displaying each property.
                    Label flavourlabel = new Label();
                    flavourlabel.SetBinding(Label.TextProperty, "RecommendedText");

                    Label datelabel = new Label();
                    datelabel.SetBinding(Label.TextProperty,
                        new Binding("Date", BindingMode.OneWay,
                            null, null, "Ordered at {0:h:mm tt 'on' dd MMMM, yyyy}"));

                    Image flavourimage = new Image();
                    flavourimage.SetBinding(Image.SourceProperty, "FlavourFilename");


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
                                            feeling,
                                            flavourlabel,
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
