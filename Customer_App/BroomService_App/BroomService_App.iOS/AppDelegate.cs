using System;
using System.Collections.Generic;
using System.Linq;
using BroomService_App.Pages;
using FFImageLoading.Forms.Platform;
using Foundation;
using ImageCircle.Forms.Plugin.iOS;
using Microsoft.AppCenter.Distribute;
using Plugin.FirebasePushNotification;
using Plugin.FirebasePushNotification.Abstractions;
using Syncfusion.SfRating.XForms.iOS;
using UIKit;
using UserNotifications;
using Xamarin;
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
            FormsMaps.Init();
            Forms.SetFlags("CollectionView_Experimental");
            global::Xamarin.Forms.Forms.Init();
            CachedImageRenderer.Init();
            new SfRatingRenderer();
            Material.Init();
            ImageCircleRenderer.Init();
            //Distribute.DontCheckForUpdatesInDebug();
            
            LoadApplication(new App());
            //FirebasePushNotificationManager.Initialize(options, true);


            FirebasePushNotificationManager.Initialize(options, new NotificationUserCategory[]
                       {
                    new NotificationUserCategory("message",new List<NotificationUserAction> {
                        new NotificationUserAction("Reply","Reply",NotificationActionType.Foreground)
                    }),
                    new NotificationUserCategory("request",new List<NotificationUserAction> {
                        new NotificationUserAction("Accept","Accept"),
                        new NotificationUserAction("Reject","Reject",NotificationActionType.Destructive)
                    })

                       });
            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                var authOptions = UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound;
                UNUserNotificationCenter.Current.RequestAuthorization(authOptions, (granted, error) =>
                {
                    Console.WriteLine(granted);
                });
                UNUserNotificationCenter.Current.Delegate = new MyNotificationCenterDelegate();
            }
            else
            {
                // iOS 9 <=
                var allNotificationTypes = UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound;
                var settings = UIUserNotificationSettings.GetSettingsForTypes(allNotificationTypes, null);
                UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
            }


            UIApplication.SharedApplication.StatusBarHidden = true;
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

                Device.BeginInvokeOnMainThread(async() =>
                {
                    var result = await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Broom Service", NotificationData, "Ok", "Cancel"); // since we are using async, we should specify the DisplayAlert as awaiting.
                    if (result) // if it's equal to Ok
                    {
                        if (NotificationData.StartsWith("You have a new message"))
                        {
                            Xamarin.Forms.Application.Current.MainPage = new NavigationPage(new HomeTabPage());
                            _ = Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new ChatListPage());
                        }
                        else
                        {
                            Xamarin.Forms.Application.Current.MainPage = new NavigationPage(new HomeTabPage());
                            MessagingCenter.Send("Notification_Tab", "HomeTabBar");
                        }
                    }
                    else // if it's equal to Cancel
                    {
                        return;
                    }
                });

                
            }




            ////Create Alert
            //var okCancelAlertController = UIAlertController.Create("Broom Service", "Choose from two buttons", UIAlertControllerStyle.Alert);

            ////Add Actions
            //okCancelAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, alert => Console.WriteLine("Okay was clicked")));
            //okCancelAlertController.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, alert => Console.WriteLine("Cancel was clicked")));

            ////Present Alert
            //PresentViewController(okCancelAlertController, true, null);
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


        public class MyNotificationCenterDelegate : UNUserNotificationCenterDelegate
        {
            public override void DidReceiveNotificationResponse(UNUserNotificationCenter center, UNNotificationResponse response, Action completionHandler)
            {
                //if (App.badgeCount > 0)
                //{
                //    App.badgeCount = App.badgeCount - 1;
                //    CrossBadge.Current.SetBadge(App.badgeCount);
                //}
                var aps = response.Notification.Request.Content.UserInfo.
                    ObjectForKey(new NSString("aps")) as NSDictionary;

                var type = response.Notification.Request.Content.UserInfo.
                    ObjectForKey(new NSString("gcm.notification.type")) as NSString;

                if (type == "5")
                {
                    //MainTabbedPage._isAskMsg = true;
                    //App.Current.MainPage = new NavigationPage(new MainTabbedPage());
                }
                else if (type == "6") //quiz notification
                {
                    //MainTabbedPage._isAskMsg = false;
                    //App.Current.MainPage = new NavigationPage(new MainTabbedPage());
                }
                else
                {
                    //App.Current.MainPage.Navigation.PushAsync(new AlertsPage());
                }

                completionHandler();
            }

            public override void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
            {
                completionHandler(UNNotificationPresentationOptions.Sound | UNNotificationPresentationOptions.Alert | UNNotificationPresentationOptions.Badge);

            }
        }
    }
}