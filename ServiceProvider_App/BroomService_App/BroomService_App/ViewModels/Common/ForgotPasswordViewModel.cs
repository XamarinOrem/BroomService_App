using BroomService_App.Helpers;
using BroomService_App.Models;
using BroomService_App.Popups;
using BroomService_App.Repository;
using BroomService_App.Resources;
using Rg.Plugins.Popup.Extensions;
using System;
using System.ComponentModel;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace BroomService_App.ViewModels
{
    public class ForgotPasswordViewModel : BaseViewModel
    {
        #region Constructor
        public ForgotPasswordViewModel(INavigation navigation) : base(navigation)
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

        #region Email Property
        private string _email;

        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        } 
        #endregion

        #region CloseCommand
        public Command CloseCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        await _navigation.PopAsync();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("CloseCommand_Exception:- " + ex.Message);
                    }
                });
            }
        }
        #endregion

        #region ForgotPasswordCommand
        public Command ForgotPasswordCommand
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
                            var requestModel = new ForgotPasswordRequestModel()
                            {
                                Email = Email
                            };

                            ForgotPasswordResponseModel response;
                            try
                            {
                                response = await webApiRestClient.PostAsync<ForgotPasswordRequestModel, ForgotPasswordResponseModel>(ApiHelpers.ForgetPasswordApi, requestModel);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("ForgotPasswordApi_Exception:- " + ex.Message);
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
                            await MaterialDialog.Instance.SnackbarAsync(message: AppResource.NoInternetError,
                                                    msDuration: MaterialSnackbar.DurationShort);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("ForgotPasswordCommand_Exception:- " + ex.Message);
                    }
                    finally
                    {
                        //await _navigation.PopAllPopupAsync(true);
                    }
                });
            }
        }
        #endregion
    }
}
