using System;
using System.ComponentModel;
using System.Threading.Tasks; 
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Net.Http;
using BroomService_App.Models;
using BroomService_App.ViewModels;
using BroomService_App.Pages;
using BroomService_App.Resources;
using BroomService_App.Helpers;
using BroomService_App.Pages.CommonPages;
using LiteDB;
using XF.Material.Forms.UI.Dialogs;
using Acr.UserDialogs;
using Xamarin.Essentials;

namespace BroomService_App.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private string _userImage;
        public string UserImage
        {
            get
            {
                return _userImage;

            }
            set
            {
                _userImage = value;
                SetProperty(ref _userImage, value);
            }
        }
        private string _userName;
        public string UserName
        {
            get
            {
                return _userName;

            }
            set
            {
                _userName = value;
                SetProperty(ref _userName, value);
            }
        }

        private List<MasterPageItem> _menuList;
        public List<MasterPageItem> MenuList
        {
            get
            {
                return _menuList;

            }
            set
            {
                _menuList = value;
                SetProperty(ref _menuList, value);
            }
        }
         
        MasterPageItem _menuSelectedItem;
        public MasterPageItem MenuSelectedItem
        {
            get
            {
                return _menuSelectedItem;
            }
            set
            {
                _menuSelectedItem = value;
                SetProperty(ref _menuSelectedItem, value);
                try
                {  
                    if (_menuSelectedItem != null)
                    { 
                        if (_menuSelectedItem.Title == AppResource.Logout)
                        {
                            _logOut();
                            MenuSelectedItem = null;
                            //_navigation.PopAsync();
                            return;
                        } 
                        else if (_menuSelectedItem.Title == "")
                        {
                            MenuSelectedItem = null;
                            _navigation.PopAsync();
                            return;
                        }
                        else
                        {
                            var page = (Page)Activator.CreateInstance(_menuSelectedItem.TargetType);
                            MenuSelectedItem = null;
                            _navigation.PopAsync();
                            _navigation.PushAsync(page);
                           
                        }

                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
      
        async void _logOut()
        {
            var answer = await App.Current.MainPage.DisplayAlert(AppResource.Logout,
               AppResource.LogoutMsg, AppResource.Yes, AppResource.No);
            if (answer)
            {
                try
                {
                    if ((Connectivity.ConnectionProfiles.Contains(ConnectionProfile.WiFi) && Connectivity.NetworkAccess.Equals(NetworkAccess.Internet)) || (Connectivity.ConnectionProfiles.Contains(ConnectionProfile.Cellular) && Connectivity.NetworkAccess.Equals(NetworkAccess.Internet)))
                    {  //await _navigation.PushPopupAsync(new LoaderPopup());
                        UserDialogs.Instance.ShowLoading("");
                        var requestModel = new LogoutModel()
                        {
                            UserId = CurrentUserId
                        };

                        LogoutResponseModel response;
                        try
                        {
                            response = await webApiRestClient.PostAsync<LogoutModel, LogoutResponseModel>(ApiHelpers.Logout, requestModel);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("LogoutApi_Exception:- " + ex.Message);
                            response = null;
                        }
                        if (response != null)
                        {
                            if (response.status)
                            {
                                //App.Database.ClearLoginDetails(); 
                                if (Application.Current.Properties.ContainsKey("CurrentUserId"))
                                {
                                    Application.Current.Properties.Remove("CurrentUserId"); ;
                                    await Application.Current.SavePropertiesAsync();
                                }
                                CurrentUserId = 0;
                                if (userDataDbService.IsUserDbPresentInDB())
                                {
                                    var data = userDataDbService.ReadAllItems().FirstOrDefault();
                                    BsonValue id = data.ID;
                                    userDataDbService.DeleteItemFromDB(id, data);
                                }

                                App.Current.MainPage = new NavigationPage(new LoginPage());
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
                    else
                    {
                        await MaterialDialog.Instance.SnackbarAsync(message: AppResource.NoInternetError,
                                                msDuration: MaterialSnackbar.DurationShort);
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
            }
            //if (answer)
            //{
            //    if (Application.Current.Properties.ContainsKey("CurrentUserId"))
            //    {
            //        Application.Current.Properties.Remove("CurrentUserId"); ;
            //        await Application.Current.SavePropertiesAsync();
            //    }
            //    CurrentUserId = 0;
            //    if (userDataDbService.IsUserDbPresentInDB())
            //    {
            //        var data = userDataDbService.ReadAllItems().FirstOrDefault();
            //        BsonValue id = data.ID;
            //        userDataDbService.DeleteItemFromDB(id, data);
            //    }

            //    App.Current.MainPage = new NavigationPage(new LoginPage());
            //}
        }
        public Command CloseCommand
        {
            get
            {
                return new Command((e) =>
                {
                    try
                    {
                        _navigation.PopAsync();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("CloseCommand_Exception:- " + ex.Message);
                    }
                });
            }
        }



        public MenuViewModel(INavigation navigation):base(navigation)
        {
            if (userDataDbService.IsUserDbPresentInDB())
            {
                var data = userDataDbService.ReadAllItems().FirstOrDefault();

                UserName = data.FirstName + " " + data.LastName;
                UserImage = IsImagesValid(data.PicturePath, ApiHelpers.ApiImageBaseUrl);
            }
            //MessagingCenter.Subscribe<ProfileViewModel>(this, "UpdateProfile", (sender) =>
            //{
            //    if (!string.IsNullOrEmpty(LoginUserDetails.image))
            //    {
            //        UserImage = new UriImageSource()
            //        {
            //            Uri = new Uri(LoginUserDetails.image),
            //            CacheValidity = TimeSpan.FromHours(24),
            //            CachingEnabled = true
            //        };
            //    } 
            //    MessagingCenter.Unsubscribe<ProfileViewModel>(sender, "UpdateProfile");
            //});

            //if (!string.IsNullOrEmpty(LoginUserDetails.image))
            //{
            //    _userImage = new UriImageSource()
            //    {
            //        Uri = new Uri(LoginUserDetails.image),
            //        CacheValidity = TimeSpan.FromHours(24),
            //        CachingEnabled = true
            //    };
            //}
            //else
            //{
            //    _userImage = "ic_placeholder.png";
            //}
            //_userName = LoginUserDetails.name; // LoginUserDetails.name;
            _menuList = new List<MasterPageItem>();

            _menuList.Add(new MasterPageItem
            {
                Title = AppResource.Chat,
                TargetType = typeof(ChatListPage),
                IconSource = "ic_drawer_chat.png",
                IsMenuItem = true,
                IsNotMenuItem = false
            });
            _menuList.Add(new MasterPageItem
            {
                Title = AppResource.changelang_headerTitle,
                TargetType = typeof(ChangeLanguagePage),
                IconSource = "ic_language.png",
                IsMenuItem = true,
                IsNotMenuItem = false
            });
            _menuList.Add(new MasterPageItem
            {
                Title = AppResource.TermandConditions,
                TargetType = typeof(TermsConditionsPage),
                IconSource = "ic_drawer_terms.png",
                IsMenuItem = true,
                IsNotMenuItem = false
            });
            _menuList.Add(new MasterPageItem
            {
                Title = AppResource.AboutUs,
                TargetType = typeof(AboutUsPage),
                IconSource = "ic_drawer_about.png",
                IsMenuItem = true,
                IsNotMenuItem = false
            });
            _menuList.Add(new MasterPageItem
            {
                Title = AppResource.ContactUs,
                TargetType = typeof(ContactUsPage),
                IconSource = "ic_drawer_contact.png",
                IsMenuItem = true,
                IsNotMenuItem = false
            });
            _menuList.Add(new MasterPageItem
            {
                Title = AppResource.Logout,
                TargetType = typeof(LoginPage),
                IconSource = "ic_drawer_logout.png",
                IsMenuItem = true,
                IsNotMenuItem = false
            });
            _menuList.Add(new MasterPageItem
            {
                Title = "",
                TargetType = typeof(LoginPage),
                IconSource = "ic_drawer_logout.png",
                IsMenuItem = false,
                IsNotMenuItem = true
            });
        }
       
        public Command ViewProfileCommand
        {
            get
            {
                return new Command((e) =>
                {
                    try
                    {
                        _navigation.PopAsync();
                        MessagingCenter.Send("Profile_Tab", "HomeTabBar");
                        //HomeMasterPage.page.IsPresented = false;
                        //_navigation.PushAsync(new EditProfilePage());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("ViewProfileCommand_Exception:- " + ex.Message);
                    }
                });
            }
        }
        public void CollectionDidChange(object sender,
                                NotifyCollectionChangedEventArgs e)
        {

        }

    }
}

