using Acr.UserDialogs;
using BroomService_App.Helpers;
using BroomService_App.Models;
using BroomService_App.Pages;
using BroomService_App.Pages.CommonPages;
using BroomService_App.Resources;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace BroomService_App.ViewModels
{
    public class ProfileViewModel:BaseViewModel
    {
        public static int? UserId; 
        private UserData currentUserData;

        #region Constructor
        public ProfileViewModel(INavigation navigation):base(navigation)
        {
            UserId = CurrentUserId;
            MessagingCenter.Subscribe<UserData>(this, "ProfileUpdate", (sender) =>
            {
                if (userDataDbService.IsUserDbPresentInDB())
                {
                    var userdata = userDataDbService.ReadAllItems().FirstOrDefault();
                    userdata.FirstName = sender.FirstName;
                    userdata.LastName = sender.LastName;
                    userdata.PicturePath = sender.PicturePath;
                    userdata.Email = sender.Email;
                    userdata.PhoneNumber = sender.PhoneNumber;
                    userdata.CountryName = sender.CountryName;
                    userdata.City = sender.City;
                    userdata.Address = sender.Address;
                    userdata.Zipcode = sender.Zipcode;

                    BsonValue id = userdata.ID;
                    userDataDbService.UpdateUserDataInDb(id, userdata);
                }

                currentUserData = sender;
                ProfileUserName = FirstCharToUpper(sender.FirstName) + " " + FirstCharToUpper(sender.LastName);
                ProfileUserImg = IsImagesValid(sender.PicturePath, ApiHelpers.ApiImageBaseUrl);
                ProfileUserFirstName = sender.FirstName;
                ProfileUserLastName = sender.LastName;
                ProfileUserEmail = sender.Email;
                ProfileUserPhoneNumber = sender.PhoneNumber;
                ProfileUserZipCode = sender.Zipcode;
                CountryCode = sender.CountryCode;
            });
        }
        #endregion

        #region ChangePasswordCommand
        public Command ChangePasswordCommand
        {
            get
            {
                return new Command(() =>
                {
                    try
                    {
                        StaticHelpers.CustomNavigation(_navigation, new ChangePasswordPage());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("ChangePasswordCommand_Exception:- " + ex.Message);
                    }
                });
            }
        }
        #endregion

        #region RightIconCommand
        public Command RightIconCommand
        {
            get
            {
                return new Command(() =>
                {
                    try
                    {
                        UserDialogs.Instance.ShowLoading("");
                        StaticHelpers.CustomNavigation(_navigation, new EditProfilePage());
                        MessagingCenter.Send(currentUserData, "CurrentUserData");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("RightIconCommand_Exception:- " + ex.Message);
                    }
                    finally
                    {
                        UserDialogs.Instance.HideLoading();
                    }
                });
            }
        }
        #endregion

        #region Profile UserImg Field
        private string _profileUserImg;

        public string ProfileUserImg
        {
            get { return _profileUserImg; }
            set { SetProperty(ref _profileUserImg, value); }
        }
        #endregion

        #region Profile Username Field
        private string _profileUserName;

        public string ProfileUserName
        {
            get { return _profileUserName; }
            set { SetProperty(ref _profileUserName, value); }
        }
        #endregion

        #region Profile Firstname Field
        private string _profileUserFirstName;

        public string ProfileUserFirstName
        {
            get { return _profileUserFirstName; }
            set { SetProperty(ref _profileUserFirstName, value); }
        }
        #endregion

        #region Profile LastName Field
        private string _profileUserLastName;

        public string ProfileUserLastName
        {
            get { return _profileUserLastName; }
            set { SetProperty(ref _profileUserLastName, value); }
        }
        #endregion

        #region Profile Email Field
        private string _profileUserEmail;

        public string ProfileUserEmail
        {
            get { return _profileUserEmail; }
            set { SetProperty(ref _profileUserEmail, value); }
        }
        #endregion

        #region Profile Firstname Field
        private string _profileUserPhoneNumber;

        public string ProfileUserPhoneNumber
        {
            get { return _profileUserPhoneNumber; }
            set { SetProperty(ref _profileUserPhoneNumber, value); }
        }
        #endregion

        #region Profile ProfileUserZipCode Field
        private string _profileUserZipCode;

        public string ProfileUserZipCode
        {
            get { return _profileUserZipCode; }
            set { SetProperty(ref _profileUserZipCode, value); }
        }
        #endregion

        #region Profile CountryCode Field
        private string _CountryCode;

        public string CountryCode
        {
            get { return _CountryCode; }
            set { SetProperty(ref _CountryCode, value); }
        }
        #endregion
    }
}
