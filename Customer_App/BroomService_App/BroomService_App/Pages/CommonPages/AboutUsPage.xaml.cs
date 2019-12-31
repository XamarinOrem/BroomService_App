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
	public partial class AboutUsPage : ContentPage
	{
		public AboutUsPage()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
            MessagingCenter.Subscribe<string>(this, "AboutUsData", (sender) =>
            {
                var htmltext = new HtmlWebViewSource
                {
                    Html = sender
                };
                aboutwebView.Source = htmltext;

                MessagingCenter.Unsubscribe<string>(this, "AboutUsData");
            });
            BindingContext = new AboutUsViewModel(Navigation);
        }

        private void WebView_Navigating(object sender, WebNavigatingEventArgs e)
        {

        }

        private void WebView_Navigated(object sender, WebNavigatedEventArgs e)
        {

        }
    }
}