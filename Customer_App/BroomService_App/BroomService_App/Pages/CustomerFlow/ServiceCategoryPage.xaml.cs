using Acr.UserDialogs;
using BroomService_App.CustomControls;
using BroomService_App.Helpers;
using BroomService_App.Models;
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
    public partial class ServiceCategoryPage : ContentPage
    {
        private string PropertyType;
        private long? PropertyId;
        public ServiceCategoryPage(string propertytype, long? propertyid)
        {
            InitializeComponent();
            PropertyType = propertytype;
            PropertyId = propertyid;
            this.BindingContext = new ServiceCategoryViewModel(Navigation, PropertyType, PropertyId);
        }

        //private void SelectedItemTap(object sender, ItemTappedEventArgs e)
        //{
        //    UserDialogs.Instance.ShowLoading("");
        //    var selectedcategory = (Category)(e.Item);
        //    StaticHelpers.CustomNavigation(Navigation, new ServiceSubCategoryPage(Propertytype, Propertyid, selectedcategory.Id,selectedcategory.HasPrice));
        //    MessagingCenter.Send(selectedcategory.SubCategories, "SubcategoryList");
        //    UserDialogs.Instance.HideLoading();
        //    //var a = (HomePageModel)e.Item;
        //    //a.FrameColor = Color.LightGreen;
        //    //Xamarin.Forms.Application.Current.Properties["LastSelectedValue"] = a;
        //    //NextButton.IsVisible = true;
        //}

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //spacingModifier = new SpacingModifier(categoryList);
            this.BindingContext = new ServiceCategoryViewModel(Navigation, PropertyType, PropertyId);
        }

        private void CategoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            categoryList.SelectedItem = null;
        }
    }
}