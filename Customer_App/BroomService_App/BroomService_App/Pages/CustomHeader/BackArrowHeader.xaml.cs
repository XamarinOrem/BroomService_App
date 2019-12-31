using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BroomService_App.Pages.CommonHeader
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BackArrowHeader : ContentView
	{
		public BackArrowHeader ()
		{
			InitializeComponent ();
		}
        public static readonly BindableProperty TitleTextProperty = BindableProperty.Create("TitleText", typeof(string), typeof(Label), null);
        public string TitleText
        {
            get { return (string)GetValue(TitleTextProperty); }
            set { SetValue(TitleTextProperty, value); }
        }

        public static readonly BindableProperty RightIconImageProperty = BindableProperty.Create("RightIconImage", typeof(string), typeof(Image), null);
        public string RightIconImage
        {
            get { return (string)GetValue(RightIconImageProperty); }
            set { SetValue(RightIconImageProperty, value); }
        }
    }
}