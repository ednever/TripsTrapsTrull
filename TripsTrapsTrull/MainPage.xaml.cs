using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TripsTrapsTrull
{
    public partial class MainPage : ContentPage
    {
        Grid grid = new Grid();
        int taps = 0;
        List<Image> images = new List<Image>();
        public MainPage()
        {
            Label label = new Label 
            { 
                Text = "Trips Traps Trull", FontSize = 20, TextColor = Color.Black, HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center 
            };

            

            Uus_mang();
            Voidu_kontroll();

            Button button = new Button { Text = "Uus mäng" };
            button.Clicked += Button_Clicked;

            StackLayout st = new StackLayout { Children = { label, grid, button } };
            Content = st;
        }

        void Tap_Tapped(object sender, EventArgs e)
        {
            taps++;
            Image img = (Image)sender;
            if (taps % 2 == 0)
            {
                img.Source = ImageSource.FromFile("circle.png");
            }
            else
            {
                img.Source = ImageSource.FromFile("cross.png");
            }
            img.IsEnabled = false;

            Voidu_kontroll();
        }

        void Button_Clicked(object sender, EventArgs e)
        {
            Uus_mang();
        }

        void Uus_mang()
        {
            images.Clear();
            grid.Children.Clear();
            for (int i = 0; i <= 2; i++)
            {
                for (int j = 0; j <= 2; j++)
                {
                    Image image = new Image
                    {
                        HeightRequest = 200,
                        WidthRequest = 200,
                        Source = ImageSource.FromFile("white.png")
                    };
                    TapGestureRecognizer tap = new TapGestureRecognizer();
                    tap.Tapped += Tap_Tapped;
                    image.GestureRecognizers.Add(tap);
                    images.Add(image);

                    //Frame frame = new Frame
                    //{
                    //    WidthRequest = 200,
                    //    HeightRequest = 200,
                    //    BorderColor = Color.Black,
                    //    Content = circle
                    //};

                    //grid.Children.Add(frame);
                    //Grid.SetRow(frame, i);
                    //Grid.SetColumn(frame, j);

                    grid.Children.Add(image);
                    Grid.SetRow(image, i);
                    Grid.SetColumn(image, j);
                }
            }
        }
        async void Voidu_kontroll()
        {
            //if (img.AutomationId == )
            //{
            //ImageSource circle = ImageSource.FromFile("white.png");
            ImageSource cross = ImageSource.FromFile("cross.png");
            //string circle = "circle.png";
            Image circle = new Image();
            circle.Source = ImageSource.FromFile("white.png");

            if (images[0].Source == circle.Source && images[1].Source == circle.Source && images[2].Source == circle.Source)
            {
                await DisplayAlert("WIN","Second on võitnud","OK");
            }
            //}
            //Если в ячейках
            //1, 2, 3;
            //4, 5, 6;
            //7, 8, 9;
            //
            //1, 4, 7;
            //2, 5, 8;
            //3, 6, 9;
            //
            //1, 5, 9
            //7, 5, 3
            //одинаковые картинки, то игра выиграна
        }
    }
}
