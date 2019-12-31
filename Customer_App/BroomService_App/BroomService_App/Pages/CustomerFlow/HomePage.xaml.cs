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

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;

namespace BroomService_App.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
        protected readonly WebApiRestClient webApiRestClient;

        public HomePage ()
		{
			InitializeComponent ();
            webApiRestClient = new WebApiRestClient();
            this.BindingContext = new HomeViewModel(Navigation);
		}

        private void PropertyList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            propertyList.SelectedItem = null;
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
            UserDialogs.Instance.ShowLoading("");
            PropertyResponse response;
            try
            {
                response = await webApiRestClient.GetAsync<PropertyResponse>(string.Format(ApiHelpers.GetPropertiesByUserId, HomeViewModel.UserId),true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetPropertyApi_Exception:- " + ex.Message);
                response = null;
            }
            if (response != null)
            {
                if (response.status)
                {
                    if (response.data.Count > 0 && response.data != null)
                    {
                        MessagingCenter.Send(response.data, "PropertyListUpdate");
                    }
                    else
                    {
                        //await MaterialDialog.Instance.SnackbarAsync(message: AppResource.NoPropertyFound,
                        //        msDuration: 1000);
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
    }
}