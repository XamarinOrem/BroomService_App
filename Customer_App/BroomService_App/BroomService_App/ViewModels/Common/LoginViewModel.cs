using Acr.UserDialogs;
using BroomService_App.Helpers;
using BroomService_App.Models;
using BroomService_App.Pages;
using BroomService_App.Popups;
using BroomService_App.Repository;
using BroomService_App.Resources;
using LiteDB;
using Rg.Plugins.Popup.Extensions;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XF.Material.Forms;
using XF.Material.Forms.Resources;
using XF.Material.Forms.UI.Dialogs;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace BroomService_App.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region property for the email field
        private string _email;
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }
        #endregion

        #region property for the password field
        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }
        #endregion

        #region Constructor
        public LoginViewModel(INavigation navigation) : base(navigation)
        {
            _navigation = navigation;
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

        #region ForgotPasswordCommand
        public Command ForgotPasswordCommand
        {
            get
            {
                return new Command(() =>
                {
                    try
                    {
                        StaticHelpers.CustomNavigation(_navigation, new ForgotPasswordPage());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("ForgotPasswordCommand_Exception:- " + ex.Message);
                    }
                });
            }
        } 
        #endregion

        #region RegisterCommand
        public Command RegisterCommand
        {
            get
            {
                return new Command(() =>
                {
                    try
                    {
                        StaticHelpers.CustomNavigation(_navigation, new RegisterPage());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("RegisterCommand_Exception:- " + ex.Message);
                    }
                });
            }
        } 
        #endregion

        #region LoginCommand
        public Command LoginCommand
        {
            get
            {
                return new Command(async() =>
                {
                    try
                    {
                        if ((Connectivity.ConnectionProfiles.Contains(ConnectionProfile.WiFi) && Connectivity.NetworkAccess.Equals(NetworkAccess.Internet)) || (Connectivity.ConnectionProfiles.Contains(ConnectionProfile.Cellular) && Connectivity.NetworkAccess.Equals(NetworkAccess.Internet)))
                        {
                            if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password) && !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password))
                            {
                                //await _navigation.PushPopupAsync(new LoaderPopup());
                                UserDialogs.Instance.ShowLoading("");
                                var requestModel = new LoginRequestModel()
                                {
                                    Email = Email,
                                    Password = Password
                                };

                                LoginResponseModel response;
                                try
                                {
                                    response = await webApiRestClient.PostAsync<LoginRequestModel, LoginResponseModel>(ApiHelpers.LoginApi, requestModel);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("LoginApi_Exception:- " + ex.Message);
                                    response = null;
                                }
                                if (response != null)
                                {
                                    if (response.status)
                                    {
                                        var deviceRequestmodel = new UpdateDeviceInfoModel()
                                        {
                                            DeviceId = Device.RuntimePlatform == Device.Android ? 1 : Device.RuntimePlatform == Device.iOS ? 2 : 0,
                                            UserId = response.userData.UserId,
                                            DeviceToken = Application.Current.Properties.ContainsKey("AppFirebaseToken") ? Application.Current.Properties["AppFirebaseToken"].ToString() : null
                                        };
                                        UpdateDeviceInfoResponse deviceInfoResponse;
                                        try
                                        {
                                            deviceInfoResponse = await webApiRestClient.PostAsync<UpdateDeviceInfoModel, UpdateDeviceInfoResponse>(ApiHelpers.UpdateDeviceInfo, deviceRequestmodel);
                                        }
                                        catch (Exception ex)
                                        {
                                            deviceInfoResponse = null;
                                        }
                                        if(deviceInfoResponse != null)
                                        {
                                            if (deviceInfoResponse.status)
                                            {
                                                await MaterialDialog.Instance.SnackbarAsync(message: AppResource.DeviceInfoupdated,
                                                    msDuration: 1000);
                                            }
                                            else
                                            {
                                                await MaterialDialog.Instance.SnackbarAsync(message: deviceInfoResponse.message,
                                                    msDuration: 1000);
                                            }
                                        }
                                        else
                                        {
                                            await MaterialDialog.Instance.SnackbarAsync(message: AppResource.ExceptionDeviceInfo,
                                                    msDuration: 1000);
                                        }

                                        Application.Current.Properties["CurrentUserId"] = response.userData.UserId;
                                        Application.Current.SavePropertiesAsync();
                                        if (userDataDbService.IsUserDbPresentInDB())
                                        {
                                            var data = userDataDbService.ReadAllItems().FirstOrDefault();
                                            BsonValue id = data.ID;
                                            userDataDbService.UpdateUserDataInDb(id, response.userData);
                                        }
                                        else
                                        {
                                            userDataDbService.CreateUserDataInDB(response.userData);
                                        }
                                        App.Current.MainPage = new NavigationPage(new HomeTabPage());
                                        //if (response.userData.UserType == Convert.ToInt32(UserTypeEnum.Customer))
                                        //{
                                        //    App.Current.MainPage = new NavigationPage(new HomeTabPage());
                                        //}
                                        //else
                                        //{
                                        //    App.Current.MainPage = new NavigationPage(new Pages.ServiceProviderFlow.HomeTabbedPage());
                                        //}
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
                                await MaterialDialog.Instance.SnackbarAsync(message: AppResource.EmptyFieldError,
                                                    msDuration: 1000);
                            }
                        }
                        else
                        {
                            await MaterialDialog.Instance.SnackbarAsync(message: AppResource.NoInternetError,
                                                    msDuration: 1000);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("LoginCommand_Exception:- " + ex.Message);
                        UserDialogs.Instance.HideLoading();
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
