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
	public partial class SearchHeaderWithRightIcon : ContentView
	{
		public SearchHeaderWithRightIcon ()
		{
			InitializeComponent ();
		}

        public static readonly BindableProperty RightIconImageProperty = BindableProperty.Create("RightIconImage", typeof(string), typeof(Image), null);
        public string RightIconImage
        {
            get { return (string)GetValue(RightIconImageProperty); }
            set { SetValue(RightIconImageProperty, value); }
        }
    }
}