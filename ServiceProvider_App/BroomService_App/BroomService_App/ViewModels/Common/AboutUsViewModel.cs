using BroomService_App.Helpers;
using BroomService_App.Models;
using BroomService_App.Popups;
using BroomService_App.Resources;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace BroomService_App.ViewModels
{
    public class AboutUsViewModel : BaseViewModel
    {
        #region Constructor
        public AboutUsViewModel(INavigation navigation) : base(navigation)
        {
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
            AboutUsDataApi();
        }
        #endregion

        #region Internet Connectivity Changed
        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            var access = e.NetworkAccess;
            var profiles = e.ConnectionProfiles;

            if (access.Equals(NetworkAccess.Internet) || (profiles.Contains(ConnectionProfile.WiFi) && access.Equals(NetworkAccess.Internet)))
            {
                AboutUsDataApi();
            }
        }
        #endregion

        #region AboutUsDataApi
        private async void AboutUsDataApi()
        {
            //await _navigation.PushPopupAsync(new LoaderPopup());
            if ((Connectivity.ConnectionProfiles.Contains(ConnectionProfile.WiFi) && Connectivity.NetworkAccess.Equals(NetworkAccess.Internet)) || (Connectivity.ConnectionProfiles.Contains(ConnectionProfile.Cellular) && Connectivity.NetworkAccess.Equals(NetworkAccess.Internet)))
            {
                AboutUsResponseModel response;
                try
                {
                    response = await webApiRestClient.GetAsync<AboutUsResponseModel>(ApiHelpers.GetAboutusApi);
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
                        MessagingCenter.Send(response.AboutUsData.Text, "AboutUsData");
                        //AboutUsText = htmlToText.Convert(response.AboutUsData.Text);
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
            }
            else
            {
                await MaterialDialog.Instance.SnackbarAsync(message: AppResource.NoInternetError,
                                        msDuration: 1000);
            }
            //await _navigation.PopAllPopupAsync(true);
        }
        #endregion

        #region AboutUsText
        private string _aboutUsText;

        public string AboutUsText
        {
            get { return _aboutUsText; }
            set { SetProperty(ref _aboutUsText, value); }
        }
        #endregion
    }
}
