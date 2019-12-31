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
    public partial class ServiceSubCategoryPage : ContentPage
    {
        //SpacingModifier spacingModifier;
        public ServiceSubCategoryPage(string propertytype, long? propertyid, int selectedcategoryid, bool hasPrice)
        {
            InitializeComponent();
            this.BindingContext = new ServiceSubCategoryViewModel(Navigation, propertytype, propertyid, selectedcategoryid, hasPrice);
        }

        //private void SelectedItemTap(object sender, ItemTappedEventArgs e)
        //{
        //    var subcategoryselected = (SubCategory)(e.Item);
        //    StaticHelpers.CustomNavigation(Navigation, new JobRequestPage(Propertytype, Propertyid, SelectedCategoryId, subcategoryselected.Id, HasPrice));
        //}

        private void SubcategoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            subcategoryList.SelectedItem = null;
        }

        //private void UpdateSelectionData(IEnumerable<object> currentSelection)
        //{
        //    try
        //    {
        //        MessagingCenter.Send(currentSelection, "SubcategorySelected");
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        //protected override void OnDisappearing()
        //{
        //    base.OnDisappearing();
        //    MessagingCenter.Unsubscribe<IEnumerable<object>>(this, "SubcategorySelected");
        //}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //spacingModifier = new SpacingModifier(subcategoryList);
        }
    }
}