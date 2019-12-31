using System;
using Android.Content;
using Android.Graphics;
using BroomService_App.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Button), typeof(FontButtonRenderer_Droid))]
namespace BroomService_App.Droid.CustomRenderers
{
    public class FontButtonRenderer_Droid : ButtonRenderer
    {
        public FontButtonRenderer_Droid(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetAllCaps(false);
            }
            try
            {
                if (!string.IsNullOrEmpty(e.NewElement?.FontFamily))
                {
                    if (e.NewElement?.FontFamily == "Raleway-ExtraBold")
                    {
                        var font = Typeface.CreateFromAsset(Android.App.Application.Context.ApplicationContext.Assets,
                            e.NewElement.FontFamily + ".ttf");
                        Control.Typeface = font;
                    }
                    else
                    {
                        var font = Typeface.CreateFromAsset(Android.App.Application.Context.ApplicationContext.Assets, e.NewElement.FontFamily + ".otf");
                        Control.Typeface = font;
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}