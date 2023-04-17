using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TripsTrapsTrull
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : TabbedPage
    {
        List<ContentPage> contentPages = new List<ContentPage> { new MainPage(), new ScorePage()};
        List<string> names = new List<string> { "Trips Traps Trull", "Tulemus" };
        public StartPage()
        {
            for (int i = 0; i < contentPages.Count; i++)
            {
                NavigationPage navigationPage = new NavigationPage(contentPages[i]);
                navigationPage.Title = names[i];
                Children.Add(navigationPage);
            }
        }
    }
}