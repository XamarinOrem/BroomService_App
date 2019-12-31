using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BroomService_App.ViewModels.CustomerFlow
{
    public class HomeTabbedViewModel : BaseViewModel
    {
        public HomeTabbedViewModel(INavigation navigation) : base(navigation)
        {
            MessagingCenter.Subscribe<string>(this, "NotificationRecieved", (sender) =>
            {
                try
                {
                    BadgeCount = sender;
                }
                catch (Exception)
                {}
            });
        }
    }
}
