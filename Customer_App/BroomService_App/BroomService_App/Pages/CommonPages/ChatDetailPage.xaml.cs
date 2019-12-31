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
	public partial class ChatDetailPage : ContentPage
	{
		public ChatDetailPage ()
		{
			InitializeComponent ();
            BindingContext = new ChatDetailViewModel(Navigation);
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<string>(this, "ScrollToEnd", (sender) =>
            {
                var v = chatDetailList.ItemsSource.Cast<object>().LastOrDefault();
                if (v != null)
                {
                    chatDetailList.ScrollTo(v, null, ScrollToPosition.End, true); 
                }
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<string>(this, "ScrollToEnd");
        }
    }
}