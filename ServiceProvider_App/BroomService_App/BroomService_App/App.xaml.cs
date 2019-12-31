﻿using Acr.UserDialogs;
using BroomService_App.DependencyInterface;
using BroomService_App.Helpers;
using BroomService_App.Models;
using BroomService_App.Pages;
using BroomService_App.Pages.CommonPages;
using BroomService_App.Resources;
using BroomService_App.Services.ApiService;
using BroomService_App.Services.DBService.LiteDB.ModelDB;
using BroomService_App.ViewModels;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;
using Plugin.FirebasePushNotification;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms;
using XF.Material.Forms.UI.Dialogs;
using XFShimmerLayout.Controls;
using Device = Xamarin.Forms.Device;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace BroomService_App
{
    public partial class App : Application
    {
        protected UserDataDbService userDataDbService;
        private static WebApiRestClient webApiRestClient;
        public static UserData userData;
        //public static string FirebaseToken;
        public static bool IsNotificationRecieved;

        public static double ScreenHeight;
        public static double ScreenWidth;

        public App()
        {
            InitializeComponent();
            userDataDbService = new UserDataDbService();
            webApiRestClient = new WebApiRestClient();
            var density = DeviceDisplay.MainDisplayInfo.Density;
            ShimmerLayout.Init(density);

            Material.Init(this);
            //L10n.SetLocale();



            //var netLanguage = DependencyService.Get<ILocale>().GetCurrent();
            //AppResource.Culture = new CultureInfo(netLanguage);

            if (Application.Current.Properties.ContainsKey("IsAppAlreadyInstalled") && Convert.ToBoolean(Application.Current.Properties["IsAppAlreadyInstalled"]))
            {
                if (Application.Current.Properties.ContainsKey("AppLocale") && !string.IsNullOrEmpty(Application.Current.Properties["AppLocale"].ToString()))
                {
                    var languageculture = Application.Current.Properties["AppLocale"].ToString();
                    Setlanguage(languageculture);
                }
                if (userDataDbService.IsUserDbPresentInDB())
                {
                    userData = userDataDbService.ReadAllItems().FirstOrDefault();
                    Application.Current.Properties["CurrentUserId"] = userData.UserId;
                    Application.Current.Properties["CurrentUserType"] = userData.UserType.Value;
                    BaseViewModel.userTypeEnum = userData.UserType.Value;
                    if (userData.UserType == Convert.ToInt32(UserTypeEnum.ServiceProvider))
                    {
                        App.Current.MainPage = new NavigationPage(new Pages.ServiceProviderFlow.HomeTabbedPage());
                    }
                    else
                    {
                        App.Current.MainPage = new NavigationPage(new Pages.WorkerFlow.HomeTabbedPage());
                    }
                    UpdateDeviceInfo();
                }
                else
                {
                    MainPage = new NavigationPage(new LoginPage());
                }
            }
            else
            {
                Setlanguage("en-US");
                MainPage = new NavigationPage(new ChangeLanguagePage(false));
            }

            //if (userDataDbService.IsUserDbPresentInDB())
            //{
            //    userData = userDataDbService.ReadAllItems().FirstOrDefault();
            //    Application.Current.Properties["CurrentUserId"] = userData.UserId;
            //    Application.Current.Properties["CurrentUserType"] = userData.UserType.Value;
            //    BaseViewModel.userTypeEnum = userData.UserType.Value;
            //    if (userData.UserType == Convert.ToInt32(UserTypeEnum.ServiceProvider))
            //    {
            //        App.Current.MainPage = new NavigationPage(new Pages.ServiceProviderFlow.HomeTabbedPage());
            //    }
            //    else
            //    {
            //        App.Current.MainPage = new NavigationPage(new Pages.WorkerFlow.HomeTabbedPage());
            //    }
            //}
            //else
            //{
            //    MainPage = new NavigationPage(new LoginPage());
            //}
        }

        public static void Setlanguage(string culturevalue)
        {

            var netLanguage = L10n.SetLocale(culturevalue);
            AppResource.Culture = new CultureInfo(netLanguage);
        }

        //public void GetMainPage()
        //{
        //    if (user != null)
        //    {
        //        if (user.UserId != 0)
        //        {
        //            MainPage = new NavigationPage(new HomeAdPage());
        //            //MainPage = new NavigationPage(new HomePage());
        //        }
        //        else
        //        {
        //            MainPage = new NavigationPage(new LoginPage());
        //        }
        //    }
        //    else
        //    {
        //        MainPage = new NavigationPage(new LoginPage());
        //    }
        //}


        protected override void OnStart()
        {
            // Handle when your app starts
            //AppCenter.Start("android=a97b2a41-0589-47b1-92dd-571d0400c65e;" +
            //      "uwp={Your UWP App secret here};" +
            //      "ios={Your iOS App secret here}",
            //      typeof(Analytics), typeof(Crashes),typeof(Distribute));

            if (Device.RuntimePlatform == Device.Android || Device.RuntimePlatform == Device.iOS)
            {

                CrossFirebasePushNotification.Current.RegisterForPushNotifications();

                CrossFirebasePushNotification.Current.Subscribe("general");
                CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
                {
                    System.Diagnostics.Debug.WriteLine($"TOKEN REC: {p.Token}");
                    Application.Current.Properties["AppFirebaseToken"] = p.Token;
                    Application.Current.SavePropertiesAsync();
                    //FirebaseToken = p.Token;
                    //if (userData != null)
                    //{
                    //    UpdateDeviceInfo();
                    //}

                };
                System.Diagnostics.Debug.WriteLine($"TOKEN: {CrossFirebasePushNotification.Current.Token}");

                CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
                {
                    try
                    {
                        IsNotificationRecieved = true;
                        //MessagingCenter.Send("5", "NotificationRecieved");
                        System.Diagnostics.Debug.WriteLine("Received");
                        if (p.Data.ContainsKey("body"))
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                var data = $"{p.Data["body"]}";
                            });

                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("NotificationRecieved_Exception:-->" + ex.Message);
                    }

                };

                CrossFirebasePushNotification.Current.OnNotificationOpened += async(s, p) =>
                {
                    //System.Diagnostics.Debug.WriteLine(p.Identifier);

                    try
                    {
                        if(Device.RuntimePlatform == Device.iOS)
                        {
                            if (p.Data.ContainsKey("aps.alert"))
                            {
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    var data = $"{p.Data["aps.alert"]}";
                                    if(data.StartsWith("You have a new message"))
                                    {
                                        if (userData.UserType == Convert.ToInt32(UserTypeEnum.ServiceProvider))
                                        {
                                            App.Current.MainPage = new NavigationPage(new Pages.ServiceProviderFlow.HomeTabbedPage());
                                            MessagingCenter.Send("Chat_Tab", "HomeTabBar");
                                        }
                                        else
                                        {
                                            App.Current.MainPage = new NavigationPage(new Pages.WorkerFlow.HomeTabbedPage());
                                            MessagingCenter.Send("Chat_Tab", "HomeTabBar");
                                        }
                                    }
                                    else
                                    {
                                        if (userData.UserType == Convert.ToInt32(UserTypeEnum.ServiceProvider))
                                        {
                                            App.Current.MainPage = new NavigationPage(new Pages.ServiceProviderFlow.HomeTabbedPage());
                                            MessagingCenter.Send("Notification_Tab", "HomeTabBar");
                                        }
                                        else
                                        {
                                            App.Current.MainPage = new NavigationPage(new Pages.WorkerFlow.HomeTabbedPage());
                                            MessagingCenter.Send("Notification_Tab", "HomeTabBar");
                                        }
                                    }
                                });

                            }
                        }
                        else if(Device.RuntimePlatform == Device.Android)
                        {
                            if (p.Data.ContainsKey("body"))
                            {
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    var data = $"{p.Data["body"]}";

                                    if (data.StartsWith("You have a new message"))
                                    {
                                        if (userData.UserType == Convert.ToInt32(UserTypeEnum.ServiceProvider))
                                        {
                                            App.Current.MainPage = new NavigationPage(new Pages.ServiceProviderFlow.HomeTabbedPage());
                                            MessagingCenter.Send("Chat_Tab", "HomeTabBar");
                                        }
                                        else
                                        {
                                            App.Current.MainPage = new NavigationPage(new Pages.WorkerFlow.HomeTabbedPage());
                                            MessagingCenter.Send("Chat_Tab", "HomeTabBar");
                                        }
                                    }
                                    else
                                    {
                                        if (userData.UserType == Convert.ToInt32(UserTypeEnum.ServiceProvider))
                                        {
                                            App.Current.MainPage = new NavigationPage(new Pages.ServiceProviderFlow.HomeTabbedPage());
                                            MessagingCenter.Send("Notification_Tab", "HomeTabBar");
                                        }
                                        else
                                        {
                                            App.Current.MainPage = new NavigationPage(new Pages.WorkerFlow.HomeTabbedPage());
                                            MessagingCenter.Send("Notification_Tab", "HomeTabBar");
                                        }
                                    }
                                });

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Some other exception occurred
                    }
                    //System.Diagnostics.Debug.WriteLine("Opened");
                    //foreach (var data in p.Data)
                    //{
                    //    System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                    //}

                    //if (!string.IsNullOrEmpty(p.Identifier))
                    //{
                    //    Device.BeginInvokeOnMainThread(() =>
                    //    {
                    //        var data = p.Identifier;
                    //    });
                    //}
                    //else if (p.Data.ContainsKey("color"))
                    //{
                    //    Device.BeginInvokeOnMainThread(() =>
                    //    {
                    //        //mPage.Navigation.PushAsync(new ContentPage()
                    //        //{
                    //        //    BackgroundColor = Color.FromHex($"{p.Data["color"]}")

                    //        //});
                    //    });

                    //}
                    //else if (p.Data.ContainsKey("aps.alert.title"))
                    //{
                    //    Device.BeginInvokeOnMainThread(() =>
                    //    {
                    //        var data = $"{p.Data["aps.alert.title"]}";
                    //    });

                    //}
                };

                CrossFirebasePushNotification.Current.OnNotificationAction += (s, p) =>
                {
                    System.Diagnostics.Debug.WriteLine("Action");

                    if (!string.IsNullOrEmpty(p.Identifier))
                    {
                        System.Diagnostics.Debug.WriteLine($"ActionId: {p.Identifier}");
                        foreach (var data in p.Data)
                        {
                            System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                        }

                    }

                };

                CrossFirebasePushNotification.Current.OnNotificationDeleted += (s, p) =>
                {
                    System.Diagnostics.Debug.WriteLine("Dismissed");
                };
            }
        }

        //public static void UpdateFirebaseService(UserData userData = null)
        //{
        //    if (Device.RuntimePlatform == Device.Android)
        //    {

        //        CrossFirebasePushNotification.Current.RegisterForPushNotifications();

        //        CrossFirebasePushNotification.Current.Subscribe("general");
        //        CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
        //        {
        //            System.Diagnostics.Debug.WriteLine($"TOKEN REC: {p.Token}");
        //            FirebaseToken = p.Token;

        //            if (userData != null)
        //            {
        //                UpdateDeviceInfo(userData);
        //            }

        //        };
        //        System.Diagnostics.Debug.WriteLine($"TOKEN: {CrossFirebasePushNotification.Current.Token}");

        //        CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
        //        {
        //            try
        //            {
        //                System.Diagnostics.Debug.WriteLine("Received");
        //                if (p.Data.ContainsKey("body"))
        //                {
        //                    Device.BeginInvokeOnMainThread(() =>
        //                    {
        //                        var data = $"{p.Data["body"]}";
        //                    });

        //                }
        //            }
        //            catch (Exception ex)
        //            {

        //            }

        //        };

        //        CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
        //        {
        //            //System.Diagnostics.Debug.WriteLine(p.Identifier);

        //            System.Diagnostics.Debug.WriteLine("Opened");
        //            foreach (var data in p.Data)
        //            {
        //                System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
        //            }

        //            if (!string.IsNullOrEmpty(p.Identifier))
        //            {
        //                Device.BeginInvokeOnMainThread(() =>
        //                {
        //                    var data = p.Identifier;
        //                });
        //            }
        //            else if (p.Data.ContainsKey("color"))
        //            {
        //                Device.BeginInvokeOnMainThread(() =>
        //                {
        //                    //mPage.Navigation.PushAsync(new ContentPage()
        //                    //{
        //                    //    BackgroundColor = Color.FromHex($"{p.Data["color"]}")

        //                    //});
        //                });

        //            }
        //            else if (p.Data.ContainsKey("aps.alert.title"))
        //            {
        //                Device.BeginInvokeOnMainThread(() =>
        //                {
        //                    var data = $"{p.Data["aps.alert.title"]}";
        //                });

        //            }
        //        };

        //        CrossFirebasePushNotification.Current.OnNotificationAction += (s, p) =>
        //        {
        //            System.Diagnostics.Debug.WriteLine("Action");

        //            if (!string.IsNullOrEmpty(p.Identifier))
        //            {
        //                System.Diagnostics.Debug.WriteLine($"ActionId: {p.Identifier}");
        //                foreach (var data in p.Data)
        //                {
        //                    System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
        //                }

        //            }

        //        };

        //        CrossFirebasePushNotification.Current.OnNotificationDeleted += (s, p) =>
        //        {
        //            System.Diagnostics.Debug.WriteLine("Dismissed");
        //        };
        //    }
        //}

        private async void UpdateDeviceInfo()
        {
            var deviceRequestmodel = new UpdateDeviceInfoModel()
            {
                DeviceId = Device.RuntimePlatform == Device.Android ? 1 : Device.RuntimePlatform == Device.iOS ? 2 : 0,
                UserId = userData.UserId,
                DeviceToken = Application.Current.Properties.ContainsKey("AppFirebaseToken") ? Application.Current.Properties["AppFirebaseToken"].ToString() : null
            };
            UpdateDeviceInfoResponse deviceInfoResponse;
            try
            {
                deviceInfoResponse = await webApiRestClient.PostAsync<UpdateDeviceInfoModel, UpdateDeviceInfoResponse>(ApiHelpers.UpdateDeviceInfo, deviceRequestmodel);
            }
            catch (Exception ex)
            {
                Console.WriteLine("UpdateDeviceApi_Exception:-->" + ex.Message);
                deviceInfoResponse = null;
            }
            if (deviceInfoResponse != null)
            {
                if (deviceInfoResponse.status)
                {
                    await MaterialDialog.Instance.SnackbarAsync(message: AppResource.DeviceInfoupdated,
                        msDuration: MaterialSnackbar.DurationShort);
                }
                else
                {
                    await MaterialDialog.Instance.SnackbarAsync(message: deviceInfoResponse.message,
                        msDuration: MaterialSnackbar.DurationShort);
                }
            }
            else
            {
                await MaterialDialog.Instance.SnackbarAsync(message: AppResource.ExceptionDeviceInfo,
                        msDuration: MaterialSnackbar.DurationShort);
            }
        }

        protected override void OnSleep()
        {
            Application.Current.Properties["isBackground"] = true;
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            Application.Current.Properties["isBackground"] = false;
            // Handle when your app resumes
        }
    }
}
