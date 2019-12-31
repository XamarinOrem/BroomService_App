using Acr.UserDialogs;
using BroomService_App.Helpers;
using BroomService_App.Models;
using BroomService_App.Resources;
using BroomService_App.Services.ApiService;
using BroomService_App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;

namespace BroomService_App.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChatListPage : ContentPage
	{
        public ChatListPage ()
		{
			InitializeComponent ();
        }

        private void ChatList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            chatList.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (App.IsNotificationRecieved)
            {
                MessagingCenter.Send("5", "NotificationRecieved");
            }
            else
            {
                MessagingCenter.Send(string.Empty, "NotificationRecieved");
            }
            BindingContext = new ChatListViewModel(navigation: Navigation);
        }
    }
}