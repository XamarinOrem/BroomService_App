using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BroomService_App.CustomControls
{
    public class CustomPicker : Picker
    {
        public static readonly BindableProperty ImageProperty = BindableProperty.Create(nameof(Icon), typeof(string), typeof(CustomPicker), string.Empty);
        public string Icon
        {
            get
            {
                return (string)GetValue(ImageProperty);
            }
            set
            {
                SetValue(ImageProperty, value);
            }
        }
    }
}
