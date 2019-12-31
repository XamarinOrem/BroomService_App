using BroomService_App.Models;
using BroomService_App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BroomService_App.Services.ApiService;
using XF.Material.Forms.UI.Dialogs;
using BroomService_App.Resources;
using BroomService_App.Helpers;
using Xamarin.Essentials;

namespace BroomService_App.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class JobDetailPage : ContentPage
	{
        public JobDetailPage (MyBookingModel bookingListTap = null)
		{
			InitializeComponent ();
            BindingContext = new JobDetailViewModel(Navigation, bookingListTap);
        }

        private void Checklists_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            checklists.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<string>(this, "RatingPopupClose", (sender) =>
            {
                Navigation.PopAsync();
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<string>(this, "RatingPopupClose");
        }

        private void RefImagesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            refImagesList.SelectedItem = null;
        }
    }
}