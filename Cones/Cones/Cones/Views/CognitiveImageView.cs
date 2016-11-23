using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Emotion;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using Plugin.Media;
using Cones.Models;
namespace Cones.Views
{
    class CognitiveImageView : ContentPage
    {
        private string userid;
        private string gender;
        private int age;
        public CognitiveImageView(string userid, int age, string gender)
        {
            this.userid = userid;
            this.age = age;
            this.gender = gender;
            NavigationPage.SetHasNavigationBar(this, false);
            BackgroundColor = Color.FromRgb(253, 240, 197);
            //Main stacklayout
            var stackmain = new StackLayout();
            Content = stackmain;
            stackmain.Orientation = StackOrientation.Vertical;
            stackmain.Padding = 10;
            stackmain.Spacing = 10;
            stackmain.VerticalOptions = LayoutOptions.CenterAndExpand;

            Label top = new Label
            {
                Text = "Take a selfie and we will recommend you a flavour based on how you're feeling today!",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };

            Label header = new Label
            {
                Text = "Current mood: ___________",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };

            Label comment = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };

            Label recommended = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };

            Label filename = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };



            var UploadingIndicator = new ActivityIndicator();
            UploadingIndicator.Color = Color.Red;
            UploadingIndicator.IsRunning = false;

            var errorLabel = new Label();

            var buttonphoto = new Button();
            buttonphoto.Text = "Take Picture";
            buttonphoto.TextColor = Color.Black;
            buttonphoto.BackgroundColor = Color.Silver;

            stackmain.Children.Add(top);
            stackmain.Children.Add(header);
            stackmain.Children.Add(buttonphoto);
            stackmain.Children.Add(UploadingIndicator);
            stackmain.Children.Add(comment);
            stackmain.Children.Add(recommended);
            stackmain.Children.Add(filename);
            stackmain.Children.Add(errorLabel);


            buttonphoto.Clicked += async (s, e) =>
            {


                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Front,
                    Directory = "Cones",
                    Name = $"{DateTime.UtcNow}.jpg",
                    CompressionQuality = 92
                });

                if (file == null)
                    return;
                try
                {
                    UploadingIndicator.IsRunning = true;

                    string emotionKey = "7e4f621d3aa241b59a0d853fcf1e4035";

                    EmotionServiceClient emotionClient = new EmotionServiceClient(emotionKey);

                    var result = await emotionClient.RecognizeAsync(file.GetStream());


                    UploadingIndicator.IsRunning = false;
                    //Get values using this result[0].Scores.ToRankedList();

                    var temp = result[0].Scores;

                    header.Text = changeText(result[0].Scores.ToRankedList().ElementAt(0).Key);
                    comment.Text = comentText(result[0].Scores.ToRankedList().ElementAt(0).Key);
                    recommended.Text = recommend(result[0].Scores.ToRankedList().ElementAt(0).Key, gender, age);
                    filename.Text = getImageName(recommended.Text);

                    Timeline emo = new Timeline(header.Text, recommended.Text, filename.Text,userid);
                    await AzureManager.AzureManagerInstance.AddTimeline(emo);
                    buttonphoto.Text = "Take another";
                }
                catch (Exception ex)
                {
                    errorLabel.Text = ex.Message;
                }
            };


        }

        public string changeText(string text)
        {
            if (text.Equals("Anger"))
            {
                return "Current mood: Anger";

            }
            else if (text.Equals("Contempt"))
            {
                return "Current mood: Contemptful";
            }
            else if (text.Equals("Disgust"))
            {
                return "Current mood: Disgusted";
            }
            else if (text.Equals("Fear"))
            {
                return "Current mood: Afraid";
            }
            else if (text.Equals("Happiness"))
            {
                return "Current mood: Happy";
            }
            else if (text.Equals("Neutral"))
            {
                return "Current mood: Neutral";
            }
            else if (text.Equals("Sadness"))
            {
                return "Current mood: Sad";
            }
            else if (text.Equals("Surprise"))
            {
                return "Current mood: Suprised";
            }
            else
            {
                return "";
            }
        }

        public string comentText(string text)
        {
            if (text.Equals("Anger"))
            {
                return "Wow you look angry right now. Try this to calm you down.";

            }
            else if (text.Equals("Contempt"))
            {
                return "A bit contemptful I see. I know just the flavour to change that mood.";
            }
            else if (text.Equals("Disgust"))
            {
                return "Ignore what ever made you feel disgusted today. Try this make your day better.";
            }
            else if (text.Equals("Fear"))
            {
                return "Don't be afraid. Here something that will change all that.";
            }
            else if (text.Equals("Happiness"))
            {
                return "Someones in a good mood. Lets make it better with this.";
            }
            else if (text.Equals("Neutral"))
            {
                return "Not feeling much today? You probably want this.";
            }
            else if (text.Equals("Sadness"))
            {
                return "Don't be so sad. Here, try this to make your day better.";
            }
            else 
            {
                return "Wow, you suprised me too. Have a try of this.";
            }
        }

        public string recommend(string text, string gender, int age)
        {
            Random rnd = new Random();
            List<string> emotionflavour;
            List<string> genderflavour;
            List<string> ageflavour;

            if (text.Equals("Anger"))
            {
                emotionflavour = new List<string>();
                emotionflavour.Add("Chocolate");  
                emotionflavour.Add("Coffee");
                emotionflavour.Add("Cookies n Cream");
            }
            else if (text.Equals("Contempt"))
            {
                emotionflavour = new List<string>();
                emotionflavour.Add("Chocolate");
                emotionflavour.Add("Vanilla");
                emotionflavour.Add("Mint Chocolate");
                emotionflavour.Add("Coffee");
                emotionflavour.Add("French Vanilla");
                emotionflavour.Add("Cookies n Cream");
            }
            else if (text.Equals("Disgust"))
            {
                emotionflavour = new List<string>();
                emotionflavour.Add("Chocolate");
                emotionflavour.Add("Vanilla");
                emotionflavour.Add("Strawberry");
            }
            else if (text.Equals("Fear"))
            {
                emotionflavour = new List<string>();
                emotionflavour.Add("Mango");
                emotionflavour.Add("Rasberry");
                emotionflavour.Add("French Vanilla");
                emotionflavour.Add("Cookies n Cream");
            }
            else if (text.Equals("Happiness"))
            {
                emotionflavour = new List<string>();
                emotionflavour.Add("Chocolate");
                emotionflavour.Add("Vanilla");
                emotionflavour.Add("Strawberry");
                emotionflavour.Add("Mint Chocolate");
                emotionflavour.Add("Coffee");
                emotionflavour.Add("Mango");
                emotionflavour.Add("Rasberry");
                emotionflavour.Add("French Vanilla");
                emotionflavour.Add("Cookies n Cream");
            }
            else if (text.Equals("Neutral"))
            {
                emotionflavour = new List<string>();
                emotionflavour.Add("Chocolate");
                emotionflavour.Add("Vanilla");
                emotionflavour.Add("Strawberry");
                emotionflavour.Add("Mint Chocolate");
                emotionflavour.Add("Coffee");
                emotionflavour.Add("Mango");
                emotionflavour.Add("Rasberry");
                emotionflavour.Add("French Vanilla");
                emotionflavour.Add("Cookies n Cream");
            }
            else if (text.Equals("Sadness"))
            {
                emotionflavour = new List<string>();
                emotionflavour.Add("Chocolate");
                emotionflavour.Add("Vanilla");
                emotionflavour.Add("Strawberry");
                emotionflavour.Add("French Vanilla");
                emotionflavour.Add("Cookies n Cream");
            }
            else
            {
                //suprise
                emotionflavour = new List<string>();
                emotionflavour.Add("Chocolate");
                emotionflavour.Add("Vanilla");
                emotionflavour.Add("Strawberry");
                emotionflavour.Add("Mint Chocolate");
                emotionflavour.Add("Coffee");
                emotionflavour.Add("Mango");
                emotionflavour.Add("Rasberry");
                emotionflavour.Add("French Vanilla");
                emotionflavour.Add("Cookies n Cream");
            }
            if (gender.Equals("Male"))
            {
                genderflavour = new List<string>();
                genderflavour.Add("Chocolate");
                genderflavour.Add("Vanilla");
                genderflavour.Add("Mint Chocolate");
                genderflavour.Add("Coffee");
                genderflavour.Add("French Vanilla");
                genderflavour.Add("Cookies n Cream");
            } else
            {
                //female
                genderflavour = new List<string>();
                genderflavour.Add("Chocolate");
                genderflavour.Add("Vanilla");
                genderflavour.Add("Strawberry");
                genderflavour.Add("Mint Chocolate");
                genderflavour.Add("Coffee");
                genderflavour.Add("Mango");
                genderflavour.Add("Rasberry");
                genderflavour.Add("French Vanilla");
            }
            if (age == 13)
            {
                ageflavour = new List<string>();
                ageflavour.Add("Chocolate");
                ageflavour.Add("Vanilla");
                ageflavour.Add("Strawberry");
                ageflavour.Add("Cookies n Cream");
            } else if (age == 18)
            {
                ageflavour = new List<string>();
                ageflavour.Add("Chocolate");
                ageflavour.Add("Vanilla");
                ageflavour.Add("Strawberry");
                ageflavour.Add("Coffee");
                ageflavour.Add("Mango");
                ageflavour.Add("Cookies n Cream");
            } else
            {
                ageflavour = new List<string>();
                ageflavour.Add("Chocolate");
                ageflavour.Add("Vanilla");
                ageflavour.Add("Mint Chocolate");
                ageflavour.Add("Coffee");
                ageflavour.Add("Mango");
                ageflavour.Add("Rasberry");
                ageflavour.Add("French Vanilla");
            }

            Dictionary<string, int> val = new Dictionary<string, int>();
            val.Add("Chocolate", 0);
            val.Add("Vanilla", 0);
            val.Add("Strawberry", 0);
            val.Add("Mint Chocolate", 0);
            val.Add("Coffee", 0);
            val.Add("Mango", 0);
            val.Add("Rasberry", 0);
            val.Add("French Vanilla", 0);
            val.Add("Cookies n Cream", 0);

            foreach (string s in emotionflavour)
            {
                val[s]++;
            }
            foreach (string s in genderflavour)
            {
                val[s]++;
            }
            foreach (string s in ageflavour)
            {
                val[s]++;
            }
            List<string> fav = new List<string>();

            bool isThree = false;
            foreach(KeyValuePair<string, int> entry in val)
            {
                if(entry.Value == 3)
                {
                    fav.Add(entry.Key);
                    isThree = true;
                }
            }
            if (!isThree)
            {
                foreach (KeyValuePair<string, int> entry in val)
                {
                    if (entry.Value == 2)
                    {
                        fav.Add(entry.Key);
                    }
                }
            }

            if( fav.Count == 0)
            {
                return "Chocolate";
            } else
            {
                int r = rnd.Next(fav.Count);
                return fav.ElementAt(r);
            }

        }

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
