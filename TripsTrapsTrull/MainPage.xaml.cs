using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TripsTrapsTrull
{
    public partial class MainPage : ContentPage
    {
        Grid grid = new Grid();
        Random rnd = new Random();
        Image cross = new Image { Source = ImageSource.FromFile("cross.png") };
        Image circle = new Image { Source = ImageSource.FromFile("circle.png") };
        Image white = new Image { Source = ImageSource.FromFile("white.png") };

        bool arvuti = false;
        int voit = 0;
        int taps = 0;
        int red = 0;
        int blue = 0;

        List<string> sources;
        List<Image> images = new List<Image>();
        List<Label> labels = new List<Label>();
        List<string> labelsNames = new List<string> { "0", "vs", "0" };
        List<Button> buttons = new List<Button>();
        List<string> buttonsNames = new List<string> { "Uus mäng", "Mäng arvuti vastu", "X", "O" };
        List<StackLayout> layouts = new List<StackLayout>();
        public MainPage()
        {
            //Создание StackLayot
            for (int i = 0; i < 2; i++)
            {
                StackLayout st1 = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                };
                layouts.Add(st1);
            }

            //Создание Label
            for (int i = 0; i < labelsNames.Count; i++)
            {
                Label label = new Label
                {
                    Text = labelsNames[i],
                    FontSize = 20,
                    TextColor = Color.Black,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                };
                labels.Add(label);
                layouts[0].Children.Add(label); //Добавление Label в вверхний StackLayout
            }

            //Создание Button
            for (int i = 0; i < buttonsNames.Count; i++)
            {
                Button button = new Button
                {
                    Text = buttonsNames[i],
                    FontSize = 20,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                };
                button.Clicked += Button_Clicked;
                buttons.Add(button);
                layouts[1].Children.Add(button); //Добавление Button в нижний StackLayout
            }

            layouts[0].Children.Insert(0, buttons[2]); //Добавление Button "X" в вверхний StackLayout
            layouts[0].Children.Insert(4, buttons[3]); //Добавление Button "O" в вверхний StackLayout

            layouts[1].Children.Remove(buttons[2]); //Удаление Button "X" из нижнего StackLayout
            layouts[1].Children.Remove(buttons[3]); //Удаление Button "O" из нижнего StackLayout

            StackLayout st = new StackLayout { Children = { layouts[0], grid, layouts[1] } };
            Content = st;

            st.BackgroundColor = Color.PeachPuff;

            Uus_mang();
        }
        void Tap_Tapped(object sender, EventArgs e)
        {
            buttons[2].IsEnabled = false;
            buttons[3].IsEnabled = false;

            taps++;
            Image img = (Image)sender;
            if (taps % 2 == 0)
            {
                img.Source = circle.Source;
                buttons[3].BackgroundColor = Color.LightGray;
                buttons[2].BackgroundColor = Color.Gray;
            }
            else
            {
                img.Source = cross.Source;
                buttons[2].BackgroundColor = Color.LightGray;
                buttons[3].BackgroundColor = Color.Gray;
            }
            img.IsEnabled = false;

            Arvuti(arvuti);
            Voidu_kontroll();
        }
        void Button_Clicked(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn == buttons[0] || btn == buttons[1])
            {
                Uus_mang();
                if (btn == buttons[1])
                {
                    arvuti = true;
                    Arvuti(arvuti);
                }
            }
            else if (btn == buttons[2])
            {
                buttons[2].IsEnabled = false;
                buttons[3].IsEnabled = true;
                buttons[3].BackgroundColor = Color.LightGray;
                buttons[2].BackgroundColor = Color.Gray;
                taps = 0;
            }
            else
            {
                buttons[3].IsEnabled = false;
                buttons[2].IsEnabled = true;
                buttons[2].BackgroundColor = Color.LightGray;
                buttons[3].BackgroundColor = Color.Gray;
                taps = 1;
                Arvuti(arvuti);
            }
        }
        void Uus_mang()
        {
            for (int i = 2; i <= 3; i++)
            {
                buttons[i].IsEnabled = true;
                buttons[i].BackgroundColor = Color.LightGray;
            }
            buttons[2].BackgroundColor = Color.Gray;

            arvuti = false;
            voit = 0;
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
                        Source = white.Source,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                    };
                    TapGestureRecognizer tap = new TapGestureRecognizer();
                    tap.Tapped += Tap_Tapped;
                    image.GestureRecognizers.Add(tap);
                    images.Add(image);

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
                        voit++;
                        Voit(item, voit);
                    }
                }
                for (int i = 0; i < 3; i++) //Выигрыш по колонке
                {
                    if (images[i].Source.ToString() == item && images[i + 3].Source.ToString() == item && images[i + 6].Source.ToString() == item)
                    {
                        voit++;
                        Voit(item, voit);
                    }
                }
                //Выигрыш по диагонали
                for (int i = 0; i < 7; i += 6)
                {
                    if (images[i].Source.ToString() == item && images[i * 0 + 4].Source.ToString() == item && images[i * 0 + 8 - i].Source.ToString() == item)
                    {
                        voit++;
                        Voit(item, voit);
                    }
                }
            }

            //Ничья
            bool vabaKoht = true;
            foreach (Image item in images)
            {
                if (item.Source.ToString() == white.Source.ToString())
                    vabaKoht = false;
            }
            if (vabaKoht)
            {
                voit++;
                string mitteKeegi = string.Empty;
                Voit(mitteKeegi, voit);
            }
        }
        async void Voit(string voitja, int onoff2)
        {
            if (onoff2 == 1)
            {
                if (sources.IndexOf(voitja) == 0)
                {
                    await DisplayAlert("Õnnitlus", "Ristid võitsid!", "OK");
                    red++;
                    labels[0].Text = red.ToString();
                    Preferences.Set("Rist", "X võitis (" + DateTime.Now + ")");
                }
                else if (sources.IndexOf(voitja) == 1)
                {
                    await DisplayAlert("Õnnitlus", "Nullid võitsid!", "OK");
                    blue++;
                    labels[2].Text = blue.ToString();
                    Preferences.Set("Null", "0 võitis (" + DateTime.Now + ")");
                }
                else
                {
                    await DisplayAlert("Õnnitlus", "Viik!", "OK");
                }

                Uus_mang();
            }
        }
        void Arvuti(bool onoff) //Из-за паузы можно нажать быстрее бота
        {
            if (onoff)
            {
                kerge_b();
            }
        }
        async void kerge_b()
        {
            List<Image> vabadKohad = new List<Image>();
            foreach (Image item in images)
            {
                if (item.Source.ToString() == white.Source.ToString())
                    vabadKohad.Add(item);
            }
            if (vabadKohad.Count > 0)
            {
                int a = rnd.Next(vabadKohad.Count);
                await Task.Delay(1000);
                if (buttons[3].IsEnabled == false)
                {
                    buttons[2].IsEnabled = false;
                    if (vabadKohad.Count == 9)
                    {
                        vabadKohad[a].Source = cross.Source;
                        taps++;
                    }
                    else
                    {
                        if (taps % 2 != 0)
                        {
                            vabadKohad[a].Source = circle.Source;
                            buttons[3].BackgroundColor = Color.LightGray;
                            buttons[2].BackgroundColor = Color.Gray;
                        }
                        else if (taps % 2 == 0)
                        {
                            vabadKohad[a].Source = cross.Source;
                            buttons[2].BackgroundColor = Color.LightGray;
                            buttons[3].BackgroundColor = Color.Gray;
                        }
                    }

                    vabadKohad[a].IsEnabled = false;
                    vabadKohad.Clear();
                    taps++;
                }
            }
            Voidu_kontroll();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}