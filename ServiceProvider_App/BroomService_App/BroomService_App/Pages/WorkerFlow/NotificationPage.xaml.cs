using Acr.UserDialogs;
using BroomService_App.Helpers;
using BroomService_App.Models;
using BroomService_App.Resources;
using BroomService_App.Services.ApiService;
using BroomService_App.ViewModels.ServiceProviderFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;

namespace BroomService_App.Pages.WorkerFlow
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotificationPage : ContentPage
    {

        public NotificationPage()
        {
            InitializeComponent();
        }

        private void NotificationList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            spnotificationList.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.IsNotificationRecieved = false;
            if (App.IsNotificationRecieved)
            {
                MessagingCenter.Send("5", "NotificationRecieved");
            }
            else
            {
                MessagingCenter.Send(string.Empty, "NotificationRecieved");
            }
            this.BindingContext = new NotificationViewModel(Navigation);

            MessagingCenter.Subscribe<string>(this, "RefreshNotificationView", (sender) =>
            {
                this.BindingContext = new NotificationViewModel(Navigation);
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<string>(this, "RefreshNotificationView");
        }
        //private void NotificationList_SelectionChanged(object sender, SelectedItemChangedEventArgs e)
        //{
        //    notificationList.SelectedItem = null;
        //}
    }
}