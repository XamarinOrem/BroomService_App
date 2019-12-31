using System;
using System.Collections.Generic;
using System.Linq;
using BroomService_App.Helpers;
using Foundation;
using ImageCircle.Forms.Plugin.iOS;
using Microsoft.AppCenter.Distribute;
using Plugin.FirebasePushNotification;
using UIKit;
using UserNotifications;
using Xamarin.Forms;
using XF.Material.iOS;

namespace BroomService_App.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            App.ScreenWidth = UIScreen.MainScreen.Bounds.Width;
            App.ScreenHeight = UIScreen.MainScreen.Bounds.Height;

            Rg.Plugins.Popup.Popup.Init();
            Forms.SetFlags("CollectionView_Experimental");
            global::Xamarin.Forms.Forms.Init();
            Material.Init();
            ImageCircleRenderer.Init();
            //Distribute.DontCheckForUpdatesInDebug();
            
            LoadApplication(new App());
            FirebasePushNotificationManager.Initialize(options, true);

            return base.FinishedLaunching(app, options);
        }

        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            FirebasePushNotificationManager.DidRegisterRemoteNotifications(deviceToken);
        }

        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {
            FirebasePushNotificationManager.RemoteNotificationRegistrationFailed(error);

        }
        // To receive notifications in foregroung on iOS 9 and below.
        // To receive notifications in background in any iOS version
        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            // If you are receiving a notification message while your app is in the background,
            // this callback will not be fired 'till the user taps on the notification launching the application.

            // If you disable method swizzling, you'll need to call this method. 
            // This lets FCM track message delivery and analytics, which is performed
            // automatically with method swizzling enabled.
            FirebasePushNotificationManager.DidReceiveMessage(userInfo);
            // Do your magic to handle the notification data
            System.Console.WriteLine(userInfo);

            completionHandler(UIBackgroundFetchResult.NewData);


            if(application.ApplicationState == UIApplicationState.Active)
            {
                var aps_d = userInfo["aps"] as NSDictionary;
                var alert_d = aps_d["alert"] as NSString;

                var NotificationData = alert_d.ToString();

                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Broom Service", NotificationData, "Ok", "Cancel"); // since we are using async, we should specify the DisplayAlert as awaiting.
                    if (result) // if it's equal to Ok
                    {
                        if (NotificationData.StartsWith("You have a new message"))
                        {
                            if (App.userData.UserType == Convert.ToInt32(UserTypeEnum.ServiceProvider))
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
                            if (App.userData.UserType == Convert.ToInt32(UserTypeEnum.ServiceProvider))
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
                    }
                    else // if it's equal to Cancel
                    {
                        return;
                    }
                });



            }
        }

        // iOS 10, fire when recieve notification foreground
        [Export("userNotificationCenter:willPresentNotification:withCompletionHandler:")]
        public void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
        {
            var title = notification.Request.Content.Title;
            var body = notification.Request.Content.Body;
            completionHandler(UNNotificationPresentationOptions.Alert);
            //debugAlert(title, body);
        }
    }
}