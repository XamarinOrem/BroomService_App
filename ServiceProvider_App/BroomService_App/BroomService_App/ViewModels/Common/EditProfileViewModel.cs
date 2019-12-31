using Acr.UserDialogs;
using BroomService_App.Helpers;
using BroomService_App.Models;
using BroomService_App.Pages;
using BroomService_App.Popups;
using BroomService_App.Resources;
using LiteDB;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace BroomService_App.ViewModels.Common
{
    public class EditProfileViewModel : BaseViewModel
    {
        private List<CountryDetailDataModel> countryDetailData;
        private UserData profileData;

        #region CountryCode
        private string _CountryCode;

        public string CountryCode
        {
            get { return _CountryCode; }
            set { SetProperty(ref _CountryCode, value); }
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
            set { SetProperty(ref _CountryCodeList, value); }
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

        #region property for the PhoneNumber field
        private string _phoneNumber;
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { SetProperty(ref _phoneNumber, value); }
        }
        #endregion

        //#region property for the Country field
        //private string _countryName;
        //public string Country
        //{
        //    get { return _countryName; }
        //    set { SetProperty(ref _countryName, value); }
        //}
        //#endregion

        #region property for the City field
        private string _cityName;
        public string City
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

        #region property for the Zipcode field
        private string _zipcode;
        public string Zipcode
        {
            get { return _zipcode; }
            set { SetProperty(ref _zipcode, value); }
        }
        #endregion

        #region Constructor
        public EditProfileViewModel(INavigation navigation) : base(navigation)
        {
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;

            MessagingCenter.Subscribe<UserData>(this, "CurrentUserData", (userdata) =>
            {
                profileData = userdata;
                FirstName = userdata.FirstName;
                LastName = userdata.LastName;
                ProfileUserImg = IsImagesValid(userdata.PicturePath, ApiHelpers.ApiImageBaseUrl);
                //ProfileUserImg = userdata.PicturePath != null ? ApiHelpers.ApiImageBaseUrl + userdata.PicturePath : "user_img.png";
                Email = userdata.Email;
                PhoneNumber = userdata.PhoneNumber;
                SelectedIndexChanged = 0;
                City = userdata.City;
                Address = userdata.Address;
                Zipcode = userdata.Zipcode;
                CountryCode = userdata.CountryCode;
                MessagingCenter.Unsubscribe<UserData>(this, "CurrentUserData");
            });
            GetCountryDetailApi();
        }
        #endregion

        #region Profile UserImg Field
        private ImageSource _profileUserImg;

        public ImageSource ProfileUserImg
        {
            get { return _profileUserImg; }
            set { SetProperty(ref _profileUserImg, value); }
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
            if (Connectivity.NetworkAccess.Equals(NetworkAccess.Internet) || Connectivity.ConnectionProfiles.Contains(ConnectionProfile.WiFi))
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
                        foreach (var data in countryDetailData)
                        {
                            CountryPickerList.Add(data);
                            CountryCodeList.Add(data);
                        }

                        var item = CountryPickerList.Where(x => x.CountryId == profileData.CountryId).FirstOrDefault();
                        CountrySelectedIndexChanged = CountryPickerList.IndexOf(item);

                        var CountryCodeitem = CountryCodeList.Where(x => x.CountryCode == profileData.CountryCode).FirstOrDefault();
                        SelectedIndexChanged = CountryCodeList.IndexOf(item);

                        //if (countryDetailData != null)
                        //{
                        //    foreach (var data in countryDetailData)
                        //    {
                        //        if (profileData.CountryId == data.CountryId)
                        //        {
                        //            CountrySelectedIndexChanged = data.CountryId - 1;
                        //        }
                        //    }
                        //}
                    }
                    else
                    {
                        await MaterialDialog.Instance.SnackbarAsync(message: response.message,
                                    msDuration: MaterialSnackbar.DurationShort);
                        countryDetailData = null;
                    }
                }
                else
                {
                    await MaterialDialog.Instance.SnackbarAsync(message: AppResource.ServerError,
                                    msDuration: MaterialSnackbar.DurationShort);
                    countryDetailData = null;
                }
            }
            else
            {
                await MaterialDialog.Instance.SnackbarAsync(message: AppResource.NoInternetError,
                                        msDuration: MaterialSnackbar.DurationShort);
            }
        }
        #endregion

        #region ChangeUserPicCommand
        public Command ChangeUserPicCommand
        {
            get
            {
                return new Command(async() =>
                {
                    try
                    {
                        var action = await App.Current.MainPage.DisplayActionSheet("Add Photo", "Cancel", null, "Camera", "Gallery");
                        switch (action)
                        {
                            case "Camera":
                                ProfileUserImg = await CameraCommand();
                                break;
                            case "Gallery":
                                ProfileUserImg = await GalleryCommand();
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
        
        //#region SaveBtnCommand
        //public Command RightIconCommand
        //{
        //    get
        //    {
        //        return new Command(async() =>
        //        {
        //            try
        //            {
        //                byte[] bitmapData;
        //                if (Connectivity.NetworkAccess.Equals(NetworkAccess.Internet) || Connectivity.ConnectionProfiles.Contains(ConnectionProfile.WiFi))
        //                {
        //                    //await _navigation.PushPopupAsync(new LoaderPopup());
        //                    if (!string.IsNullOrEmpty(FirstName) && !string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrEmpty(LastName) && !string.IsNullOrWhiteSpace(LastName))
        //                    {
        //                        UserDialogs.Instance.ShowLoading("");
        //                        try
        //                        {
        //                            string boundary = "---8d0f01e6b3b5dafaaadaad";
        //                            MultipartFormDataContent multipartContent = new MultipartFormDataContent(boundary);
        //                            multipartContent.Add(new StringContent(FirstName), "FirstName");
        //                            multipartContent.Add(new StringContent(LastName), "LastName");
        //                            multipartContent.Add(new StringContent(Email), "Email");

        //                            multipartContent.Add(new StringContent("123456"), "Password");
        //                            //multipartContent.Add(new StringContent(SelectedItem), "CountryCode");
        //                            multipartContent.Add(new StringContent(PhoneNumber), "PhoneNumber");
        //                            multipartContent.Add(new StringContent(Address), "Address");
        //                            multipartContent.Add(new StringContent(City), "City");
        //                            multipartContent.Add(new StringContent(CountrySelected.CountryId.ToString()), "CountryId");
        //                            multipartContent.Add(new StringContent(4.ToString()), "UserType");
        //                            multipartContent.Add(new StringContent(Zipcode), "Zipcode");
        //                            multipartContent.Add(new StringContent(CurrentUserId.ToString()), "UserId");
        //                            //multipartContent.Add(new StringContent(ProfileUserImg), "PicturePath");

        //                            try
        //                            {
        //                                StreamImageSource streamImageSource = (StreamImageSource)ProfileUserImg;
        //                                System.Threading.CancellationToken cancellationToken = System.Threading.CancellationToken.None;
        //                                System.Threading.Tasks.Task<System.IO.Stream> task =
        //                                    streamImageSource.Stream(cancellationToken);
        //                                System.IO.Stream stream = task.Result;

        //                                bitmapData = ReadFully(stream);
        //                                var fileContent = new ByteArrayContent(bitmapData);

        //                                fileContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/octet-stream");
        //                                fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
        //                                {
        //                                    Name = "PicturePath",
        //                                    FileName = "profilepic" + ".png",
        //                                };

        //                                multipartContent.Add(fileContent);
        //                            }
        //                            catch (Exception ex)
        //                            {
        //                                Console.WriteLine("Adding Image to api:- " + ex.Message);
        //                            }

        //                            EditProfileModel response;
        //                            try
        //                            {
        //                                response = await webApiRestClient.PostAsync<MultipartFormDataContent, EditProfileModel>(ApiHelpers.EditProfile, multipartContent);
        //                            }
        //                            catch (Exception ex)
        //                            {
        //                                Console.WriteLine("EditProfileApi_Exception:- " + ex.Message);
        //                                response = null;
        //                            }
        //                            if (response != null)
        //                            {
        //                                if (response.status)
        //                                {
        //                                    App.Current.MainPage = new NavigationPage(new Pages.ServiceProviderFlow.HomeTabbedPage());
        //                                    await MaterialDialog.Instance.SnackbarAsync(message: response.message,
        //                                            msDuration: MaterialSnackbar.DurationShort);
        //                                    UserDialogs.Instance.HideLoading();
        //                                    MessagingCenter.Send("Profile_Tab", "HomeTabBar");
        //                                }
        //                                else
        //                                {
        //                                    await MaterialDialog.Instance.SnackbarAsync(message: response.message,
        //                                                msDuration: MaterialSnackbar.DurationShort);
        //                                }
        //                            }
        //                            else
        //                            {
        //                                await MaterialDialog.Instance.SnackbarAsync(message: AppResource.ServerError,
        //                                                msDuration: MaterialSnackbar.DurationShort);
        //                            }
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            UserDialogs.Instance.HideLoading();
        //                            Console.WriteLine("SaveBtnCommand_Exception:- " + ex.Message);
        //                        }
        //                        UserDialogs.Instance.HideLoading();
        //                    }
        //                    else
        //                    {
        //                        await MaterialDialog.Instance.SnackbarAsync(message: AppResource.ProfileNameError,
        //                                            msDuration: MaterialSnackbar.DurationShort);
        //                    }
        //                }
        //                else
        //                {
        //                    await MaterialDialog.Instance.SnackbarAsync(message: AppResource.NoInternetError,
        //                                            msDuration: MaterialSnackbar.DurationShort);
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine("SavingButtonProfile_Exception:-->" + ex.Message);
        //                UserDialogs.Instance.HideLoading();
        //            }
        //            finally
        //            {
        //                //await _navigation.PopAllPopupAsync(true);
        //            }
        //        });
        //    }
        //}
        //#endregion

        #region SaveBtnCommand
        public Command RightIconCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        byte[] bitmapData;
                        if (Connectivity.NetworkAccess.Equals(NetworkAccess.Internet) || Connectivity.ConnectionProfiles.Contains(ConnectionProfile.WiFi))
                        {
                            //await _navigation.PushPopupAsync(new LoaderPopup());
                            if (!string.IsNullOrEmpty(FirstName) && !string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrEmpty(CountryCode) && !string.IsNullOrWhiteSpace(CountryCode) && !string.IsNullOrEmpty(LastName) && !string.IsNullOrWhiteSpace(LastName))
                            {
                                UserDialogs.Instance.ShowLoading("");
                                try
                                {
                                    string boundary = "---8d0f01e6b3b5dafaaadaad";
                                    MultipartFormDataContent multipartContent = new MultipartFormDataContent(boundary);
                                    multipartContent.Add(new StringContent(FirstName), "FirstName");
                                    multipartContent.Add(new StringContent(LastName), "LastName");
                                    multipartContent.Add(new StringContent(Email), "Email");

                                    //multipartContent.Add(new StringContent("123456"), "Password");
                                    multipartContent.Add(new StringContent(CountryCode), "CountryCode");
                                    //if (CountryCodeSelected != null)
                                    //{
                                    //    try
                                    //    {
                                    //        multipartContent.Add(new StringContent(CountryCodeSelected.CountryCode), "CountryCode");
                                    //    }
                                    //    catch (Exception ex)
                                    //    {
                                    //        Console.WriteLine(ex.Message);
                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    UserDialogs.Instance.HideLoading();
                                    //    await MaterialDialog.Instance.SnackbarAsync(message: AppResource.CountryCodeError,
                                    //                    msDuration: 1000);
                                    //    return;
                                    //}
                                    multipartContent.Add(new StringContent(PhoneNumber), "PhoneNumber");
                                    multipartContent.Add(new StringContent(Address == null ? String.Empty : Address), "Address");
                                    multipartContent.Add(new StringContent(City == null ? string.Empty : City), "City");

                                    try
                                    {
                                        multipartContent.Add(new StringContent(CountrySelected.CountryId.ToString()), "CountryId");
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    multipartContent.Add(new StringContent(4.ToString()), "UserType");
                                    multipartContent.Add(new StringContent(Zipcode == null ? String.Empty : Zipcode), "Zipcode");
                                    multipartContent.Add(new StringContent(CurrentUserId.ToString()), "UserId");
                                    //multipartContent.Add(new StringContent(ProfileUserImg), "PicturePath");

                                    try
                                    {
                                        StreamImageSource streamImageSource = (StreamImageSource)ProfileUserImg;
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
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("Adding Image to api:- " + ex.Message);
                                    }

                                    EditProfileModel response;
                                    try
                                    {
                                        response = await webApiRestClient.PostAsync<MultipartFormDataContent, EditProfileModel>(ApiHelpers.EditProfile, multipartContent);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("EditProfileApi_Exception:- " + ex.Message);
                                        response = null;
                                    }
                                    if (response != null)
                                    {
                                        if (response.status)
                                        {
                                            //if (IsCustomerFlow)
                                            //{
                                            //    App.Current.MainPage = new NavigationPage(new HomeTabPage());
                                            //    await MaterialDialog.Instance.SnackbarAsync(message: response.message,
                                            //            msDuration: 1000);
                                            //    UserDialogs.Instance.HideLoading();
                                            //    MessagingCenter.Send("Profile_Tab", "HomeTabBar");
                                            //}
                                            //else
                                            //{
                                            //    App.Current.MainPage = new NavigationPage(new Pages.ServiceProviderFlow.HomeTabbedPage());
                                            //    await MaterialDialog.Instance.SnackbarAsync(message: response.message,
                                            //            msDuration: 1000);
                                            //    UserDialogs.Instance.HideLoading();
                                            //    MessagingCenter.Send("Profile_Tab", "HomeTabBar");
                                            //}
                                            App.Current.MainPage = new NavigationPage(new Pages.ServiceProviderFlow.HomeTabbedPage());
                                            await MaterialDialog.Instance.SnackbarAsync(message: response.message,
                                                    msDuration: 1000);
                                            UserDialogs.Instance.HideLoading();
                                            MessagingCenter.Send("Profile_Tab", "HomeTabBar");
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
                                catch (Exception ex)
                                {
                                    UserDialogs.Instance.HideLoading();
                                    Console.WriteLine("SaveBtnCommand_Exception:- " + ex.Message);
                                }
                                UserDialogs.Instance.HideLoading();
                            }
                            else
                            {
                                await MaterialDialog.Instance.SnackbarAsync(message: AppResource.ProfileNameError,
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

        #region Picker SelectedIndexChanged
        private int _selectedIndexChanged;

        public int SelectedIndexChanged
        {
            get { return _selectedIndexChanged; }
            set { SetProperty(ref _selectedIndexChanged, value); }
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

    }
}
