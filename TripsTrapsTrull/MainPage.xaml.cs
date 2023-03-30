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
        List<string> sources;


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
            taps = 0;
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
        void Voidu_kontroll()
        {
            sources = new List<string> { cross.Source.ToString(), circle.Source.ToString() };
            
            foreach (string item in sources)
            {
                for (int i = 0; i < 9; i += 3) //Выигрыш по ряду
                {
                    if (images[i].Source.ToString() == item && images[i + 1].Source.ToString() == item && images[i + 2].Source.ToString() == item)
                    {
                        Voit(item);
                    }
                }
                for (int i = 0; i < 3; i++) //Выигрыш по колонке
                {
                    if (images[i].Source.ToString() == item && images[i + 3].Source.ToString() == item && images[i + 6].Source.ToString() == item)
                    {
                        Voit(item);
                    }
                }
                //Выигрыш по диагонали
                if (images[0].Source.ToString() == item && images[4].Source.ToString() == item && images[8].Source.ToString() == item ||
                    images[6].Source.ToString() == item && images[4].Source.ToString() == item && images[2].Source.ToString() == item)
                {
                    Voit(item);
                }
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
            //1, 5, 9 >>> 0, 4, 8  >>> 0, (0 - 4)
            //7, 5, 3 >>> 6, 4, 2  >>> 6, (6 - 4)
            //одинаковые картинки, то игра выиграна

            //Next level оптимизация под списки >>> в for длина Math.Sqrt(images.Count) + создание переменной, считающая совпадения
        }
        async void Voit(string voitja)
        {            
            if (sources.IndexOf(voitja) == 0)
            {
                await DisplayAlert("Õnnitlus", "Ristid võitsid!", "OK");
            }
            else
            {
                await DisplayAlert("Õnnitlus", "Nullid võitsid!", "OK");
            }            
            Uus_mang();
        }
    }
}
