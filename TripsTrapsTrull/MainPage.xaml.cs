using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
        Image cross = new Image { Source = ImageSource.FromFile("cross.png") };
        Image circle = new Image { Source = ImageSource.FromFile("circle.png") };
        
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
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
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
            List<string> sources = new List<string> { cross.ToString(), circle.ToString() };
            //Оптимизация правил 
            //Next level оптимизация под списки >>> в for длина Math.Sqrt(images.Count) + создание переменной, считающая совпадения
            for (int i = 0; i < sources.Count; i++)
            {                
                for (int j = 0; j < 3; j += 3) //Выигрыш по ряду
                {
                    if (images[j].ToString() == sources[i] && images[j + 1].ToString() == sources[i] && images[j + 2].ToString() == sources[i])
                    {
                        await DisplayAlert("WIN", sources[i] + " on võitnud", "OK");
                    }
                }               
                //for (int j = 0; j < 3; j++) //Выигрыш по колонке
                //{
                //    if (images[j].Source == sources[i] && images[j + 3].Source == sources[i] && images[j + 6].Source == sources[i])
                //    {
                //        await DisplayAlert("WIN", sources[i].ToString() + " on võitnud", "OK");
                //    }
                //}
                ////Выигрыш по диагонали
                //if (images[0].Source == sources[i] && images[4].Source == sources[i] && images[8].Source == sources[i] ||
                //    images[6].Source == sources[i] && images[4].Source == sources[i] && images[2].Source == sources[i])
                //{
                //    await DisplayAlert("WIN", sources[i].ToString() + " on võitnud", "OK");
                //}
            }


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
            //идея содержит ли список из 2 картинок (прошлую идею) >>> не работает
            //идея с двумерным массивом и передачей информации местоположения (ряд, колонка)
        }
    }
}
