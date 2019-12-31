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
    public class TermsConditionsViewModel:BaseViewModel
    {
        #region Constructor
        public TermsConditionsViewModel(INavigation navigation) : base(navigation)
        {
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
            TermConditionApi();
        } 
        #endregion

        #region Internet Connectivity Changed
        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            var access = e.NetworkAccess;
            var profiles = e.ConnectionProfiles;

            if(access.Equals(NetworkAccess.Internet) || (profiles.Contains(ConnectionProfile.WiFi) && access.Equals(NetworkAccess.Internet)))
            {
                TermConditionApi();
            }
        }
        #endregion

        #region Api Call
        private async void TermConditionApi()
        {
            if ((Connectivity.ConnectionProfiles.Contains(ConnectionProfile.WiFi) && Connectivity.NetworkAccess.Equals(NetworkAccess.Internet)) || (Connectivity.ConnectionProfiles.Contains(ConnectionProfile.Cellular) && Connectivity.NetworkAccess.Equals(NetworkAccess.Internet)))
            {
                //await _navigation.PushPopupAsync(new LoaderPopup());
                TermConditionResponseModel response;
                try
                {
                    response = await webApiRestClient.GetAsync<TermConditionResponseModel>(ApiHelpers.GetTermsConditionsApi);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("TermConditionApi_Exception:- " + ex.Message);
                    response = null;
                }
                if (response != null)
                {
                    if (response.status)
                    {
                        //TermConditionText = htmlToText.Convert(response.TermsConditionsData.TermsConditionText);
                        MessagingCenter.Send(response.TermsConditionsData.TermsConditionText, "TermsConditionsData");
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

        #region TermConditionText Field
        private string _termConditionText;

        public string TermConditionText
        {
            get { return _termConditionText; }
            set { SetProperty(ref _termConditionText, value); }
        } 
        #endregion

    }
}