using BroomService_App.Models;
using BroomService_App.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BroomService_App.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class JobRequestPage : ContentPage
	{
        private DateTime startdatetimeSelected;
        private DateTime enddatetimeSelected;
        public JobRequestViewModel jobRequestViewModel;

        public JobRequestPage(string propertytype, long? propertyid, int selectedCategoryId, int selectedSubCategoryId, bool hasPrice, double? price, double? clientprice, List<int> selectedSubSubCategoryId = null)
		{
			InitializeComponent ();
            jobRequestViewModel = new JobRequestViewModel(Navigation, propertytype, propertyid.Value, selectedCategoryId, selectedSubCategoryId, selectedSubSubCategoryId, hasPrice, price, clientprice);
            this.BindingContext = jobRequestViewModel;

            //referenceImagesList.IsVisible = false;
        }

        private void StartDatePicker_Unfocused(object sender, FocusEventArgs e)
        {
            startdatetimeSelected = ((DatePicker)sender).Date;
            starttimePicker.Focus();
        }

        private void StartTimePicker_Unfocused(object sender, FocusEventArgs e)
        {
            startdatetimeSelected = startdatetimeSelected.Add(((TimePicker)sender).Time);
            jobRequestViewModel.StartDateTimeValue = startdatetimeSelected.ToString("dd/MM/yyyy") +" at " + startdatetimeSelected.ToString("hh:mm tt");
            jobRequestViewModel.selectedstartDateTime = startdatetimeSelected;
        }

        private void EndDatePicker_Unfocused(object sender, FocusEventArgs e)
        {
            enddatetimeSelected = ((DatePicker)sender).Date;
            endtimePicker.Focus();
        }

        private void EndTimePicker_Unfocused(object sender, FocusEventArgs e)
        {
            enddatetimeSelected = enddatetimeSelected.Add(((TimePicker)sender).Time);
            jobRequestViewModel.EndDateTimeValue = enddatetimeSelected.ToString("dd/MM/yyyy") +" at " + enddatetimeSelected.ToString("hh:mm tt");
            jobRequestViewModel.selectedendDateTime = enddatetimeSelected;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<string>(this, "DateTimePicker");
            //MessagingCenter.Unsubscribe<IEnumerable<object>>(this, "SubcategorySelected");
            MessagingCenter.Unsubscribe<IEnumerable<object>>(this, "SubcategorySelected");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<string>(this, "DateTimePicker", (sender) =>
            {
                switch (sender.ToLower())
                {
                    case "start":
                        startdatePicker.Focus();
                        break;
                    case "end":
                        enddatePicker.Focus();
                        break;
                }
                
            });
        }

        private void Workerworklist_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            workerworklist.SelectedItem = null;
        }

        //private void SubcategoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    UpdateSelectionData(e.CurrentSelection);
        //    //subcategoryList.SelectedItem = null;
        //}

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
    }
}