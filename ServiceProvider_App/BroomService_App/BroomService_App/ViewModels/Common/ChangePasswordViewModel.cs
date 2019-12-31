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
    public class ChangePasswordViewModel:BaseViewModel
    {
        #region Constructor
        public ChangePasswordViewModel(INavigation navigation):base(navigation)
        {
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }
        #endregion

        #region Internet Connectivity Changed
        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            var access = e.NetworkAccess;
            var profiles = e.ConnectionProfiles;
        }
        #endregion

        #region OldPassword Field
        private string _oldPassword;

        public string OldPassword
        {
            get { return _oldPassword; }
            set { SetProperty(ref _oldPassword, value); }
        }
        #endregion

        #region NewPassword Field
        private string _newPassword;

        public string NewPassword
        {
            get { return _newPassword; }
            set { SetProperty(ref _newPassword, value); }
        }
        #endregion

        #region ConfirmPassword Field
        private string _confirmPassword;

        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set { SetProperty(ref _confirmPassword, value); }
        }
        #endregion

        #region SendCommand
        public Command SendCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        if (Connectivity.NetworkAccess.Equals(NetworkAccess.Internet) || Connectivity.ConnectionProfiles.Contains(ConnectionProfile.WiFi))
                        {
                            //await _navigation.PushPopupAsync(new LoaderPopup());
                            if (!string.IsNullOrEmpty(OldPassword) && !string.IsNullOrWhiteSpace(OldPassword) && !string.IsNullOrEmpty(NewPassword) && !string.IsNullOrWhiteSpace(NewPassword))
                            {
                                if (NewPassword == ConfirmPassword)
                                {
                                    var requestData = new ChangePasswordRequestModel()
                                    {
                                        userId = CurrentUserId.ToString(),
                                        oldPassword = OldPassword,
                                        newPassword = NewPassword
                                    };
                                    ChangePasswordResponseModel response;
                                    try
                                    {
                                        response = await webApiRestClient.PostAsync<ChangePasswordRequestModel, ChangePasswordResponseModel>(ApiHelpers.ChangePasswordApi, requestData);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("ChangePswdApi_Exception:- " + ex.Message);
                                        response = null;
                                    }
                                    if (response != null)
                                    {
                                        if (response.status)
                                        {
                                            await MaterialDialog.Instance.SnackbarAsync(message: response.message,
                                                        msDuration: MaterialSnackbar.DurationShort);
                                            await _navigation.PopAsync();
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
                                }
                                else
                                {
                                    await MaterialDialog.Instance.SnackbarAsync(message: AppResource.ConfirmPasswordError,
                                                       msDuration: MaterialSnackbar.DurationShort);
                                }
                            }
                            else
                            {
                                await MaterialDialog.Instance.SnackbarAsync(message: AppResource.EmptyFieldError,
                                                       msDuration: MaterialSnackbar.DurationShort);
                            }
                        }
                        else
                        {
                            await MaterialDialog.Instance.SnackbarAsync(message: AppResource.NoInternetError,
                                                    msDuration: MaterialSnackbar.DurationShort);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    //await _navigation.PopAllPopupAsync(true);
                });
            }
        } 
        #endregion
    }
}