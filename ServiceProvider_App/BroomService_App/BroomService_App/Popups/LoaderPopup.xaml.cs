using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;

namespace BroomService_App.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoaderPopup : PopupPage
    {
        public LoaderPopup()
        {
            InitializeComponent();
        }

        public static async void CloseAllPopup()
        {
            await App.Current.MainPage.Navigation.PopPopupAsync(true);
        }
    }
}