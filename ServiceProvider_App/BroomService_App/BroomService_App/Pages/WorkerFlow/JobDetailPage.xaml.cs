using BroomService_App.Helpers;
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
    public partial class JobDetailPage : ContentPage
    {
        public JobDetailPage(Models.MyBookingModel bookingListTap = null)
        {
            InitializeComponent();
            this.BindingContext = new JobDetailViewModel(Navigation, bookingListTap);
            startTimerBtn.CommandParameter = ButtonParameters.startTimer;
            endTimerBtn.CommandParameter = ButtonParameters.endTimer;
            completeBtn.CommandParameter = ButtonParameters.complete;
        }

        private void Checklistsworker_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            checklistsworker.SelectedItem = null;
        }

        private void RefImagesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            refImagesList.SelectedItem = null;
        }
    }
}