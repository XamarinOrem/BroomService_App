using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace BroomService_App.CustomControls
{
    public class RatingView : ContentView
    {

        String SelectedImage;
        String UnselectedImage;
        Grid gridview = new Grid();
        StackLayout stackview = new StackLayout();
        Image ratingImage1, ratingImage2, ratingImage3, ratingImage4, ratingImage5 = new Image();
        public RatingView(string unselectedImage, string selectedImage)
        {
            SelectedImage = selectedImage;
            UnselectedImage = unselectedImage;
            ratingImage1.Source = ratingImage2.Source = ratingImage3.Source = ratingImage4.Source = ratingImage5.Source = UnselectedImage;
            ratingImage1.HeightRequest = ratingImage2.HeightRequest = ratingImage3.HeightRequest = ratingImage4.HeightRequest = ratingImage5.HeightRequest = ratingImage1.WidthRequest = ratingImage2.WidthRequest = ratingImage3.WidthRequest = ratingImage4.WidthRequest = ratingImage5.WidthRequest = 30;

            ratingImage1.ClassId = "1";
            ratingImage2.ClassId = "2";
            ratingImage3.ClassId = "3";
            ratingImage4.ClassId = "4";
            ratingImage5.ClassId = "5";

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;
            ratingImage1.GestureRecognizers.Add(tapGestureRecognizer);
            ratingImage2.GestureRecognizers.Add(tapGestureRecognizer);
            ratingImage3.GestureRecognizers.Add(tapGestureRecognizer);
            ratingImage4.GestureRecognizers.Add(tapGestureRecognizer);
            ratingImage5.GestureRecognizers.Add(tapGestureRecognizer);

            stackview.Children.Add(ratingImage1);
            stackview.Children.Add(ratingImage2);
            stackview.Children.Add(ratingImage3);
            stackview.Children.Add(ratingImage4);
            stackview.Children.Add(ratingImage5);

            gridview.Children.AddHorizontal(stackview);
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var parm = ((Image)sender).ClassId;
            var count = int.Parse(parm);

            HandleRatingChange(count);
        }


        public static BindableProperty RatingValueProperty = BindableProperty.Create(
                declaringType: typeof(RatingView),
                propertyName: "RatingValue",
                returnType: typeof(double),
                defaultValue: 0,
                defaultBindingMode: BindingMode.TwoWay,
                propertyChanged: HandleRatingViewPropertyChanged);

        public double RatingValue
        {
            get { return (double)GetValue(RatingValueProperty); }
            set { SetValue(RatingValueProperty, value); }
        }

        private static void HandleRatingViewPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ratingView = (RatingView)bindable;
            //ratingView.HandleRatingChange();
        }

        private void HandleRatingChange(int count)
        {
            switch (count)
            {
                case 1:
                    ratingImage1.Source = SelectedImage;
                    ratingImage2.Source = ratingImage3.Source = ratingImage4.Source = ratingImage5.Source = UnselectedImage;
                    break;
                case 2:
                    ratingImage1.Source = ratingImage2.Source = SelectedImage;
                    ratingImage3.Source = ratingImage4.Source = ratingImage5.Source = UnselectedImage;
                    break;
                case 3:
                    ratingImage1.Source = ratingImage2.Source = ratingImage3.Source = SelectedImage;
                    ratingImage4.Source = ratingImage5.Source = UnselectedImage;
                    break;
                case 4:
                    ratingImage1.Source = ratingImage2.Source = ratingImage3.Source = ratingImage4.Source = SelectedImage;
                    ratingImage5.Source = UnselectedImage;
                    break;
                case 5:
                    ratingImage1.Source = ratingImage2.Source = ratingImage3.Source = ratingImage4.Source = ratingImage5.Source = SelectedImage;
                    break;
            }
        }
    }
}