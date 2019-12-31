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
using Xamarin.Forms;

namespace BroomService_App.ViewModels.ServiceProviderFlow
{
    public class HomeViewModel : BaseViewModel
    {
        public static int UserId;
        protected ObservableCollection<MyBookingModel> AllBookingList = new ObservableCollection<MyBookingModel>();

        #region Constructor
        public HomeViewModel(INavigation navigation) : base(navigation)
        {
            if (Application.Current.Properties.ContainsKey("CurrentUserId"))
            {
                UserId = CurrentUserId = Convert.ToInt32(Application.Current.Properties["CurrentUserId"]);
            }
            if (Application.Current.Properties.ContainsKey("CurrentUserType"))
            {
                CurrentUserType = Convert.ToInt32(Application.Current.Properties["CurrentUserType"]);
            }
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
                searchbookingList = new ObservableCollection<MyBookingModel>();
                SearchBarText = string.Empty;
                AllBookingList.Clear();
                foreach (var item in response)
                {
                    item.CategoryName = item.Category.Name;
                    item.Category.Picture = IsImagesValid(item.Category.Picture, apiImageBaseUrl: ApiHelpers.CategoryImageBaseUrl);
                    item.Category.Icon = IsImagesValid(item.Category.Icon, apiImageBaseUrl: ApiHelpers.CategoryImageBaseUrl);
                    item.SubCategory.Picture = IsImagesValid(item.SubCategory.Picture, apiImageBaseUrl: ApiHelpers.SubCategoryImageBaseUrl);
                    item.SubCategory.Icon = IsImagesValid(item.SubCategory.Icon, apiImageBaseUrl: ApiHelpers.SubCategoryImageBaseUrl);
                    item.CustomerImage = IsImagesValid(item.CustomerImage, ApiHelpers.ApiImageBaseUrl);
                    item.CustomerName = !string.IsNullOrEmpty(item.CustomerName) && !string.IsNullOrWhiteSpace(item.CustomerName) ? FirstCharToUpper(item.CustomerName) : "Nate Parker";
                    item.ServiceStartDate = item.JobStartDatetime.Value.ToString("ddd, MMMM dd, yyyy");
                    item.ServiceStartTime = item.JobStartDatetime.Value.ToString("hh:mm tt");
                    item.ServiceStartDateTime = item.JobStartDatetime.Value.ToString("dd/MM/yyyy") + " at " + item.JobStartDatetime.Value.ToString("hh:mm tt");
                    item.ServiceEndDate = item.JobEndDatetime.Value.ToString("ddd, MMMM dd, yyyy");
                    item.ServiceEndTime = item.JobEndDatetime.Value.ToString("hh:mm tt");
                    item.ServiceEndDateTime = item.JobEndDatetime.Value.ToString("dd/MM/yyyy") + " at " + item.JobEndDatetime.Value.ToString("hh:mm tt");
                    item.ServiceDetailStartDate = item.JobStartDatetime.Value.ToString("dd/MM/yyyy");
                    item.ServiceDetailEndDate = item.JobEndDatetime.Value.ToString("dd/MM/yyyy");
                    //item.IsNoJobStatusPending = item.JobStatus == Convert.ToInt32(RequestStatus.Pending) ? false : true;
                    AllBookingList.Add(item);
                }
                GetBookingListByStatus();
            });
        }
        #endregion

        #region SearchBarText
        private string _SearchBarText;

        public string SearchBarText
        {
            get { return _SearchBarText; }
            set
            {
                SetProperty(ref _SearchBarText, value);
                if (searchbookingList != null && searchbookingList.Count > 0)
                {
                    if (string.IsNullOrEmpty(SearchBarText) || string.IsNullOrWhiteSpace(SearchBarText))
                    {
                        BookingList = searchbookingList;
                    }
                    else
                    {
                        BookingList = new ObservableCollection<MyBookingModel>(searchbookingList.Where(x => x.CategoryName.ToLower().StartsWith(SearchBarText.ToLower())).ToList());
                    } 
                }
            }
        } 
        #endregion

        #region GetBookingListByStatus
        private void GetBookingListByStatus()
        {
            try
            {
                UserDialogs.Instance.ShowLoading("");
                //BookingList.Clear();
                if (PendingBgColor == StaticHelpers.BlueColor)
                {
                    searchbookingList = StaticHelpers.ConvertintoObservable<MyBookingModel>(AllBookingList.Where(x => x.JobStatus == Convert.ToInt32(RequestStatus.InProgress) && x.TimerStartTime == null).OrderByDescending(x => x.Id));
                }
                else if (InprogressBgColor == StaticHelpers.BlueColor)
                {
                    searchbookingList = StaticHelpers.ConvertintoObservable<MyBookingModel>(AllBookingList.Where(x => x.JobStatus.Value == Convert.ToInt32(RequestStatus.InProgress) && x.TimerStartTime != null).OrderByDescending(x=>x.Id));
                }
                else if (CompletedBgColor == StaticHelpers.BlueColor)
                {
                    searchbookingList = StaticHelpers.ConvertintoObservable<MyBookingModel>(AllBookingList.Where(x => x.JobStatus.Value == Convert.ToInt32(RequestStatus.Completed)).OrderByDescending(x=>x.Id));
                }
                else if (CanceledBgColor == StaticHelpers.BlueColor)
                {
                    searchbookingList = StaticHelpers.ConvertintoObservable<MyBookingModel>(AllBookingList.Where(x => (x.JobStatus.Value == Convert.ToInt32(RequestStatus.Canceled) || x.JobStatus.Value == Convert.ToInt32(RequestStatus.QuoteCanceled))).OrderByDescending(x => x.Id));
                }

                if (!string.IsNullOrEmpty(SearchBarText) && !string.IsNullOrWhiteSpace(SearchBarText))
                {
                    BookingList = new ObservableCollection<MyBookingModel>(searchbookingList.Where(x => x.CategoryName.ToLower().StartsWith(SearchBarText.ToLower())).ToList());
                }
                else
                {
                    BookingList = searchbookingList;
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

        #region BookingList property
        private ObservableCollection<MyBookingModel> _bookingList = new ObservableCollection<MyBookingModel>();

        public ObservableCollection<MyBookingModel> BookingList
        {
            get { return _bookingList; }
            set { SetProperty(ref _bookingList, value); }
        }
        protected ObservableCollection<MyBookingModel> searchbookingList = new ObservableCollection<MyBookingModel>();
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
                //        if (userTypeEnum == Convert.ToInt32(UserTypeEnum.ServiceProvider))
                //        {
                //            StaticHelpers.CustomNavigation(_navigation, new Pages.ServiceProviderFlow.JobDetailPage(BookingListTap));
                //        }
                //        else
                //        {
                //            StaticHelpers.CustomNavigation(_navigation, new Pages.WorkerFlow.JobDetailPage(BookingListTap));
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        Console.WriteLine("BookingListTapCommand_Exception:- " + ex.Message);
                //    }
                //}
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
                        if (userTypeEnum == Convert.ToInt32(UserTypeEnum.ServiceProvider))
                        {
                            StaticHelpers.CustomNavigation(_navigation, new Pages.ServiceProviderFlow.JobDetailPage(bookingdetail));
                        }
                        else
                        {
                            StaticHelpers.CustomNavigation(_navigation, new Pages.WorkerFlow.JobDetailPage(bookingdetail));
                        }
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
                                    PendingBgColor = StaticHelpers.GrayColor;
                                    PendingtextColor = StaticHelpers.Black2Color;
                                    InprogressBgColor = StaticHelpers.BlueColor;
                                    InprogresstextColor = StaticHelpers.WhiteColor;
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

        #region RightIconCommand
        public Command RightIconCommand
        {
            get
            {
                return new Command((e) =>
                {
                    try
                    {
                        MessagingCenter.Send("Notification_Tab", "HomeTabBar");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("RightIconCommand_Exception:- " + ex.Message);
                    }
                });
            }
        }
        #endregion
    }
}
