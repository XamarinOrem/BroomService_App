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
    public partial class ChangeLanguagePage : ContentPage
    {
        private bool IsAppAlreadyInstalled;
        public ChangeLanguagePage(bool isAppAlreadyInstalled)
        {
            InitializeComponent();
            IsAppAlreadyInstalled = isAppAlreadyInstalled;
        }
        public ChangeLanguagePage()
        {
            InitializeComponent();
            IsAppAlreadyInstalled = true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.BindingContext = new ChangeLanguageViewModel(Navigation, IsAppAlreadyInstalled);
        }
    }
}