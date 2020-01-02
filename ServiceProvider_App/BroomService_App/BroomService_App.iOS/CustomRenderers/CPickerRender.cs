using System;
using System.ComponentModel;
using BroomService_App.iOS.CustomRenderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

//[assembly: ExportRenderer(typeof(Picker), typeof(CPickerRender))]
namespace BroomService_App.iOS.CustomRenderers
{
    public class CPickerRender : PickerRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            try
            {
                if (Control != null)
                {
                    Control.BorderStyle = UITextBorderStyle.None;
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}