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
	public partial class NotificationPage : ContentPage
	{
		public NotificationPage ()
		{
			InitializeComponent ();
		}

        private void NotificationList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            notificationList.SelectedItem = null;
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
    }
}