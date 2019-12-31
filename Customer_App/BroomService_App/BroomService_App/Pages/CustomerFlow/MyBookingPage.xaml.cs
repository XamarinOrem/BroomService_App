using Acr.UserDialogs;
using BroomService_App.Helpers;
using BroomService_App.Models;
using BroomService_App.Resources;
using BroomService_App.Services.ApiService;
using BroomService_App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;

namespace BroomService_App.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MyBookingPage : ContentPage
	{
        protected MyBookingResponseModel response;
        protected readonly WebApiRestClient webApiRestClient;

        public MyBookingPage ()
		{
			InitializeComponent ();
            this.BindingContext = new MyBookingViewModel(Navigation);
            webApiRestClient = new WebApiRestClient();

		}

        private void BookingList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bookingList.SelectedItem = null;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (App.IsNotificationRecieved)
            {
                MessagingCenter.Send("5", "NotificationRecieved");
            }
            else
            {
                MessagingCenter.Send(string.Empty, "NotificationRecieved");
            }

            if ((Connectivity.ConnectionProfiles.Contains(ConnectionProfile.WiFi) && Connectivity.NetworkAccess.Equals(NetworkAccess.Internet)) || (Connectivity.ConnectionProfiles.Contains(ConnectionProfile.Cellular) && Connectivity.NetworkAccess.Equals(NetworkAccess.Internet)))
            {
                UserDialogs.Instance.ShowLoading("");

                try
                {
                    response = await webApiRestClient.GetAsync<MyBookingResponseModel>(string.Format(ApiHelpers.GetCustomerJobRequests, ProfileViewModel.UserId), true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("MyBookingApi_Exception:-" + ex.Message);
                    response = null;
                }
                if (response != null)
                {
                    if (response.status)
                    {
                        try
                        {
                            MessagingCenter.Send(response.data, "MyBookingListUpdate");
                        }
                        catch (Exception ex)
                        {
                            await MaterialDialog.Instance.SnackbarAsync(message: ex.Message,
                                    msDuration: 1000);
                        }
                    }
                    else
                    {
                        await MaterialDialog.Instance.SnackbarAsync(message: response.message,
                                    msDuration: 1000);
                    }
                }
                else
                {
                    await MaterialDialog.Instance.SnackbarAsync(message: AppResource.ServerError,
                                    msDuration: 1000);
                }
                UserDialogs.Instance.HideLoading();
            }
            else
            {
                await MaterialDialog.Instance.SnackbarAsync(message: AppResource.NoInternetError,
                                        msDuration: 1000);
            }
        }
    }
}