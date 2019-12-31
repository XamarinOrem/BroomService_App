using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BroomService_App.Pages.CommonPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImageZoomPage : ContentPage
    {
        private bool IsZoom;
        public ImageZoomPage(ImageSource referenceImages)
        {
            InitializeComponent();
            IsZoom = false;
            ReferenceImage.Source = referenceImages;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void ImageTapped(object sender, EventArgs e)
        {
            if (IsZoom)
            {
                IsZoom = false;
                ReferenceImage.HeightRequest = ReferenceImage.Height / 2;
                ReferenceImage.WidthRequest = ReferenceImage.Width / 2;
                imageBG.HeightRequest = imageBG.Height - ReferenceImage.Height;
                zoomimagescroll.Orientation = ScrollOrientation.Vertical;
            }
            else
            {
                IsZoom = true;
                ReferenceImage.HeightRequest = ReferenceImage.Height * 2;
                ReferenceImage.WidthRequest = ReferenceImage.Width * 2;
                imageBG.HeightRequest = imageBG.Height + ReferenceImage.Height;
                zoomimagescroll.Orientation = ScrollOrientation.Both;
            }
        }
    }
}