using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace BroomService_App.Pages.CustomerFlow
{
    public partial class PaymentWebPage : ContentPage
    {
        public PaymentWebPage(string paymentweburl)
        {
            InitializeComponent();
            paymentweburl = paymentweburl.Replace(",", ".");
            paymentWebView.Source = paymentweburl;
            paymentWebView.PropertyChanged += PaymentWebView_PropertyChanged;
        }

        private void PaymentWebView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //Get Success url
            var webview = ((WebView)sender).Source;
            var url = webview.GetValue(UrlWebViewSource.UrlProperty).ToString();
            var paymentsuccess = url.Contains("payme_status=success");
            if (paymentsuccess)
            {
                UserDialogs.Instance.ShowLoading("");
                App.Current.MainPage = new NavigationPage(new HomeTabPage());
                UserDialogs.Instance.HideLoading();
                //paymentWebView.PropertyChanged -= PaymentWebView_PropertyChanged;
            }
            //var path = url.Substring(url.LastIndexOf('/') + 1);
            //if (path == "devicemanagement" || path == "add-device")
            //{
            //    //UserDialogs.Instance.HideLoading();
            //    //IsBackEnabled = true;
            //    //App.Current.MainPage = new NavigationPage(new AddDeviceOption());
            //    //UserDialogs.Instance.HideLoading();
            //}
        }

        private void PaymentWebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            UserDialogs.Instance.ShowLoading("");
        }

        private void PaymentWebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            UserDialogs.Instance.HideLoading();
        }
    }
}