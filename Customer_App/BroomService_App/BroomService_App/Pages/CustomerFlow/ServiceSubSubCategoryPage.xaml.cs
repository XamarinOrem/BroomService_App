using BroomService_App.CustomControls;
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
    public partial class ServiceSubSubCategoryPage : ContentPage
    {
        //SpacingModifier spacingModifier;
        public ServiceSubSubCategoryPage(string propertytype, long? propertyid, int selectedCategoryId, int selectedSubCategoryid, bool hasPrice)
        {
            InitializeComponent();
            this.BindingContext = new ServiceSubSubCategoryViewModel(Navigation, propertytype, propertyid, selectedCategoryId, selectedSubCategoryid, hasPrice);
        }

        private void SubSubcategoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            subsubcategoryList.SelectedItem = null;
            //UpdateSelectionData(e.CurrentSelection);
        }

        private void UpdateSelectionData(IEnumerable<object> currentSelection)
        {
            try
            {
                //MessagingCenter.Send(currentSelection, "SubcategorySelected");
            }
            catch (Exception ex)
            {

            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //MessagingCenter.Unsubscribe<IEnumerable<object>>(this, "SubcategorySelected");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //spacingModifier = new SpacingModifier(subsubcategoryList);
        }
    }
}