using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using BroomService_App.CustomControls;
using BroomService_App.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]

namespace BroomService_App.Droid.CustomRenderers
{
    public class CustomPickerRenderer : PickerRenderer
    {
        CustomPicker element;

        public CustomPickerRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            element = (CustomPicker)this.Element;
            if (Control != null && this.Element != null && !string.IsNullOrEmpty(element.Icon))
            {
                Control.Background = AddPickerStyles(element.Icon);
                //Control.SetHintTextColor(Android.Graphics.Color.#533f95); 
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
            catch (System.Exception)
            {
            }
        }
        public LayerDrawable AddPickerStyles(string imagePath)
        {
            ShapeDrawable border = new ShapeDrawable();
            border.Paint.Color = Android.Graphics.Color.Transparent;
            border.SetPadding(0, 0, 0, 0);
            border.Paint.SetStyle(Paint.Style.Stroke);
            Drawable[] layers = {
                            border,
                            GetDrawable(imagePath)
                        };
            LayerDrawable layerDrawable = new LayerDrawable(layers);

            return layerDrawable;
        }
        private BitmapDrawable GetDrawable(string imagePath)
        {
            var drawable = Resources.GetDrawable(imagePath);
            var bitmap = ((BitmapDrawable)drawable).Bitmap;
            var result = new BitmapDrawable(Resources, Bitmap.CreateScaledBitmap(bitmap, 20, 20, true));
            result.Gravity = Android.Views.GravityFlags.Right;
            return result;
        }
    }
}