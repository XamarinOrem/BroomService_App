using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using Xamarin.Forms.Xaml;

namespace BroomService_App.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NoInternetPopup : PopupPage
    {
		public NoInternetPopup ()
		{
			InitializeComponent ();
		}

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PopPopupAsync();
        }
    }
}