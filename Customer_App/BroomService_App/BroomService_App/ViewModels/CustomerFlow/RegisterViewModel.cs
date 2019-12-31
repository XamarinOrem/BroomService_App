using Acr.UserDialogs;
using BroomService_App.Helpers;
using BroomService_App.Models;
using BroomService_App.Pages;
using BroomService_App.Popups;
using BroomService_App.Repository;
using BroomService_App.Resources;
using LiteDB;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace BroomService_App.ViewModels
{
    public class RegisterViewModel: BaseViewModel
    {
        private List<CountryDetailDataModel> countryDetailData;

        #region CountryCode
        private string _CountryCode;

        public string CountryCode
        {
            get { return _CountryCode; }
            set { SetProperty(ref _CountryCode, value); }
        } 
        #endregion


        #region TermConditionCheck image
        private string _termConditionCheck;

        public string TermConditionCheck
        {
            get { return _termConditionCheck; }
            set { SetProperty(ref _termConditionCheck, value); }
        }
        #endregion

        #region Picker SelectedItem property
        private CountryDetailDataModel _CountryCodeSelected = new CountryDetailDataModel();
        public CountryDetailDataModel CountryCodeSelected
        {
            get { return _CountryCodeSelected; }
            set
            {
                SetProperty(ref _CountryCodeSelected, value);
                if (CountryCodeSelected != null)
                {
                    CountrySelectedIndexChanged = CountryPickerList.IndexOf(CountryCodeSelected);
                }
            }
        }
        #endregion

        #region CountryCodeList Picker
        private ObservableCollection<CountryDetailDataModel> _CountryCodeList = new ObservableCollection<CountryDetailDataModel>();
        public ObservableCollection<CountryDetailDataModel> CountryCodeList
        {
            get { return _CountryCodeList; }
            set
            { SetProperty(ref _CountryCodeList, value); }
        }
        #endregion

        #region CountryPickerList
        private ObservableCollection<CountryDetailDataModel> _CountryPickerList = new ObservableCollection<CountryDetailDataModel>();
        public ObservableCollection<CountryDetailDataModel> CountryPickerList
        {
            get { return _CountryPickerList; }
            set { SetProperty(ref _CountryPickerList, value); }
        }
        #endregion
        
        #region CountrySelected
        private CountryDetailDataModel _CountrySelected = new CountryDetailDataModel();
        public CountryDetailDataModel CountrySelected
        {
            get { return _CountrySelected; }
            set { SetProperty(ref _CountrySelected, value); }
        }
        #endregion

        #region CountryPicker SelectedIndexChanged
        private int _CountrySelectedIndexChanged;

        public int CountrySelectedIndexChanged
        {
            get { return _CountrySelectedIndexChanged; }
            set { SetProperty(ref _CountrySelectedIndexChanged, value); }
        }
        #endregion

        #region property for the FirstName field
        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }
        #endregion

        #region property for the LastName field
        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
        }
        #endregion
        
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

        #region property for the PhoneNumber field
        private string _phoneNumber;
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { SetProperty(ref _phoneNumber, value); }
        }
        #endregion

        //#region property for the CountryName field
        //private string _countryName;
        //public string CountryName
        //{
        //    get { return _countryName; }
        //    set { SetProperty(ref _countryName, value); }
        //}
        //#endregion

        #region property for the CityName field
        private string _cityName;
        public string CityName
        {
            get { return _cityName; }
            set { SetProperty(ref _cityName, value); }
        }
        #endregion

        #region property for the Address field
        private string _address;
        public string Address
        {
            get { return _address; }
            set { SetProperty(ref _address, value); }
        }
        #endregion

        #region property for the ZipCode field
        private string _zipCode;
        public string ZipCode
        {
            get { return _zipCode; }
            set { SetProperty(ref _zipCode, value); }
        }
        #endregion

        #region Constructor
        public RegisterViewModel(INavigation navigation) : base(navigation)
        {
            CountryCode = "+972";
            UserPic = "ic_profile.png";
            TermConditionCheck = "ic_uncheked.png";
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
            GetCountryDetailApi();
        }
        #endregion

        #region TermConditionCheckCommand
        public Command TermConditionCheckCommand
        {
            get
            {
                return new Command(() =>
                {
                    if (TermConditionCheck == "ic_uncheked.png")
                    {
                        TermConditionCheck = "ic_register_check.png";
                    }
                    else if (TermConditionCheck == "ic_register_check.png")
                    {
                        TermConditionCheck = "ic_uncheked.png";
                    }
                });
            }
        } 
        #endregion

        #region Internet Connectivity Changed
        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            var access = e.NetworkAccess;
            var profiles = e.ConnectionProfiles;

            if (access.Equals(NetworkAccess.Internet) || (profiles.Contains(ConnectionProfile.WiFi) && access.Equals(NetworkAccess.Internet)))
            {
                GetCountryDetailApi();
            }
        }
        #endregion

        #region GetCountryDetailApi Call Method
        private async void GetCountryDetailApi()
        {
            if ((Connectivity.ConnectionProfiles.Contains(ConnectionProfile.WiFi) && Connectivity.NetworkAccess.Equals(NetworkAccess.Internet)) || (Connectivity.ConnectionProfiles.Contains(ConnectionProfile.Cellular) && Connectivity.NetworkAccess.Equals(NetworkAccess.Internet)))
            {
                CountryDetailModel response;
                try
                {
                    response = await webApiRestClient.GetAsync<CountryDetailModel>(ApiHelpers.GetCountries);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("CountryDetailApi_Exception:- " + ex.Message);
                    response = null;
                }
                if (response != null)
                {
                    if (response.status)
                    {
                        countryDetailData = response.Data;
                        foreach(var data in countryDetailData)
                        {
                            CountryPickerList.Add(data);
                            CountryCodeList.Add(data);
                        }
                    }
                    else
                    {
                        await MaterialDialog.Instance.SnackbarAsync(message: response.message,
                                    msDuration: 1000);
                        countryDetailData = null;
                    }
                }
                else
                {
                    await MaterialDialog.Instance.SnackbarAsync(message: AppResource.ServerError,
                                    msDuration: 1000);
                    countryDetailData = null;
                }
            }
            else
            {
                await MaterialDialog.Instance.SnackbarAsync(message: AppResource.NoInternetError,
                                        msDuration: 1000);
            }
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

        #region TermConditionCommand
        public Command TermConditionCommand
        {
            get
            {
                return new Command( () =>
                {
                    try
                    {
                        StaticHelpers.CustomNavigation(_navigation, new TermsConditionsPage());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("TermConditionCommand_Exception:- " + ex.Message);
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
                return new Command(async() =>
                {
                    try
                    {
                        byte[] bitmapData;
                        if ((Connectivity.ConnectionProfiles.Contains(ConnectionProfile.WiFi) && Connectivity.NetworkAccess.Equals(NetworkAccess.Internet)) || (Connectivity.ConnectionProfiles.Contains(ConnectionProfile.Cellular) && Connectivity.NetworkAccess.Equals(NetworkAccess.Internet)))
                        {
                            if (!string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(PhoneNumber) && !string.IsNullOrEmpty(CountryCode) && !string.IsNullOrWhiteSpace(CountryCode) && CountrySelected != null)
                            {
                                if (CheckValidEmail(Email))
                                {
                                    if (TermConditionCheck == "ic_register_check.png")
                                    {
                                        if (string.IsNullOrEmpty(CityName))
                                        {
                                            CityName = "";
                                        }
                                        if (string.IsNullOrEmpty(Address))
                                        {
                                            Address = "";
                                        }
                                        if (string.IsNullOrEmpty(ZipCode))
                                        {
                                            ZipCode = "";
                                        }

                                        //await _navigation.PushPopupAsync(new LoaderPopup());
                                        UserDialogs.Instance.ShowLoading("");
                                        string boundary = "---8d0f01e6b3b5dafaaadaad";
                                        MultipartFormDataContent multipartContent = new MultipartFormDataContent(boundary);
                                        multipartContent.Add(new StringContent(FirstCharToUpper(FirstName)), "FirstName");
                                        multipartContent.Add(new StringContent(FirstCharToUpper(LastName)), "LastName");
                                        multipartContent.Add(new StringContent(Email), "Email");

                                        multipartContent.Add(new StringContent(Password), "Password");
                                        multipartContent.Add(new StringContent(CountryCode), "CountryCode");
                                        multipartContent.Add(new StringContent(PhoneNumber), "PhoneNumber");
                                        multipartContent.Add(new StringContent(Address), "Address");
                                        multipartContent.Add(new StringContent(FirstCharToUpper(CityName)), "City");
                                        multipartContent.Add(new StringContent(FirstCharToUpper(CountrySelected.CountryId.ToString())), "CountryId");
                                        multipartContent.Add(new StringContent(4.ToString()), "UserType");
                                        multipartContent.Add(new StringContent(ZipCode), "Zipcode");
                                        //multipartContent.Add(new StringContent(UserPic), "PicturePath");


                                        try
                                        {
                                            if (UserPic != null)
                                            {
                                                StreamImageSource streamImageSource = (StreamImageSource)UserPic;
                                                System.Threading.CancellationToken cancellationToken = System.Threading.CancellationToken.None;
                                                System.Threading.Tasks.Task<System.IO.Stream> task =
                                                    streamImageSource.Stream(cancellationToken);
                                                System.IO.Stream stream = task.Result;

                                                bitmapData = ReadFully(stream);
                                                var fileContent = new ByteArrayContent(bitmapData);

                                                fileContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/octet-stream");
                                                fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
                                                {
                                                    Name = "PicturePath",
                                                    FileName = "profilepic" + ".png",
                                                };

                                                multipartContent.Add(fileContent);
                                            }
                                        }
                                        catch (Exception)
                                        {
                                            //multipartContent.Add(new StringContent("ic_profile.png"), "PicturePath");
                                        }




                                        RegisterResponseModel response;
                                        try
                                        {
                                            response = await webApiRestClient.PostAsync<MultipartFormDataContent, RegisterResponseModel>(ApiHelpers.SignUpApi, multipartContent);
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine("SignupApi_Exception:- " + ex.Message);
                                            response = null;
                                        }
                                        if (response != null)
                                        {
                                            if (response.status)
                                            {
                                                //Application.Current.Properties["CurrentUserId"] = response.userData.UserId;
                                                //Application.Current.SavePropertiesAsync();
                                                //if (userDataDbService.IsUserDbPresentInDB())
                                                //{
                                                //    var data = userDataDbService.ReadAllItems().FirstOrDefault();
                                                //    BsonValue id = data.ID;
                                                //    userDataDbService.UpdateUserDataInDb(id, response.userData);
                                                //}
                                                //else
                                                //{
                                                //    userDataDbService.CreateUserDataInDB(response.userData);
                                                //}
                                                await _navigation.PopAsync();
                                                //StaticHelpers.CustomNavigation(_navigation, new AddPropertyPage());
                                                //MessagingCenter.Send("Register", "IsNavigationFromRegisterPage");
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
                                        await MaterialDialog.Instance.SnackbarAsync(message: AppResource.TermConditionError,
                                                        msDuration: 1000);
                                    } 
                                }
                                else
                                {
                                    await MaterialDialog.Instance.SnackbarAsync(message: AppResource.EmailValidation,
                                                    msDuration: 1000);
                                }
                            }
                            else
                            {
                                await MaterialDialog.Instance.SnackbarAsync(message: AppResource.RegisterServerError,
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
                        Console.WriteLine("RegisterCommand_Exception:- " + ex.Message);
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

        #region LoginCommand
        public Command LoginCommand
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
                        Console.WriteLine("LoginCommand_Exception:- " + ex.Message);
                    }
                });
            }
        }
        #endregion

        #region ProfilePicCommand
        public Command ProfilePicCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        var action = await App.Current.MainPage.DisplayActionSheet("Add Photo", "Cancel", null, "Camera", "Gallery");
                        switch (action)
                        {
                            case "Camera":
                                var CamData = await CameraCommand();
                                UserPic = CamData.ReferenceImages;
                                break;
                            case "Gallery":
                                var GallData = await GalleryCommand();
                                UserPic = GallData.ReferenceImages;
                                break;
                            default:
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("ProfilePicCommand_Exception:- " + ex.Message);
                    }
                });
            }
        }
        #endregion

        #region UserPic Field
        private ImageSource _userPic;

        public ImageSource UserPic
        {
            get { return _userPic; }
            set { SetProperty(ref _userPic, value); }
        }
        #endregion
    }
}
