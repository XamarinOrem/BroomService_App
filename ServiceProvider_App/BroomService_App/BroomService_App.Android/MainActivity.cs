using Acr.UserDialogs;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using HockeyApp.Android;
using ImageCircle.Forms.Plugin.Droid;
using Microsoft.AppCenter.Distribute;
using Plugin.CurrentActivity;
using Plugin.FirebasePushNotification;
using Xamarin.Forms;
using XF.Material.Droid;

namespace BroomService_App.Droid
{
    [Activity(Label = "BS Crew", Icon = "@drawable/ic_app_icon", Theme = "@style/MainTheme", MainLauncher = false, 
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
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
            Material.Init(this, savedInstanceState);
            ImageCircleRenderer.Init();
            //Distribute.SetEnabledForDebuggableBuild(true);
            LoadApplication(new App());

            FirebasePushNotificationManager.ProcessIntent(this, Intent);
        }

        //protected override void OnNewIntent(Intent intent)
        //{
        //    base.OnNewIntent(intent);
        //    FirebasePushNotificationManager.ProcessIntent(this, Intent);
        //}

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

        string HOCKEYAPP_APPID = "8126215704274ffd80b14d60bd437327";

        protected override void OnResume()
        {
            base.OnResume();
            CrashManager.Register(this, HOCKEYAPP_APPID);
        }



        readonly string[] permissions =
        {
           Manifest.Permission.ReadExternalStorage,
           Manifest.Permission.WriteExternalStorage,
           Manifest.Permission.Camera
        };
    }
}