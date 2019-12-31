using Acr.UserDialogs;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Common;
using Android.OS;
using Android.Widget;
using BroomService_App.Droid.DependencyInterface;
using FFImageLoading.Forms.Platform;
using HockeyApp.Android;
using ImageCircle.Forms.Plugin.Droid;
using Microsoft.AppCenter.Distribute;
using Plugin.CurrentActivity;
using Plugin.FirebasePushNotification;
using Plugin.Permissions;
using Xamarin;
using Xamarin.Forms;
using XF.Material.Droid;

namespace BroomService_App.Droid
{
    [Activity(Label = "Broom Service", Icon = "@drawable/ic_app_icon", Theme = "@style/MainTheme", MainLauncher = false, 
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static string webContentList = "";
        public static bool isNotification = false;

        const string permissionRS = Manifest.Permission.ReadExternalStorage;
        const string permissionWS = Manifest.Permission.WriteExternalStorage;
        const string permissionCM = Manifest.Permission.Camera;
        const int RequestLocationId = 0;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            // fetch screen height and width
            var width = Resources.DisplayMetrics.WidthPixels;
            var height = Resources.DisplayMetrics.HeightPixels;
            var density = Resources.DisplayMetrics.Density;

            App.ScreenWidth = (width - 0.5f) / density;
            App.ScreenHeight = (height - 0.5f) / density;
            UserDialogs.Init(this);
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            GetGalleryPermissions();
            Forms.SetFlags("CollectionView_Experimental");
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            CachedImageRenderer.Init(true);
            FormsMaps.Init(this, savedInstanceState);
            Material.Init(this, savedInstanceState);
            ImageCircleRenderer.Init();
            //Distribute.SetEnabledForDebuggableBuild(true);

            
            //Foreground mode
            if (webContentList.ToString() != "")
            {
                isNotification = true;
                LoadApplication(new App());
                webContentList = "";
            }

            //Normal loading
            if (!isNotification)
            {
                LoadApplication(new App());
            }


            //LoadApplication(new App());
            CheckForGoogleServices();
            FirebasePushNotificationManager.ProcessIntent(this, Intent);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            MultiMediaPickerService.SharedInstance.OnActivityResult(requestCode, resultCode, data);
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            //if (intent.Extras != null)
            //{
            //    foreach (var key in intent.Extras.KeySet())
            //    {
            //        var value = intent.Extras.GetString(key);
            //        if (key == "action_notification_tag")
            //        {
            //            if (value?.Length > 0)
            //            {
            //                isNotification = true;
            //                LoadApplication(new App());
            //            }
            //        }
            //    }
            //}
            FirebasePushNotificationManager.ProcessIntent(this, Intent);
        }

        string HOCKEYAPP_APPID = "febab6eb36ed4e3bb55ba976df3d2df4";
        protected override void OnResume()
        {
            base.OnResume();
            CrashManager.Register(this, HOCKEYAPP_APPID);
        }


        public bool CheckForGoogleServices()
        {
            var resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                {
                    Toast.MakeText(Android.App.Application.Context, GoogleApiAvailability.Instance.GetErrorString(resultCode), ToastLength.Long);
                }
                else
                {
                    Toast.MakeText(Android.App.Application.Context, " This device does not support Google Play Services ", ToastLength.Long);
                }
                return false;
            }
            return true;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }



        public void GetGalleryPermissions()
        {
            if ((int)Build.VERSION.SdkInt >= 23 && this.CheckSelfPermission(permissionRS) != (int)Permission.Granted)
            {
                this.RequestPermissions(permissions, RequestLocationId);
            }
            if ((int)Build.VERSION.SdkInt >= 23 && this.CheckSelfPermission(permissionWS) != (int)Permission.Granted)
            {
                this.RequestPermissions(permissions, RequestLocationId);
            }
            if ((int)Build.VERSION.SdkInt >= 23 && this.CheckSelfPermission(permissionCM) != (int)Permission.Granted)
            {
                this.RequestPermissions(permissions, RequestLocationId);
            }
        }

        





        readonly string[] permissions =
        {
           Manifest.Permission.ReadExternalStorage,
           Manifest.Permission.WriteExternalStorage,
           Manifest.Permission.Camera
        };
    }
}