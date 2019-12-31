using BroomService_App.Resources;
using BroomService_App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BroomService_App.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuListPage : ContentPage
    {
        public ListView ListView { get { return listView; } }
        public static ListView listView;

        public MenuListPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            Title = AppResource.Home;
            IconImageSource = "hamburger_white.png";
            BackgroundImageSource = "menu_bg.png";
            BindingContext = new MenuViewModel(Navigation);
            listView = lstVieMenu;

            if(Device.RuntimePlatform == Device.iOS)
            {
                ImgUser.HeightRequest = 70;
                ImgUser.WidthRequest = 70;
            }


            //var lng = App.Database.GetLng();
            //if (lng != null && !string.IsNullOrEmpty(lng.Language))
            //{
            //    if (lng.Language == Models.CultureLanguage.Arabic)
            //    {
            //        this.FlowDirection = FlowDirection.RightToLeft;
            //    }
            //    else
            //    {
            //        this.FlowDirection = FlowDirection.LeftToRight;
            //    }
            //}
            //else
            //{
            //    this.FlowDirection = FlowDirection.LeftToRight;
            //}
        }

        private void LstVieMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            lstVieMenu.SelectedItem = null;
        }
    }
}