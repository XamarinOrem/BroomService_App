using BroomService_App.Helpers;
using BroomService_App.ViewModels.ServiceProviderFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BroomService_App.Pages.ServiceProviderFlow
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class JobDetailPage : ContentPage
	{
        private JobDetailViewModel jobdetailvm;
        public JobDetailPage (Models.MyBookingModel bookingListTap = null)
		{
			InitializeComponent ();
            jobdetailvm = new JobDetailViewModel(Navigation, bookingListTap);
            this.BindingContext = jobdetailvm;
            startTimerBtn.CommandParameter = ButtonParameters.startJob;
            completeBtn.CommandParameter = ButtonParameters.complete;
            acceptBtn.CommandParameter = ButtonParameters.accept;
            rejectBtn.CommandParameter = ButtonParameters.reject;
            sendQuoteBtn.CommandParameter = ButtonParameters.sendQuote;
		}

        private void Checklists_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            checklists.SelectedItem = null;
        }

        private void RefImagesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            refImagesList.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<string>(this, "ImageHeight");
        }
    }
}