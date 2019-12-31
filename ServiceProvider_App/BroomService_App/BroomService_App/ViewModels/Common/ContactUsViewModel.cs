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
    public class ContactUsViewModel:BaseViewModel
    {
        #region Constructor
        public ContactUsViewModel(INavigation navigation) : base(navigation)
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

        #region Name Field
        private string _name;

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        #endregion

        #region Email Field
        private string _email;

        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }
        #endregion

        #region Message Field
        private string _message;

        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
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
                            if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Message) && !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Message) && CheckValidEmail(Email))
                            {
                                var requestData = new ContactUsRequestModel()
                                {
                                    Email = Email,
                                    Message = Message,
                                    Name = Name,
                                    UserId = CurrentUserId
                                };
                                ContactUsResponseModel response;
                                try
                                {
                                    response = await webApiRestClient.PostAsync<ContactUsRequestModel, ContactUsResponseModel>(ApiHelpers.ContactUs, requestData);
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
                                if (string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(Message) && string.IsNullOrWhiteSpace(Email) && string.IsNullOrWhiteSpace(Name) && string.IsNullOrWhiteSpace(Message))
                                {
                                    await MaterialDialog.Instance.SnackbarAsync(message: AppResource.EmptyFieldError,
                                                    msDuration: MaterialSnackbar.DurationShort);
                                }
                                else if(string.IsNullOrEmpty(Name) || string.IsNullOrWhiteSpace(Name))
                                {
                                    await MaterialDialog.Instance.SnackbarAsync(message: AppResource.ContactUsNameError,
                                                    msDuration: MaterialSnackbar.DurationShort);
                                }
                                else if(string.IsNullOrEmpty(Email) || string.IsNullOrWhiteSpace(Email))
                                {
                                    await MaterialDialog.Instance.SnackbarAsync(message: AppResource.ContactUsEmailError,
                                                    msDuration: MaterialSnackbar.DurationShort);
                                }
                                else if(string.IsNullOrEmpty(Message) || string.IsNullOrWhiteSpace(Message))
                                {
                                    await MaterialDialog.Instance.SnackbarAsync(message: AppResource.ContactUsMsgError,
                                                    msDuration: MaterialSnackbar.DurationShort);
                                }
                                else if(!CheckValidEmail(Email))
                                {
                                    await MaterialDialog.Instance.SnackbarAsync(message: AppResource.EmailValidation,
                                                    msDuration: MaterialSnackbar.DurationShort);
                                }
                            }
                        }
                        else
                        {
                            await MaterialDialog.Instance.SnackbarAsync(message: AppResource.NoInternetError,
                                                    msDuration: MaterialSnackbar.DurationShort);
                        }
                    }
                    catch (Exception)
                    {

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
