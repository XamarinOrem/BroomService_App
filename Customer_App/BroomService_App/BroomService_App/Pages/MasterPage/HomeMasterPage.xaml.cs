using BroomService_App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BroomService_App.Pages.MasterPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeMasterPage : MasterDetailPage
    {
        public HomeMasterPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.BindingContext = new HomeMasterViewModel(Navigation);

            MessagingCenter.Subscribe<string>(this, "MenuIconClicked", (sender) =>
            {
                IsPresented = true;
            });
        }
    }
}