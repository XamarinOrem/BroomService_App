using Acr.UserDialogs;
using BroomService_App.ViewModels;
using BroomService_App.ViewModels.CustomerFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BroomService_App.Pages.CustomerFlow
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdatePropertyPage : ContentPage
    {
        public UpdatePropertyPage(Models.PropertyDataModel propertyDataModel = null)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new UpdatePropertyViewModel(Navigation, propertyDataModel);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            UserDialogs.Instance.HideLoading();
        }
    }
}