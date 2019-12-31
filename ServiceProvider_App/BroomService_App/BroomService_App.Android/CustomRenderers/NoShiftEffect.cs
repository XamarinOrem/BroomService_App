using Android.Support.Design.BottomNavigation;
using Android.Support.Design.Widget;
using Android.Views;
using BroomService_App.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("TabPageEffect")]
[assembly: ExportEffect(typeof(NoShiftEffect), "NoShiftEffect")]
namespace BroomService_App.Droid.CustomRenderers
{
    public class NoShiftEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            if (!(Container.GetChildAt(0) is ViewGroup layout))
                return;

            if (!(layout.GetChildAt(1) is BottomNavigationView bottomNavigationView))
                return;

            // This is what we set to adjust if the shifting happens
            bottomNavigationView.LabelVisibilityMode = LabelVisibilityMode.LabelVisibilityLabeled;
        }

        protected override void OnDetached()
        {
        }
    }
}