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
                Text = "Trips Traps Trull", 
                FontSize = 20, 
                TextColor = Color.Black, 
                HorizontalOptions = LayoutOptions.Center, 
                VerticalOptions = LayoutOptions.Center 
            };            

            Uus_mang();

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
            //if (img.AutomationId == ) {}
            ImageSource cross = ImageSource.FromFile("cross.png");
            ImageSource circle = ImageSource.FromFile("circle.png");

            List<ImageSource> sources = new List<ImageSource> { cross, circle };



            //Оптимизация правил (только с ноликом)
            //Next level оптимизация под списки >>> в for длина Math.Sqrt(images.Count) + создание переменной, считающая совпадения
            //for (int i = 0; i < 3; i += 3) //Выигрыш в ряд
            //{
            //    if (images[i].Source == circle.Source && images[i + 1].Source == circle.Source && images[i + 2].Source == circle.Source)
            //    {
            //        await DisplayAlert("WIN", "Second on võitnud", "OK");
            //    }
            //}
            //for (int i = 0; i < 3; i++)
            //{
            //    if (images[i].Source == circle.Source && images[i + 3].Source == circle.Source && images[i + 6].Source == circle.Source)
            //    {
            //        await DisplayAlert("WIN", "Second on võitnud", "OK");
            //    }
            //}
            //if (images[0].Source == circle.Source && images[4].Source == circle.Source && images[8].Source == circle.Source ||
            //    images[6].Source == circle.Source && images[4].Source == circle.Source && images[2].Source == circle.Source)
            //{
            //    await DisplayAlert("WIN", "Second on võitnud", "OK");
            //}
            

            //}
            //Если в ячейках
            //1, 2, 3; >>> 0, 1, 2
            //4, 5, 6; >>> 3, 4, 5
            //7, 8, 9; >>> 6, 7, 8
            //
            //1, 4, 7; >>> 0, 3, 6
            //2, 5, 8; >>> 1, 4, 7
            //3, 6, 9; >>> 2, 5, 8
            //
            //1, 5, 9 >>> 0, 4, 8
            //7, 5, 3 >>> 6, 4, 2
            //одинаковые картинки, то игра выиграна

            //идея со списком >>> не работает
            //идея содержит ли список из 2 картинок (прошлую идею)
            //идея с двумерным массивом и передачей информации местоположения (ряд, колонка)
        }
    }
}
