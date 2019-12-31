using System;
using BroomService_App.iOS.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Editor), typeof(FontEditorRenderer_iOS))]
namespace BroomService_App.iOS.CustomRenderers
{
    public class FontEditorRenderer_iOS : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                if (Control != null)
                {
                    // do whatever you want to the UITextField here!
                    Control.Layer.BorderColor = Color.Transparent.ToCGColor();
                    Control.Layer.BorderWidth = 0;
                }
            }
        }
    }
}