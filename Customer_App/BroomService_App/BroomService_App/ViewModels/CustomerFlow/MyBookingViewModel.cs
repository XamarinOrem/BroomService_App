using Acr.UserDialogs;
using BroomService_App.Helpers;
using BroomService_App.Models;
using BroomService_App.Pages;
using BroomService_App.Pages.CustomerFlow;
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
    public class MyBookingViewModel : BaseViewModel
    {
        protected static int UserID;

        protected ObservableCollection<MyBookingModel> AllBookingList = new ObservableCollection<MyBookingModel>();

        #region Constructor
        public MyBookingViewModel(INavigation navigation):base(navigation)
        {
            UserID = CurrentUserId;
            PendingBgColor = StaticHelpers.BlueColor;
            PendingtextColor = StaticHelpers.WhiteColor;
            InprogressBgColor = StaticHelpers.GrayColor;
            InprogresstextColor = StaticHelpers.Black2Color;
            CompletedBgColor = StaticHelpers.GrayColor;
            CompletedtextColor = StaticHelpers.Black2Color;
            CanceledBgColor = StaticHelpers.GrayColor;
            CanceledtextColor = StaticHelpers.Black2Color;

            MessagingCenter.Subscribe<List<MyBookingModel>>(this, "MyBookingListUpdate", (response) =>
            {
                AllBookingList.Clear();
                foreach (var item in response)
                {
                    var bookinglistitem = new MyBookingModel();
                    bookinglistitem = item;
                    
                    bookinglistitem.CategoryName = item.Category.Name;
                    bookinglistitem.Category.Picture = IsImagesValid(item.Category.Picture, apiImageBaseUrl: ApiHelpers.CategoryImageBaseUrl);
                    bookinglistitem.Category.Icon = IsImagesValid(item.Category.Icon, apiImageBaseUrl: ApiHelpers.CategoryImageBaseUrl);
                    bookinglistitem.SubCategory.Picture = IsImagesValid(item.SubCategory.Picture, apiImageBaseUrl: ApiHelpers.SubCategoryImageBaseUrl);
                    bookinglistitem.SubCategory.Icon = IsImagesValid(item.SubCategory.Icon, apiImageBaseUrl: ApiHelpers.SubCategoryImageBaseUrl);
                    bookinglistitem.ServiceProviderProfilePic = IsImagesValid(item.ServiceProviderProfilePic,ApiHelpers.ApiImageBaseUrl);
                    bookinglistitem.ServiceProviderName = !string.IsNullOrEmpty(item.ServiceProviderName) && !string.IsNullOrWhiteSpace(item.ServiceProviderName) ? item.ServiceProviderName : AppResource.BroomService;
                    bookinglistitem.ServiceStartDate = item.JobStartDatetime.Value.ToString("ddd, MMMM dd, yyyy");
                    bookinglistitem.ServiceStartTime = item.JobStartDatetime.Value.ToString("hh:mm tt");
                    bookinglistitem.ServiceStartDateTime = item.JobStartDatetime.Value.ToString("dd/MM/yyyy") + " at " + item.JobStartDatetime.Value.ToString("hh:mm tt");
                    bookinglistitem.ServiceDetailStartDate = item.JobStartDatetime.Value.ToString("dd/MM/yyyy");
                    bookinglistitem.ServiceEndDate = item.JobEndDatetime.Value.ToString("ddd, MMMM dd, yyyy");
                    bookinglistitem.ServiceEndTime = item.JobEndDatetime.Value.ToString("hh:mm tt");
                    bookinglistitem.ServiceEndDateTime = item.JobEndDatetime.Value.ToString("dd/MM/yyyy") + " at " + item.JobEndDatetime.Value.ToString("hh:mm tt");
                    bookinglistitem.ServiceDetailEndDate = item.JobEndDatetime.Value.ToString("dd/MM/yyyy");
                    bookinglistitem.IsNoJobStatusPending = item.JobStatus == Convert.ToInt32(RequestStatus.Pending) ? false : true;

                    if(item.JobStatus == Convert.ToInt32(RequestStatus.Completed))
                    {
                        bookinglistitem.IsPaymentBtnVisible = true;
                        if (item.IsPaymentDone != null && item.IsPaymentDone.Value)
                        {
                            bookinglistitem.PaymentBtnText = AppResource.PaymentBtn1;
                            bookinglistitem.PaymentBgColor = Color.FromHex(StaticHelpers.GrayColor);
                            bookinglistitem.IsPaymentBtnEnable = false;
                        }
                        else
                        {
                            bookinglistitem.PaymentBtnText = AppResource.PaymentBtn;
                            bookinglistitem.PaymentBgColor = Color.FromHex(StaticHelpers.BlueColor);
                            bookinglistitem.IsPaymentBtnEnable = true;
                        }
                    }
                    else
                    {
                        bookinglistitem.IsPaymentBtnVisible = false;
                    }
                    AllBookingList.Add(bookinglistitem);
                }
                GetBookingListByStatus();
            });
        }
        #endregion

        #region BookingList property
        private ObservableCollection<MyBookingModel> _bookingList = new ObservableCollection<MyBookingModel>();

        public ObservableCollection<MyBookingModel> BookingList
        {
            get { return _bookingList; }
            set { SetProperty(ref _bookingList, value); }
        }
        #endregion

        #region Booking Tap Command
        private MyBookingModel _bookingListTap;

        public MyBookingModel BookingListTap
        {
            get { return _bookingListTap; }
            set
            {
                SetProperty(ref _bookingListTap, value);
                //if (BookingListTap != null)
                //{
                //    try
                //    {
                //        StaticHelpers.CustomNavigation(_navigation, new JobDetailPage(BookingListTap));
                //    }
                //    catch (Exception ex)
                //    {
                //        Console.WriteLine("BookingListTapCommand_Exception:- " + ex.Message);
                //    }
                //}
            }
        } 
        #endregion

        #region PendingtextColor Properties
        private string _PendingtextColor;

        public string PendingtextColor
        {
            get { return _PendingtextColor; }
            set { SetProperty(ref _PendingtextColor, value); }
        }
        #endregion

        #region InprogresstextColor Properties
        private string _InprogresstextColor;

        public string InprogresstextColor
        {
            get { return _InprogresstextColor; }
            set { SetProperty(ref _InprogresstextColor, value); }
        }
        #endregion

        #region CompletedtextColor Properties
        private string _completedtextColor;

        public string CompletedtextColor
        {
            get { return _completedtextColor; }
            set { SetProperty(ref _completedtextColor, value); }
        }
        #endregion

        #region CanceledtextColor Properties
        private string _canceledtextColor;

        public string CanceledtextColor
        {
            get { return _canceledtextColor; }
            set { SetProperty(ref _canceledtextColor, value); }
        }
        #endregion

        #region PendingBgColor Properties
        private string _PendingBgColor;

        public string PendingBgColor
        {
            get { return _PendingBgColor; }
            set { SetProperty(ref _PendingBgColor, value); }
        }
        #endregion

        #region InprogressBgColor Properties
        private string _InprogressBgColor;

        public string InprogressBgColor
        {
            get { return _InprogressBgColor; }
            set { SetProperty(ref _InprogressBgColor, value); }
        }
        #endregion

        #region CompletedBgColor Properties
        private string _completedBgColor;

        public string CompletedBgColor
        {
            get { return _completedBgColor; }
            set { SetProperty(ref _completedBgColor, value); }
        }
        #endregion

        #region CanceledBgColor Properties
        private string _canceledBgColor;

        public string CanceledBgColor
        {
            get { return _canceledBgColor; }
            set { SetProperty(ref _canceledBgColor, value); }
        }
        #endregion

        #region BookingStatusCommand
        public Command BookingStatusCommand
        {
            get
            {
                return new Command((e) =>
                {
                    try
                    {
                        switch (e.ToString().ToLower())
                        {
                            case "pending":
                                if (PendingBgColor != StaticHelpers.BlueColor)
                                {
                                    PendingBgColor = StaticHelpers.BlueColor;
                                    PendingtextColor = StaticHelpers.WhiteColor;
                                    InprogressBgColor = StaticHelpers.GrayColor;
                                    InprogresstextColor = StaticHelpers.Black2Color;
                                    CompletedBgColor = StaticHelpers.GrayColor;
                                    CompletedtextColor = StaticHelpers.Black2Color;
                                    CanceledBgColor = StaticHelpers.GrayColor;
                                    CanceledtextColor = StaticHelpers.Black2Color;

                                    GetBookingListByStatus();
                                }
                                break;
                            case "inprogress":
                                if (InprogressBgColor != StaticHelpers.BlueColor)
                                {
                                    InprogressBgColor = StaticHelpers.BlueColor;
                                    InprogresstextColor = StaticHelpers.WhiteColor;
                                    PendingBgColor = StaticHelpers.GrayColor;
                                    PendingtextColor = StaticHelpers.Black2Color;
                                    CompletedBgColor = StaticHelpers.GrayColor;
                                    CompletedtextColor = StaticHelpers.Black2Color;
                                    CanceledBgColor = StaticHelpers.GrayColor;
                                    CanceledtextColor = StaticHelpers.Black2Color;

                                    GetBookingListByStatus();
                                }
                                break;
                            case "completed":
                                if (CompletedBgColor != StaticHelpers.BlueColor)
                                {
                                    PendingBgColor = StaticHelpers.GrayColor;
                                    PendingtextColor = StaticHelpers.Black2Color;
                                    InprogressBgColor = StaticHelpers.GrayColor;
                                    InprogresstextColor = StaticHelpers.Black2Color;
                                    CompletedBgColor = StaticHelpers.BlueColor;
                                    CompletedtextColor = StaticHelpers.WhiteColor;
                                    CanceledBgColor = StaticHelpers.GrayColor;
                                    CanceledtextColor = StaticHelpers.Black2Color;

                                    GetBookingListByStatus();
                                }
                                break;
                            case "canceled":
                                if (CanceledBgColor != StaticHelpers.BlueColor)
                                {
                                    PendingBgColor = StaticHelpers.GrayColor;
                                    PendingtextColor = StaticHelpers.Black2Color;
                                    InprogressBgColor = StaticHelpers.GrayColor;
                                    InprogresstextColor = StaticHelpers.Black2Color;
                                    CompletedBgColor = StaticHelpers.GrayColor;
                                    CompletedtextColor = StaticHelpers.Black2Color;
                                    CanceledBgColor = StaticHelpers.BlueColor;
                                    CanceledtextColor = StaticHelpers.WhiteColor;

                                    GetBookingListByStatus();
                                }
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("BookingStatusCommand_Exception:- " + ex.Message);
                    }
                });
            }
        }
        #endregion

        #region GetBookingListByStatus
        private async void GetBookingListByStatus()
        {
            try
            {
                UserDialogs.Instance.ShowLoading("");
                //BookingList.Clear();
                if (PendingBgColor == StaticHelpers.BlueColor)
                {
                    BookingList = StaticHelpers.ConvertintoObservable<MyBookingModel>(AllBookingList.Where(x => x.JobStatus == Convert.ToInt32(RequestStatus.Pending)).OrderByDescending(x => x.Id));
                }
                else if (InprogressBgColor == StaticHelpers.BlueColor)
                {
                    BookingList = StaticHelpers.ConvertintoObservable<MyBookingModel>(AllBookingList.Where(x => x.JobStatus == Convert.ToInt32(RequestStatus.InProgress)).OrderByDescending(x => x.Id));
                }
                else if (CompletedBgColor == StaticHelpers.BlueColor)
                {
                    BookingList = StaticHelpers.ConvertintoObservable<MyBookingModel>(AllBookingList.Where(x => x.JobStatus == Convert.ToInt32(RequestStatus.Completed)).OrderByDescending(x => x.Id));
                }
                else if (CanceledBgColor == StaticHelpers.BlueColor)
                {
                    BookingList = StaticHelpers.ConvertintoObservable<MyBookingModel>(AllBookingList.Where(x => (x.JobStatus == Convert.ToInt32(RequestStatus.Canceled) || x.JobStatus == Convert.ToInt32(RequestStatus.QuoteCanceled))).OrderByDescending(x => x.Id));
                }

                if(BookingList == null || BookingList.Count <= 0)
                {
                    await MaterialDialog.Instance.SnackbarAsync(message: AppResource.NoJobRequest,
                                       msDuration: 1000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("BookingListbyStatus_Exception:- " + ex.Message);
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }
        #endregion

        #region ViewDetailCommand
        public Command ViewDetailCommand
        {
            get
            {
                return new Command((e) =>
                {
                    try
                    {
                        UserDialogs.Instance.ShowLoading("");
                        var bookingdetail = ((MyBookingModel)e);
                        StaticHelpers.CustomNavigation(_navigation, new JobDetailPage(bookingdetail));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("BookingDetailCommand_Exception:- " + ex.Message);
                    }
                    finally
                    {
                        UserDialogs.Instance.HideLoading();
                    }
                });
            }
        }
        #endregion

        #region PaymentCommand
        public Command PaymentCommand
        {
            get
            {
                return new Command((e) =>
                {
                    try
                    {
                        var bookingdetail = ((MyBookingModel)e);
                        if (bookingdetail.IsPaymentDone == null || !bookingdetail.IsPaymentDone.Value)
                        {
                            StaticHelpers.CustomNavigation(_navigation, new PaymentWebPage(String.Format(ApiHelpers.PaymentWebUrl, CurrentUserId, bookingdetail.Id, bookingdetail.CustomerQuotePrice))); 
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("BookingPaymentCommand_Exception:- " + ex.Message);
                    }
                });
            }
        } 
        #endregion
    }
}
