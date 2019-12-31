using Acr.UserDialogs;
using BroomService_App.Helpers;
using BroomService_App.Models;
using BroomService_App.Pages;
using BroomService_App.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace BroomService_App.ViewModels
{
    public class NotificationViewModel:BaseViewModel
    {
        public static int UserId;
        #region Constructor
        public NotificationViewModel(INavigation navigation) : base(navigation)
        {
            UserId = CurrentUserId;

            getNotificationList();
        }
        #endregion

        #region NotificationList property
        private ObservableCollection<GetNotifications> _notificationList = new ObservableCollection<GetNotifications>();

        public ObservableCollection<GetNotifications> NotificationList
        {
            get { return _notificationList; }
            set { SetProperty(ref _notificationList, value); }
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
                if (NotificationSelected != null)
                {
                    switch (NotificationSelected.IsButtonVisible)
                    {
                        case true:
                            //StaticHelpers.CustomNavigation(_navigation, new JobDetailPage());
                            //MessagingCenter.Send("AcceptVisible", "AcceptVisible");
                            break;
                        case false:
                            //StaticHelpers.CustomNavigation(_navigation, new JobDetailPage());
                            //MessagingCenter.Send("NoButtonVisible", "AcceptVisible");
                            break;
                    }
                }
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
                        if ((Connectivity.ConnectionProfiles.Contains(ConnectionProfile.WiFi) && Connectivity.NetworkAccess.Equals(NetworkAccess.Internet)) || (Connectivity.ConnectionProfiles.Contains(ConnectionProfile.Cellular) && Connectivity.NetworkAccess.Equals(NetworkAccess.Internet)))
                        {
                            UserDialogs.Instance.ShowLoading("");
                            var requestModel = new AcceptRejectQuoteModel()
                            {
                                IsAccept = false,
                                JobRequestId = ((GetNotifications)e).JobRequestId.Value,
                                UserId = CurrentUserId,
                                NotificationId = ((GetNotifications)e).Id.Value
                            };


                            QuotationResponseModel response;
                            try
                            {
                                response = await webApiRestClient.PostAsync<AcceptRejectQuoteModel, QuotationResponseModel>(ApiHelpers.AcceptRejectQuote, requestModel, true);
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
                                    await MaterialDialog.Instance.SnackbarAsync(message: response.message,
                                                msDuration: 1000);
                                    try
                                    {
                                        MessagingCenter.Send("RefreshNotificationView", "RefreshNotificationView");
                                    }
                                    catch (Exception ex)
                                    {

                                    }
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
                            UserDialogs.Instance.HideLoading();
                        }
                        else
                        {
                            await MaterialDialog.Instance.SnackbarAsync(message: AppResource.NoInternetError,
                                                    msDuration: 1000);
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

        #region AcceptCommand
        public Command AcceptCommand
        {
            get
            {
                return new Command(async (e) =>
                {
                    try
                    {
                        if ((Connectivity.ConnectionProfiles.Contains(ConnectionProfile.WiFi) && Connectivity.NetworkAccess.Equals(NetworkAccess.Internet)) || (Connectivity.ConnectionProfiles.Contains(ConnectionProfile.Cellular) && Connectivity.NetworkAccess.Equals(NetworkAccess.Internet)))
                        {
                            UserDialogs.Instance.ShowLoading("");
                            var requestModel = new AcceptRejectQuoteModel()
                            {
                                IsAccept = true,
                                JobRequestId = ((GetNotifications)e).JobRequestId.Value,
                                UserId = CurrentUserId,
                                NotificationId = ((GetNotifications)e).Id.Value
                            };


                            QuotationResponseModel response;
                            try
                            {
                                response = await webApiRestClient.PostAsync<AcceptRejectQuoteModel, QuotationResponseModel>(ApiHelpers.AcceptRejectQuote, requestModel, true);
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
                                    await MaterialDialog.Instance.SnackbarAsync(message: response.message,
                                                msDuration: 1000);
                                    try
                                    {
                                        MessagingCenter.Send("RefreshNotificationView", "RefreshNotificationView");
                                    }
                                    catch (Exception ex)
                                    {
                                        
                                    }
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
                            UserDialogs.Instance.HideLoading();
                        }
                        else
                        {
                            await MaterialDialog.Instance.SnackbarAsync(message: AppResource.NoInternetError,
                                                    msDuration: 1000);
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

        #region getNotificationList
        private async void getNotificationList()
        {
            try
            {
                if ((Connectivity.ConnectionProfiles.Contains(ConnectionProfile.WiFi) && Connectivity.NetworkAccess.Equals(NetworkAccess.Internet)) || (Connectivity.ConnectionProfiles.Contains(ConnectionProfile.Cellular) && Connectivity.NetworkAccess.Equals(NetworkAccess.Internet)))
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
                                if (response.data != null && response.data.Count > 0)
                                {
                                    response.data.Reverse();
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
                                            //FromUserName = notificationdata.Text.StartsWith(" ")? notificationdata.FromUserName : String.Empty,
                                            FromUserName = notificationdata.NotificationStatus.Value != Convert.ToInt32(NotificationStatus.AcceptedQuotation) && notificationdata.NotificationStatus.Value != Convert.ToInt32(NotificationStatus.Rejected) && notificationdata.NotificationStatus.Value != Convert.ToInt32(NotificationStatus.RejectedQuotation) ? notificationdata.FromUserName : String.Empty,
                                            IsButtonVisible = notificationdata.NotificationStatus.Value == Convert.ToInt32(NotificationStatus.SentQuotation) ? true : false,
                                            UserNotificationTime = RelativeDate(notificationdata.CreatedDate.Value),
                                            UserPic = notificationdata.NotificationStatus.Value != Convert.ToInt32(NotificationStatus.AcceptedQuotation) && notificationdata.NotificationStatus.Value != Convert.ToInt32(NotificationStatus.Rejected) && notificationdata.NotificationStatus.Value != Convert.ToInt32(NotificationStatus.RejectedQuotation) ? IsImagesValid(notificationdata.FromUserImage, ApiHelpers.ApiImageBaseUrl) : IsImagesValid(notificationdata.ToUserImage, ApiHelpers.ApiImageBaseUrl),
                                            //UserPic = notificationdata.Text.StartsWith(" ") ? IsImagesValid(notificationdata.FromUserImage, ApiHelpers.ApiImageBaseUrl) : IsImagesValid(notificationdata.ToUserImage, ApiHelpers.ApiImageBaseUrl),
                                            QuotePrice = notificationdata.QuotePrice,
                                            NotificationStatus = notificationdata.NotificationStatus,
                                            ToUserId = notificationdata.ToUserId,
                                            ToUserName = notificationdata.ToUserName
                                        });
                                    }
                                    //NotificationList = _items;
                                    NotificationList = new ObservableCollection<GetNotifications>(_items.OrderByDescending(a => a.CreatedDate).ToList());
                                }
                                else
                                {
                                    //await MaterialDialog.Instance.SnackbarAsync(message: AppResource.NoNotificationError,
                                    //   msDuration: 1000);
                                }
                            }
                            catch (Exception ex)
                            {
                                await MaterialDialog.Instance.SnackbarAsync(message: ex.Message,
                                       msDuration: 1000);
                            }
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
                    UserDialogs.Instance.HideLoading();
                }
                else
                {
                    await MaterialDialog.Instance.SnackbarAsync(message: AppResource.NoInternetError,
                                            msDuration: 1000);
                }
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
            }
        } 
        #endregion
    }
}