using Acr.UserDialogs;
using BroomService_App.Helpers;
using BroomService_App.Models;
using BroomService_App.Pages.ServiceProviderFlow;
using BroomService_App.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace BroomService_App.ViewModels.ServiceProviderFlow
{
    public class NotificationViewModel : BaseViewModel
    {
        MyBookingModel jobdetaildata;
        public static int UserId;
        #region Constructor
        public NotificationViewModel(INavigation navigation) : base(navigation)
        {
            UserId = CurrentUserId;

            getNotificationList();

            

        }
        #endregion

        #region getNotificationList
        private async void getNotificationList()
        {
            try
            {
                if (Connectivity.NetworkAccess.Equals(NetworkAccess.Internet) || Connectivity.ConnectionProfiles.Contains(ConnectionProfile.WiFi))
                {
                    UserDialogs.Instance.ShowLoading("");
                    GetNotificationsModel response;
                    try
                    {
                        response = await webApiRestClient.GetAsync<GetNotificationsModel>(string.Format(ApiHelpers.GetNotifications, CurrentUserId), true);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("GetNotificationsApi_Exception:-" + ex.Message);
                        response = null;
                    }
                    if (response != null)
                    {
                        if (response.status)
                        {
                            try
                            {
                                //response.data.Reverse();
                                var _items = new ObservableCollection<GetNotifications>();
                                foreach (var notificationdata in response.data)
                                {
                                    // notificationdata.UserNotificationTime = notificationdata.CreatedDate.ToString("hh:mm tt");
                                    //notificationdata.UserPic = "ic_user_notification.png";
                                    // notificationdata.IsButtonVisible = notificationdata.NotificationStatus == Convert.ToInt32(NotificationStatus.Pending) ? true : false;
                                    _items.Add(new GetNotifications()
                                    {
                                        CreatedDate = notificationdata.CreatedDate,
                                        Text = notificationdata.NotificationStatus.Value == Convert.ToInt32(NotificationStatus.SentQuotation) ? notificationdata.Text + notificationdata.QuotePrice : notificationdata.Text,
                                        Id = notificationdata.Id,
                                        JobRequestId = notificationdata.JobRequestId,
                                        FromUserId = notificationdata.FromUserId,
                                        FromUserName = notificationdata.NotificationStatus.Value == Convert.ToInt32(NotificationStatus.AcceptedQuotation) || notificationdata.NotificationStatus.Value == Convert.ToInt32(NotificationStatus.RejectedQuotation) || notificationdata.NotificationStatus.Value == Convert.ToInt32(NotificationStatus.Assigned) || notificationdata.NotificationStatus.Value == Convert.ToInt32(NotificationStatus.Pending) ? FirstCharToUpper(notificationdata.FromUserName) : String.Empty,
                                        //FromUserName = notificationdata.Text.StartsWith(" ") ? FirstCharToUpper(notificationdata.FromUserName) : String.Empty,
                                        IsButtonVisible = notificationdata.NotificationStatus.Value == Convert.ToInt32(NotificationStatus.Pending) ? true : false,
                                        UserNotificationTime = RelativeDate(notificationdata.CreatedDate.Value),
                                        UserPic = notificationdata.NotificationStatus.Value == Convert.ToInt32(NotificationStatus.AcceptedQuotation) || notificationdata.NotificationStatus.Value == Convert.ToInt32(NotificationStatus.RejectedQuotation) || notificationdata.NotificationStatus.Value == Convert.ToInt32(NotificationStatus.Assigned) || notificationdata.NotificationStatus.Value == Convert.ToInt32(NotificationStatus.Pending) ? IsImagesValid(notificationdata.FromUserImage, ApiHelpers.ApiImageBaseUrl) : IsImagesValid(notificationdata.ToUserImage, ApiHelpers.ApiImageBaseUrl),
                                        //UserPic = notificationdata.Text.StartsWith(" ") ? IsImagesValid(notificationdata.FromUserImage, ApiHelpers.ApiImageBaseUrl) : IsImagesValid(notificationdata.ToUserImage, ApiHelpers.ApiImageBaseUrl),
                                        QuotePrice = notificationdata.QuotePrice,
                                        NotificationStatus = notificationdata.NotificationStatus,
                                        ToUserId = notificationdata.ToUserId,
                                        ToUserName = FirstCharToUpper(notificationdata.ToUserName)
                                    });
                                }
                                NotificationList = new ObservableCollection<GetNotifications>(_items.OrderByDescending(a => a.CreatedDate).ToList());
                            }
                            catch (Exception ex)
                            {
                                await MaterialDialog.Instance.SnackbarAsync(message: ex.Message,
                                       msDuration: MaterialSnackbar.DurationShort);
                            }
                        }
                        else
                        {
                            await MaterialDialog.Instance.SnackbarAsync(message: response.message,
                                        msDuration: MaterialSnackbar.DurationShort);
                        }
                    }
                    else
                    {
                        await MaterialDialog.Instance.SnackbarAsync(message: AppResource.ServerError,
                                        msDuration: MaterialSnackbar.DurationShort);
                    }
                    UserDialogs.Instance.HideLoading();
                }
                else
                {
                    await MaterialDialog.Instance.SnackbarAsync(message: AppResource.NoInternetError,
                                            msDuration: MaterialSnackbar.DurationShort);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GettingNotificationList_Exception:-->" + ex.Message);
                UserDialogs.Instance.HideLoading();
            }
        } 
        #endregion

        #region NotificationList property
        private ObservableCollection<GetNotifications> _notificationList = new ObservableCollection<GetNotifications>();

        public ObservableCollection<GetNotifications> NotificationList
        {
            get { return _notificationList; }
            set
            {
                SetProperty(ref _notificationList, value);
            }
        }
        #endregion

        #region NotificationSelected property
        private NotificationModel _notificationSelected;

        public NotificationModel NotificationSelected
        {
            get { return _notificationSelected; }
            set
            {
                SetProperty(ref _notificationSelected, value);
            }
        }
        #endregion

        #region RejectCommand
        public Command RejectCommand
        {
            get
            {
                return new Command(async (e) =>
                {
                    try
                    {
                        if (Connectivity.NetworkAccess.Equals(NetworkAccess.Internet) || Connectivity.ConnectionProfiles.Contains(ConnectionProfile.WiFi))
                        {
                            UserDialogs.Instance.ShowLoading("");
                            var requestModel = new ApplyJobRequestModel()
                            {
                                IsAccept = "false",
                                JobRequestId = ((GetNotifications)e).JobRequestId.Value,
                                UserId = CurrentUserId
                            };


                            ApplyJobRequestResponseModel response;
                            try
                            {
                                response = await webApiRestClient.PostAsync<ApplyJobRequestModel, ApplyJobRequestResponseModel>(ApiHelpers.AcceptRejectRequest, requestModel, true);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                response = null;
                            }
                            if (response != null)
                            {
                                if (response.status)
                                {
                                    //if (response.userData.UserType == Convert.ToInt32(UserTypeEnum.Customer))
                                    //{
                                    //    App.Current.MainPage = new NavigationPage(new HomeTabPage());
                                    //}
                                    //else
                                    //{
                                    //    App.Current.MainPage = new NavigationPage(new Pages.ServiceProviderFlow.HomeTabbedPage());
                                    //}
                                    await MaterialDialog.Instance.SnackbarAsync(message: response.message,
                                                msDuration: MaterialSnackbar.DurationShort);

                                    try
                                    {
                                        MessagingCenter.Send("RefreshNotificationView", "RefreshNotificationView");
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("RightIconCommand_Exception:- " + ex.Message);
                                    }
                                }
                                else
                                {
                                    await MaterialDialog.Instance.SnackbarAsync(message: response.message,
                                                msDuration: MaterialSnackbar.DurationShort);
                                }
                            }
                            else
                            {
                                await MaterialDialog.Instance.SnackbarAsync(message: AppResource.ServerError,
                                                msDuration: MaterialSnackbar.DurationShort);
                            }
                            UserDialogs.Instance.HideLoading();
                        }
                        else
                        {
                            await MaterialDialog.Instance.SnackbarAsync(message: AppResource.NoInternetError,
                                                    msDuration: MaterialSnackbar.DurationShort);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("RejectCommand_Exception:- " + ex.Message);
                        UserDialogs.Instance.HideLoading();
                    }
                    finally
                    {
                        //await _navigation.PopAllPopupAsync(true);
                    }
                });
            }
        }
        #endregion

        #region AcceptCommand
        public Command AcceptCommand
        {
            get
            {
                return new Command(async(e) =>
                {
                    try
                    {
                        if (Connectivity.NetworkAccess.Equals(NetworkAccess.Internet) || Connectivity.ConnectionProfiles.Contains(ConnectionProfile.WiFi))
                        {
                            UserDialogs.Instance.ShowLoading("");
                            var requestModel = new ApplyJobRequestModel()
                            {
                                IsAccept = "true",
                                JobRequestId = ((GetNotifications)e).JobRequestId.Value,
                                UserId = CurrentUserId
                            };


                            ApplyJobRequestResponseModel response;
                            try
                            {
                                response = await webApiRestClient.PostAsync<ApplyJobRequestModel, ApplyJobRequestResponseModel>(ApiHelpers.AcceptRejectRequest, requestModel,true);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                response = null;
                            }
                            if (response != null)
                            {
                                if (response.status)
                                {
                                    //if (response.userData.UserType == Convert.ToInt32(UserTypeEnum.Customer))
                                    //{
                                    //    App.Current.MainPage = new NavigationPage(new HomeTabPage());
                                    //}
                                    //else
                                    //{
                                    //    App.Current.MainPage = new NavigationPage(new Pages.ServiceProviderFlow.HomeTabbedPage());
                                    //}
                                    await MaterialDialog.Instance.SnackbarAsync(message: response.message,
                                                msDuration: MaterialSnackbar.DurationShort);
                                    try
                                    {
                                        MessagingCenter.Send("RefreshNotificationView", "RefreshNotificationView");
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("RightIconCommand_Exception:- " + ex.Message);
                                    }
                                }
                                else
                                {
                                    await MaterialDialog.Instance.SnackbarAsync(message: response.message,
                                                msDuration: MaterialSnackbar.DurationShort);
                                }
                            }
                            else
                            {
                                await MaterialDialog.Instance.SnackbarAsync(message: AppResource.ServerError,
                                                msDuration: MaterialSnackbar.DurationShort);
                            }
                            UserDialogs.Instance.HideLoading();
                        }
                        else
                        {
                            await MaterialDialog.Instance.SnackbarAsync(message: AppResource.NoInternetError,
                                                    msDuration: MaterialSnackbar.DurationShort);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("AcceptCommand_Exception:- " + ex.Message);
                        UserDialogs.Instance.HideLoading();
                    }
                    finally
                    {
                        //await _navigation.PopAllPopupAsync(true);
                    }
                });
            }
        }
        #endregion

        #region DetailCommand
        public Command DetailCommand
        {
            get
            {
                return new Command(async(e) =>
                {
                    try
                    {
                        if (Connectivity.NetworkAccess.Equals(NetworkAccess.Internet) || Connectivity.ConnectionProfiles.Contains(ConnectionProfile.WiFi))
                        {
                            UserDialogs.Instance.ShowLoading("");

                            GetJobDetailByRequestIdModel response;
                            try
                            {
                                response = await webApiRestClient.GetAsync<GetJobDetailByRequestIdModel>(String.Format(ApiHelpers.GetJobRequestDetail, ((GetNotifications)e).JobRequestId.Value,CurrentUserId), true);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                response = null;
                            }
                            if (response != null)
                            {
                                if (response.status)
                                {
                                    try
                                    {
                                        jobdetaildata = response.data;
                                        jobdetaildata.CategoryName = jobdetaildata.Category.Name;
                                        jobdetaildata.Category.Picture = IsImagesValid(jobdetaildata.Category.Picture, apiImageBaseUrl: ApiHelpers.CategoryImageBaseUrl);
                                        jobdetaildata.Category.Icon = IsImagesValid(jobdetaildata.Category.Icon, apiImageBaseUrl: ApiHelpers.CategoryImageBaseUrl);
                                        jobdetaildata.SubCategory.Picture = IsImagesValid(jobdetaildata.SubCategory.Picture, apiImageBaseUrl: ApiHelpers.SubCategoryImageBaseUrl);
                                        jobdetaildata.SubCategory.Icon = IsImagesValid(jobdetaildata.SubCategory.Icon, apiImageBaseUrl: ApiHelpers.SubCategoryImageBaseUrl);
                                        jobdetaildata.CustomerImage = IsImagesValid(jobdetaildata.CustomerImage, ApiHelpers.ApiImageBaseUrl);
                                        jobdetaildata.CustomerName = !string.IsNullOrEmpty(jobdetaildata.CustomerName) && !string.IsNullOrWhiteSpace(jobdetaildata.CustomerName) ? FirstCharToUpper(jobdetaildata.CustomerName) : "Nate Parker";
                                        jobdetaildata.ServiceStartDate = jobdetaildata.JobStartDatetime.Value.ToString("ddd, MMMM dd, yyyy");
                                        jobdetaildata.ServiceStartTime = jobdetaildata.JobStartDatetime.Value.ToString("hh:mm tt");
                                        jobdetaildata.ServiceStartDateTime = jobdetaildata.JobStartDatetime.Value.ToString("dd/MM/yyyy") + " at " + jobdetaildata.JobStartDatetime.Value.ToString("hh:mm tt");
                                        jobdetaildata.ServiceEndDate = jobdetaildata.JobEndDatetime.Value.ToString("ddd, MMMM dd, yyyy");
                                        jobdetaildata.ServiceEndTime = jobdetaildata.JobEndDatetime.Value.ToString("hh:mm tt");
                                        jobdetaildata.ServiceEndDateTime = jobdetaildata.JobEndDatetime.Value.ToString("dd/MM/yyyy") + " at " + jobdetaildata.JobEndDatetime.Value.ToString("hh:mm tt");
                                        //StaticHelpers.CustomNavigation(_navigation, new Pages.ServiceProviderFlow.JobDetailPage(bookingListTap: jobdetaildata));
                                        if (userTypeEnum == Convert.ToInt32(UserTypeEnum.ServiceProvider))
                                        {
                                            StaticHelpers.CustomNavigation(_navigation, new Pages.ServiceProviderFlow.JobDetailPage(jobdetaildata));
                                        }
                                        else
                                        {
                                            StaticHelpers.CustomNavigation(_navigation, new Pages.WorkerFlow.JobDetailPage(jobdetaildata));
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("JobDetailByrequestID_Exception:-->" + ex.Message);
                                    }
                                }
                                else
                                {
                                    await MaterialDialog.Instance.SnackbarAsync(message: response.message,
                                                msDuration: MaterialSnackbar.DurationShort);
                                }
                            }
                            else
                            {
                                await MaterialDialog.Instance.SnackbarAsync(message: AppResource.ServerError,
                                                msDuration: MaterialSnackbar.DurationShort);
                            }
                            UserDialogs.Instance.HideLoading();
                        }
                        else
                        {
                            await MaterialDialog.Instance.SnackbarAsync(message: AppResource.NoInternetError,
                                                    msDuration: MaterialSnackbar.DurationShort);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("AcceptCommand_Exception:- " + ex.Message);
                        UserDialogs.Instance.HideLoading();
                    }
                    finally
                    {
                        //await _navigation.PopAllPopupAsync(true);
                    }
                });
            }
        } 
        #endregion
    }
}
