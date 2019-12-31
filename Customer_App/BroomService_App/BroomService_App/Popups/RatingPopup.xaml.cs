using BroomService_App.Services.ApiService;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BroomService_App.Models;
using BroomService_App.Repository;
using BroomService_App.Resources;
using XF.Material.Forms.UI.Dialogs;
using Xamarin.Essentials;
using BroomService_App.Helpers;
using BroomService_App.ViewModels;


namespace BroomService_App.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RatingPopup : PopupPage
    {
        private readonly WebApiRestClient webApiRestClient;

        private MyBookingModel MyBookingModel;
        //LoggedInUser user = App.Database.GetLoggedInUser();

        private int CurrentUserId;
        private int ToUserID;
        private double SpRating = 0;
        private double JobRating = 0;

        public RatingPopup(int customerid, MyBookingModel myBookingModel)
        {
            InitializeComponent();
            MyBookingModel = myBookingModel;
            CurrentUserId = customerid;
            ToUserID = MyBookingModel.ServiceProviderId.Value;

            if(MyBookingModel.UserRating != null)
            {
                customerratingview.IsVisible = false;
                SpRating = MyBookingModel.UserRating.Value;
                spComment.Text = MyBookingModel.UserReview;
            }
            else
            {
                customerratingview.IsVisible = true;
            }

            webApiRestClient = new WebApiRestClient();

        }

        void OnCloseButtonTapped(object sender, EventArgs e)
        {
            Navigation.PopPopupAsync();
        }

        private async void Submit_Button_Clicked(object sender, EventArgs e)
        {
            if (customerratingview.IsVisible)
            {
                SpRating = userratingview.Value;
            }
            JobRating = jobratingview.Value;
            if (SpRating != 0 && JobRating != 0)
            {
                RatingModel rateUserModel = new RatingModel();
                rateUserModel.JobReview = spJobComment.Text;
                rateUserModel.UserReview = spComment.Text;
                rateUserModel.JobRating = Convert.ToInt32(JobRating);
                rateUserModel.UserRating = Convert.ToInt32(SpRating);
                rateUserModel.ToUserId = ToUserID;
                rateUserModel.CustomerId = CurrentUserId;
                rateUserModel.JobRequestId = MyBookingModel.Id;
                await RateUser(rateUserModel);
            }
            else
            {
                await MaterialDialog.Instance.SnackbarAsync(message: AppResource.RatingError,
                                        msDuration: 1000);
            }
        }


        //public string CheckValidations()
        //{
        //    string msg = string.Empty;
        //    if (Rating == 0)
        //    {
        //        msg += AppResources.add_rating_message;
        //    }
        //    return msg;
        //}

        public async Task RateUser(RatingModel rateUserModel)
        {
            try
            {
                if ((Connectivity.ConnectionProfiles.Contains(ConnectionProfile.WiFi) && Connectivity.NetworkAccess.Equals(NetworkAccess.Internet)) || (Connectivity.ConnectionProfiles.Contains(ConnectionProfile.Cellular) && Connectivity.NetworkAccess.Equals(NetworkAccess.Internet)))
                {
                    RatingModelResponse response;
                    try
                    {
                        response = await webApiRestClient.PostAsync<RatingModel, RatingModelResponse>(ApiHelpers.SubmitUserReview, rateUserModel, true);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("RatingApi_Exception:- " + ex.Message);
                        response = null;
                    }
                    if (response != null)
                    {
                        if (response.status)
                        {
                            await Navigation.PopPopupAsync();
                            await MaterialDialog.Instance.SnackbarAsync(message: response.message,
                                       msDuration: 1000);
                            MessagingCenter.Send("", "RatingPopupClose");
                            //OnCloseButtonTapped(null, null);
                        }
                        else
                        {
                            await MaterialDialog.Instance.SnackbarAsync(message: response.message,
                                        msDuration: 1000);
                        }
                    }
                    else
                    {
                        await MaterialDialog.Instance.SnackbarAsync(message: AppResource.ServerError,
                                        msDuration: 1000);
                    }
                }
                else
                {
                    await MaterialDialog.Instance.SnackbarAsync(message: AppResource.NoInternetError,
                                            msDuration: 1000);
                }
            }
            catch (Exception ex)
            {
                LoaderPopup.CloseAllPopup();
                //await Application.Current.MainPage.DisplayAlert("", ex.ToString(), "OK");
            }
        }

        private void Ratingview_SizeChanged(object sender, EventArgs e)
        {

        }

        private void Ratingview_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

        }
    }
}