using BroomService_App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BroomService_App.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PropertyDetailPage : ContentPage
	{
		public PropertyDetailPage (Models.PropertyDataModel propertyTapCommand=null)
		{
			InitializeComponent ();
            this.BindingContext = new PropertyDetailViewModel(Navigation, propertyTapCommand);
		}
	}
}