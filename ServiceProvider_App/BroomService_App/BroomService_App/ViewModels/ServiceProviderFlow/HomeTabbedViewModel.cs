using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BroomService_App.ViewModels.ServiceProviderFlow
{
    public class HomeTabbedViewModel : BaseViewModel
    {
        public HomeTabbedViewModel(INavigation navigation) : base(navigation)
        {
            MessagingCenter.Subscribe<string>(this, "NotificationRecieved", (sender) =>
            {
                BadgeCount = sender;
            });
        }
    }
}
