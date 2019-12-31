using BroomService_App.CustomControls.Effects;
using BroomService_App.ViewModels.ServiceProviderFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BroomService_App.Pages.WorkerFlow
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeTabbedPage : TabbedPage
    {
        public HomeTabbedPage()
        {
            InitializeComponent();
            if (Device.RuntimePlatform == Device.Android)
            {
                hometabpageworker.Effects.Add(new NoShiftEffect()); 
            }
            MessagingCenter.Subscribe<string>(this, "HomeTabBar", (sender) =>
            {
                if (sender.Equals("Home_Tab"))
                {
                    CurrentPage = Children[0];
                }
                else if (sender.Equals("Chat_Tab"))
                {
                    CurrentPage = Children[1];
                }
                else if (sender.Equals("Notification_Tab"))
                {
                    CurrentPage = Children[2];
                }
                else if (sender.Equals("Profile_Tab"))
                {
                    CurrentPage = Children[3];
                }
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.BindingContext = new HomeTabbedViewModel(Navigation);
            if (App.IsNotificationRecieved)
            {
                MessagingCenter.Send("5", "NotificationRecieved");
            }
            else
            {
                MessagingCenter.Send(string.Empty, "NotificationRecieved");
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<string>(this, "NotificationRecieved");
        }
    }
}