using System;
using BroomService_App.iOS.CustomRenderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms.PlatformConfiguration;

[assembly: ExportRenderer(typeof(Entry), typeof(FontEntryRenderer_iOS))]
namespace BroomService_App.iOS.CustomRenderers
{
    public class FontEntryRenderer_iOS : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                if (Control != null)
                {
                    // do whatever you want to the UITextField here!
                    Control.BorderStyle = UITextBorderStyle.None;
                }
            }
        }
    }
}
