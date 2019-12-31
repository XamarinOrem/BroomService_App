using BroomService_App.Models;
using BroomService_App.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BroomService_App.Pages.CommonPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditProfilePage : ContentPage
	{
		public EditProfilePage ()
		{
			InitializeComponent ();
            this.BindingContext = new EditProfileViewModel(Navigation);
		}

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}