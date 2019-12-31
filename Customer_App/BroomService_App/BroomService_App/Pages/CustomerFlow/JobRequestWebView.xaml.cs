using Acr.UserDialogs;
using BroomService_App.Helpers;
using BroomService_App.ViewModels.CustomerFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BroomService_App.Pages.CustomerFlow
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JobRequestWebView : ContentPage
    {
        public JobRequestWebView(MultipartFormDataContent multipartFormData, string termsConditionText)
        {
            InitializeComponent();
            this.BindingContext = new JobRequestWebViewModel(Navigation, multipartFormData);
            var htmltext = new HtmlWebViewSource
            {
                Html = termsConditionText
            };
            webView.Source = htmltext;

        }

        private void WebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            //UserDialogs.Instance.ShowLoading("");
        }

        private void WebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            //UserDialogs.Instance.HideLoading();
        }
    }
}