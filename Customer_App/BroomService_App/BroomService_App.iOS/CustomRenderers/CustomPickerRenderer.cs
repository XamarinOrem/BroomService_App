using System;
using System.ComponentModel;
using BroomService_App.CustomControls;
using BroomService_App.iOS.CustomRenderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace BroomService_App.iOS.CustomRenderers
{
    public class CustomPickerRenderer : PickerRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (Control != null)
            {
                Control.BorderStyle = UITextBorderStyle.None;

                var downarrow = UIImage.FromBundle("ic_drop_arrow.png");
                try
                {
                    Control.RightViewMode = UITextFieldViewMode.Always;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Crash on Picker render::-->" + ex.Message);
                }
                Control.RightView = new UIImageView(downarrow);
            }
        }
    }
}
