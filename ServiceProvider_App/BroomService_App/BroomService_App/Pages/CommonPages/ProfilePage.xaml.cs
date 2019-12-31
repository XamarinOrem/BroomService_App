using Acr.UserDialogs;
using BroomService_App.Helpers;
using BroomService_App.Models;
using BroomService_App.Resources;
using BroomService_App.Services.ApiService;
using BroomService_App.Services.DBService.LiteDB.ModelDB;
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
	public partial class ProfilePage : ContentPage
	{
        protected readonly WebApiRestClient webApiRestClient;

        public ProfilePage ()
		{
			InitializeComponent ();
            webApiRestClient = new WebApiRestClient();
            BindingContext = new ProfileViewModel(Navigation);
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
            UserProfileModel response;
            try
            {
                response = await webApiRestClient.GetAsync<UserProfileModel>(string.Format(ApiHelpers.GetProfile, ProfileViewModel.UserId));
            }
            catch (Exception ex)
            {
                Console.WriteLine("AboutusApi_Exception:- " + ex.Message);
                response = null;
            }
            if (response != null)
            {
                if (response.status)
                {
                    MessagingCenter.Send(response.Data, "ProfileUpdate");
                }
                else
                {
                    await MaterialDialog.Instance.SnackbarAsync(message: response.message,
                                msDuration: MaterialSnackbar.DurationShort);
                }
            }
            else
            {
                await MaterialDialog.Instance.SnackbarAsync(message: AppResource.ServerError,
                                msDuration: MaterialSnackbar.DurationShort);
            }
            UserDialogs.Instance.HideLoading();
        }
    }
}