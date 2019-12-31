using Acr.UserDialogs;
using BroomService_App.Helpers;
using BroomService_App.Models;
using BroomService_App.Pages;
using BroomService_App.Pages.CommonPages;
using BroomService_App.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using Xamarin.Essentials;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace BroomService_App.ViewModels.ServiceProviderFlow
{
    public class JobDetailViewModel : BaseViewModel
    {
        int hour, min, sec = 0;
        Timer jobstartTimer;
        private string languageculture;

        #region IsJobPriceVisible Property
        private bool _IsJobPriceVisible;

        public bool IsJobPriceVisible
        {
            get { return _IsJobPriceVisible; }
            set { SetProperty(ref _IsJobPriceVisible, value); }
        }
        #endregion

        #region IsJobReviewVisible Property
        private bool _IsJobReviewVisible;

        public bool IsJobReviewVisible
        {
            get { return _IsJobReviewVisible; }
            set { SetProperty(ref _IsJobReviewVisible, value); }
        }
        #endregion

        #region IsJobRating Property
        private bool _IsJobRating;

        public bool IsJobRating
        {
            get { return _IsJobRating; }
            set { SetProperty(ref _IsJobRating, value); }
        }
        #endregion

        #region JobRating Property
        private double _JobRating;

        public double JobRating
        {
            get { return _JobRating; }
            set { SetProperty(ref _JobRating, value); }
        }
        #endregion

        #region TimerStart Property
        private string _TimerStart;

        public string TimerStart
        {
            get { return _TimerStart; }
            set { SetProperty(ref _TimerStart, value); }
        }
        #endregion

        #region CheckListHeight Property
        private double _CheckListHeight;

        public double CheckListHeight
        {
            get { return _CheckListHeight; }
            set { SetProperty(ref _CheckListHeight, value); }
        }

        #endregion

        #region CheckList Property
        private ObservableCollection<CheckListModel> _CheckList = new ObservableCollection<CheckListModel>();
        public ObservableCollection<CheckListModel> CheckList
        {
            get { return _CheckList; }
            set { SetProperty(ref _CheckList, value); }
        }
        #endregion

        #region IsCheckList Property
        private bool _IsCheckList;
        public bool IsCheckList
        {
            get { return _IsCheckList; }
            set { SetProperty(ref _IsCheckList, value); }
        }
        #endregion

        #region CheckListSelected
        private CheckListModel _CheckListSelected = new CheckListModel();
        public CheckListModel CheckListSelected
        {
            get { return _CheckListSelected; }
            set
            {
                SetProperty(ref _CheckListSelected, value);
                if (CheckListSelected != null && !string.IsNullOrEmpty(CheckListSelected.CheckListValue))
                {
                    if (userTypeEnum == Convert.ToInt32(UserTypeEnum.ServiceProvider))
                    {
                        //if (MyBooking.JobStatus.Value == Convert.ToInt32(RequestStatus.InProgress) && (IsCompleteTimerBtn))
                        if (IsCompleteTimerBtn)
                        {
                            var item = ((CheckListModel)CheckListSelected);
                            switch (item.CheckListCheck)
                            {
                                case "ic_uncheked.png":
                                    var index = CheckList.IndexOf(item);
                                    CheckList[index].CheckListCheck = "ic_register_check.png";
                                    break;
                                case "ic_register_check.png":
                                    var index1 = CheckList.IndexOf(item);
                                    CheckList[index1].CheckListCheck = "ic_uncheked.png";
                                    break;
                            }
                        }
                    }
                    else if (userTypeEnum == Convert.ToInt32(UserTypeEnum.Worker))
                    {
                        //if (MyBooking.JobStatus.Value == Convert.ToInt32(RequestStatus.InProgress) && (IsEndTimerBtn))
                        if (IsEndTimerBtn)
                        {
                            var item = ((CheckListModel)CheckListSelected);
                            switch (item.CheckListCheck)
                            {
                                case "ic_uncheked.png":
                                    var index = CheckList.IndexOf(item);
                                    CheckList[index].CheckListCheck = "ic_register_check.png";
                                    break;
                                case "ic_register_check.png":
                                    var index1 = CheckList.IndexOf(item);
                                    CheckList[index1].CheckListCheck = "ic_uncheked.png";
                                    break;
                            }
                        }
                    }
                }
            }
        } 
        #endregion

        #region CheckBoxCommand
        public Command CheckBoxCommand
        {
            get
            {
                return new Command((e) =>
                {
                    if (userTypeEnum == Convert.ToInt32(UserTypeEnum.ServiceProvider))
                    {
                        if (MyBooking.JobStatus.Value == Convert.ToInt32(RequestStatus.InProgress) && (IsCompleteTimerBtn))
                        {
                            var item = ((CheckListModel)e);
                            switch (item.CheckListCheck)
                            {
                                case "ic_uncheked.png":
                                    var index = CheckList.IndexOf(item);
                                    CheckList[index].CheckListCheck = "ic_register_check.png";
                                    break;
                                case "ic_register_check.png":
                                    var index1 = CheckList.IndexOf(item);
                                    CheckList[index1].CheckListCheck = "ic_uncheked.png";
                                    break;
                            }
                        }
                    }
                    else if (userTypeEnum == Convert.ToInt32(UserTypeEnum.Worker))
                    {
                        if (MyBooking.JobStatus.Value == Convert.ToInt32(RequestStatus.InProgress) && (IsEndTimerBtn))
                        {
                            var item = ((CheckListModel)e);
                            switch (item.CheckListCheck)
                            {
                                case "ic_uncheked.png":
                                    var index = CheckList.IndexOf(item);
                                    CheckList[index].CheckListCheck = "ic_register_check.png";
                                    break;
                                case "ic_register_check.png":
                                    var index1 = CheckList.IndexOf(item);
                                    CheckList[index1].CheckListCheck = "ic_uncheked.png";
                                    break;
                            }
                        }
                    }
                });
            }
        }
        #endregion

        #region IsSubsubCategoryVisible property
        private bool _IsSubsubCategoryVisible;

        public bool IsSubsubCategoryVisible
        {
            get { return _IsSubsubCategoryVisible; }
            set { SetProperty(ref _IsSubsubCategoryVisible, value); }
        } 
        #endregion

        #region MyBookingModel
        private MyBookingModel _MyBooking = new MyBookingModel();
        public MyBookingModel MyBooking
        {
            get { return _MyBooking; }
            set { SetProperty(ref _MyBooking, value); }
        }
        #endregion

        #region SubSubCategoryList
        private ObservableCollection<SubSubCategory> _SubSubCategoryList = new ObservableCollection<SubSubCategory>();
        public ObservableCollection<SubSubCategory> SubSubCategoryList
        {
            get { return _SubSubCategoryList; }
            set { SetProperty(ref _SubSubCategoryList, value); }
        }
        #endregion

        #region SubSubCategoryHeight
        private double _SubSubCategoryHeight;

        public double SubSubCategoryHeight
        {
            get { return _SubSubCategoryHeight; }
            set { SetProperty(ref _SubSubCategoryHeight, value); }
        }
        #endregion

        #region Constructor
        public JobDetailViewModel(INavigation navigation, MyBookingModel bookingListTap) : base(navigation)
        {
            if (Xamarin.Forms.Application.Current.Properties.ContainsKey("AppLocale") && !string.IsNullOrEmpty(Xamarin.Forms.Application.Current.Properties["AppLocale"].ToString()))
            {
                languageculture = Xamarin.Forms.Application.Current.Properties["AppLocale"].ToString();
            }
            else
            {
                languageculture = "en-US";
            }
            //IsReferenceImagepopup = false;
            //IsQuotePopup = false;
            MyBooking = bookingListTap;
            switch (languageculture)
            {
                case "en-US":
                    MyBooking.Category.Name = MyBooking.Category.Name;
                    MyBooking.SubCategory.Name = MyBooking.SubCategory.Name;
                    break;
                case "fr-FR":
                    MyBooking.Category.Name = MyBooking.Category.Name_French;
                    MyBooking.SubCategory.Name = MyBooking.SubCategory.Name_French;
                    break;
                case "he-IL":
                    MyBooking.Category.Name = MyBooking.Category.Name_Hebrew;
                    MyBooking.SubCategory.Name = MyBooking.SubCategory.Name_Hebrew;
                    break;
                case "ru-RU":
                    MyBooking.Category.Name = MyBooking.Category.Name_Russian;
                    MyBooking.SubCategory.Name = MyBooking.SubCategory.Name_Russian;
                    break;
            }
            if (MyBooking.SubSubCategories != null && MyBooking.SubSubCategories.Count > 0)
            {
                IsSubsubCategoryVisible = true;
                foreach (var item in MyBooking.SubSubCategories)
                {
                    switch (languageculture)
                    {
                        case "en-US":
                            item.Name = item.Name;
                            break;
                        case "fr-FR":
                            item.Name = item.Name_French;
                            break;
                        case "he-IL":
                            item.Name = item.Name_Hebrew;
                            break;
                        case "ru-RU":
                            item.Name = item.Name_Russian;
                            break;
                    }
                    SubSubCategoryList.Add(item);
                }
                SubSubCategoryHeight = SubSubCategoryList.Count * 70;
            }
            else
            {
                IsSubsubCategoryVisible = false;
            }

            if (MyBooking.IsShownQuote != null && MyBooking.IsShownQuote.Value && MyBooking.QuotePrice != null && MyBooking.QuotePrice.Value > 0)
            {
                IsJobPriceVisible = true;
            }
            else
            {
                IsJobPriceVisible = false;
            }

            if(MyBooking.UserJobRating != null)
            {
                JobRating = MyBooking.UserJobRating.Value;
            }
            else
            {
                JobRating = 0;
            }

            if(!string.IsNullOrEmpty(MyBooking.JobReview)&&!string.IsNullOrWhiteSpace(MyBooking.JobReview))
            {
                IsJobReviewVisible = true;
            }
            else
            {
                IsJobReviewVisible = false;
            }

            if ((string.IsNullOrEmpty(MyBooking.Description) || string.IsNullOrWhiteSpace(MyBooking.Description)) && (MyBooking.ReferenceImages == null || MyBooking.ReferenceImages.Count == 0))
            {
                IsRefDescriptionVisible = false;
            }
            else
            {
                IsRefDescriptionVisible = true;
                if (MyBooking.ReferenceImages != null && MyBooking.ReferenceImages.Count > 0)
                {
                    IsRefImagesVisible = true;
                    foreach (var item in MyBooking.ReferenceImages)
                    {
                        ReferenceImagesList.Add(new ReferenceImagesModel()
                        {
                            ReferenceImages = IsImagesValid(item, ApiHelpers.ReferenceImageBaseUrl)
                        });
                    }
                }
                else
                {
                    IsRefImagesVisible = false;
                }

                if (string.IsNullOrEmpty(MyBooking.Description) || string.IsNullOrWhiteSpace(MyBooking.Description))
                {
                    IsJobDescriptionVisible = false;
                }
                else
                {
                    IsJobDescriptionVisible = true;
                }
            }


            switch (MyBooking.JobStatus.Value)
            {
                case 1:                                     // RequestStatus.Pending
                    IsJobRating = false;
                    IsStartTimerBtn = false;
                    IsEndTimerBtn = false;
                    IsCompleteTimerBtn = false;
                    IsAcceptRejectBtn = true;
                    IsSendQuoteBtn = false;
                    break;
                case 2:                                     // RequestStatus.InProgress
                    IsJobRating = false;
                    if (MyBooking.IsShownQuote.Value)
                    {
                        if (MyBooking.QuotePrice == null)
                        {
                            IsStartTimerBtn = false;
                            IsEndTimerBtn = false;
                            IsCompleteTimerBtn = false;
                            IsAcceptRejectBtn = false;
                            IsSendQuoteBtn = true;
                        }
                        else if (MyBooking.IsQuoteApproved.HasValue && MyBooking.IsQuoteApproved.Value)
                        {
                            if (userTypeEnum == Convert.ToInt32(UserTypeEnum.Worker))
                            {
                                if (MyBooking.TimerStartTime == null)
                                {
                                    IsStartTimerBtn = true;
                                    IsEndTimerBtn = false;
                                    IsCompleteTimerBtn = false;
                                    IsAcceptRejectBtn = false;
                                    IsSendQuoteBtn = false;
                                }
                                else if (MyBooking.TimerEndTime == null)
                                {
                                    var currenttime = DateTime.Now - MyBooking.TimerStartTime.Value;
                                    if (currenttime.Days > 0)
                                    {
                                        hour = (currenttime.Days * 24) + currenttime.Hours;
                                    }
                                    else
                                    {
                                        hour = currenttime.Hours;
                                    }
                                    min = currenttime.Minutes;
                                    sec = currenttime.Seconds;
                                    jobstartTimer = new Timer(_ => UpdateJobTimer(), null, 0, 1000);

                                    IsStartTimerBtn = false;
                                    IsEndTimerBtn = true;
                                    IsCompleteTimerBtn = false;
                                    IsAcceptRejectBtn = false;
                                    IsSendQuoteBtn = false;
                                }
                                else
                                {
                                    IsStartTimerBtn = false;
                                    IsEndTimerBtn = false;
                                    IsCompleteTimerBtn = true;
                                    IsAcceptRejectBtn = false;
                                    IsSendQuoteBtn = false;
                                }
                            }
                            else if (userTypeEnum == Convert.ToInt32(UserTypeEnum.ServiceProvider))
                            {
                                if (MyBooking.TimerStartTime == null)
                                {
                                    IsStartTimerBtn = true;
                                    IsEndTimerBtn = false;
                                    IsCompleteTimerBtn = false;
                                    IsAcceptRejectBtn = false;
                                    IsSendQuoteBtn = false;
                                }
                                else
                                {
                                    IsStartTimerBtn = false;
                                    IsEndTimerBtn = false;
                                    IsCompleteTimerBtn = true;
                                    IsAcceptRejectBtn = false;
                                    IsSendQuoteBtn = false;
                                }
                            }
                        }
                        else
                        {
                            IsStartTimerBtn = false;
                            IsEndTimerBtn = false;
                            IsCompleteTimerBtn = false;
                            IsAcceptRejectBtn = false;
                            IsSendQuoteBtn = false;
                        }
                    }
                    else
                    {
                        if (userTypeEnum == Convert.ToInt32(UserTypeEnum.Worker))
                        {
                            if (MyBooking.TimerStartTime == null)
                            {
                                IsStartTimerBtn = true;
                                IsEndTimerBtn = false;
                                IsCompleteTimerBtn = false;
                                IsAcceptRejectBtn = false;
                                IsSendQuoteBtn = false;
                            }
                            else if (MyBooking.TimerEndTime == null)
                            {
                                var currenttime = DateTime.Now - MyBooking.TimerStartTime.Value;
                                if (currenttime.Days > 0)
                                {
                                    hour = (currenttime.Days * 24) + currenttime.Hours;
                                }
                                else
                                {
                                    hour = currenttime.Hours;
                                }
                                min = currenttime.Minutes;
                                sec = currenttime.Seconds;
                                jobstartTimer = new Timer(_ => UpdateJobTimer(), null, 0, 1000);

                                IsStartTimerBtn = false;
                                IsEndTimerBtn = true;
                                IsCompleteTimerBtn = false;
                                IsAcceptRejectBtn = false;
                                IsSendQuoteBtn = false;
                            }
                            else
                            {
                                IsStartTimerBtn = false;
                                IsEndTimerBtn = false;
                                IsCompleteTimerBtn = true;
                                IsAcceptRejectBtn = false;
                                IsSendQuoteBtn = false;
                            }
                        }
                        else
                        {
                            if (MyBooking.TimerStartTime == null)
                            {
                                IsStartTimerBtn = true;
                                IsEndTimerBtn = false;
                                IsCompleteTimerBtn = false;
                                IsAcceptRejectBtn = false;
                                IsSendQuoteBtn = false;
                            }
                            else
                            {
                                IsStartTimerBtn = false;
                                IsEndTimerBtn = false;
                                IsCompleteTimerBtn = true;
                                IsAcceptRejectBtn = false;
                                IsSendQuoteBtn = false;
                            }
                        }
                    }
                    break;
                case 3:                                     // RequestStatus.Completed
                    IsJobRating = true;
                    IsStartTimerBtn = false;
                    IsEndTimerBtn = false;
                    IsCompleteTimerBtn = false;
                    IsAcceptRejectBtn = false;
                    IsSendQuoteBtn = false;
                    break;
                case 4:                                     // RequestStatus.Cancelled
                    IsJobRating = false;
                    IsStartTimerBtn = false;
                    IsEndTimerBtn = false;
                    IsCompleteTimerBtn = false;
                    IsAcceptRejectBtn = false;
                    IsSendQuoteBtn = false;
                    break;
            }

            if (MyBooking.CheckList != null && MyBooking.CheckList.Count > 0)
            {
                IsCheckList = true;
                foreach (var item in MyBooking.CheckList)
                {
                    CheckList.Add(new CheckListModel()
                    {
                        CheckListValue = item.TaskDetail,
                        CheckListCheck = item.IsDone.HasValue && item.IsDone.Value == true ? "ic_register_check.png" : MyBooking.TimerEndTime != null ? "ic_register_check.png" : "ic_uncheked.png",
                        //IsCheckListCheck = userTypeEnum == Convert.ToInt32(UserTypeEnum.ServiceProvider) ? IsCheckboxVisible(MyBooking, IsCompleteTimerBtn) : IsCheckboxVisible(MyBooking, IsEndTimerBtn)
                    });
                    CheckListHeight = (50 * CheckList.Count) + 10;
                }
            }
            else
            {
                IsCheckList = false;
            }
        }
        #endregion

        //#region ReferenceImage
        //private ImageSource _ReferenceImage;

        //public ImageSource ReferenceImage
        //{
        //    get { return _ReferenceImage; }
        //    set { SetProperty(ref _ReferenceImage, value); }
        //}
        //#endregion

        #region ReferenceImageSelected
        private ReferenceImagesModel _ReferenceImageSelected = new ReferenceImagesModel();

        public ReferenceImagesModel ReferenceImageSelected
        {
            get { return _ReferenceImageSelected; }
            set
            {
                SetProperty(ref _ReferenceImageSelected, value);
                if(ReferenceImageSelected != null && ReferenceImageSelected.ReferenceImages != null)
                {
                    StaticHelpers.CustomNavigation(_navigation, new ImageZoomPage(ReferenceImageSelected.ReferenceImages));
                    //ReferenceImage = ReferenceImageSelected.ReferenceImages;
                    //IsReferenceImagepopup = true;
                }
            }
        }
        #endregion
        
        #region ReferenceImages
        private ObservableCollection<ReferenceImagesModel> _ReferenceImagesList = new ObservableCollection<ReferenceImagesModel>();

        public ObservableCollection<ReferenceImagesModel> ReferenceImagesList
        {
            get { return _ReferenceImagesList; }
            set { SetProperty(ref _ReferenceImagesList, value); }
        }
        #endregion

        #region IsRefImagesVisible property
        private bool _IsRefImagesVisible;

        public bool IsRefImagesVisible
        {
            get { return _IsRefImagesVisible; }
            set { SetProperty(ref _IsRefImagesVisible, value); }
        }
        #endregion

        #region IsRefDescriptionVisible property
        private bool _IsRefDescriptionVisible;

        public bool IsRefDescriptionVisible
        {
            get { return _IsRefDescriptionVisible; }
            set { SetProperty(ref _IsRefDescriptionVisible, value); }
        }
        #endregion

        #region IsJobDescriptionVisible property
        private bool _IsJobDescriptionVisible;

        public bool IsJobDescriptionVisible
        {
            get { return _IsJobDescriptionVisible; }
            set { SetProperty(ref _IsJobDescriptionVisible, value); }
        }
        #endregion

        #region ChatCommand
        public Command ChatCommand
        {
            get
            {
                return new Command(() =>
                {
                    StaticHelpers.CustomNavigation(_navigation, new ChatDetailPage());
                    MessagingCenter.Send(MyBooking.CustomerName, "ChatDetailTitle", MyBooking.CustomerId.Value);
                });
            }
        }
        #endregion

        #region PropertyDetailCommand
        public Command PropertyDetailCommand
        {
            get
            {
                return new Command(() =>
                {
                    try
                    {
                        UserDialogs.Instance.ShowLoading("");
                        StaticHelpers.CustomNavigation(_navigation, new PropertyDetailPage(MyBooking.PropertyDataModel));
                        UserDialogs.Instance.HideLoading();
                    }
                    catch (Exception ex)
                    {
                        UserDialogs.Instance.HideLoading();
                    }
                });
            }
        }
        #endregion

        #region MapOpenCommand
        public Command MapOpenCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (MyBooking.PropertyLatitude.HasValue && MyBooking.PropertyLongitude.HasValue)
                    {
                        if (Device.RuntimePlatform == Device.iOS)
                        {
                            var request = string.Format("http://maps.apple.com/?daddr=" + MyBooking.PropertyLatitude.Value + "," + MyBooking.PropertyLongitude.Value + "");
                            await Launcher.OpenAsync(new Uri(request));
                        }
                        if (Device.RuntimePlatform == Device.Android)
                        {
                            var request = string.Format("http://maps.google.com/?daddr=" + MyBooking.PropertyLatitude.Value + "," + MyBooking.PropertyLongitude.Value + "");
                            await Launcher.OpenAsync(new Uri(request));
                        }
                    }
                });
            }
        }
        #endregion

        #region IsStartTimerBtn Property
        private bool _isStartTimerBtn;

        public bool IsStartTimerBtn
        {
            get { return _isStartTimerBtn; }
            set { SetProperty(ref _isStartTimerBtn, value); }
        }
        #endregion

        #region IsEndTimerBtn Property
        private bool _isEndTimerBtn;

        public bool IsEndTimerBtn
        {
            get { return _isEndTimerBtn; }
            set { SetProperty(ref _isEndTimerBtn, value); }
        }
        #endregion

        #region IsCompleteTimerBtn Property
        private bool _isCompleteTimerBtn;

        public bool IsCompleteTimerBtn
        {
            get { return _isCompleteTimerBtn; }
            set { SetProperty(ref _isCompleteTimerBtn, value); }
        }
        #endregion

        //#region IsReferenceImagepopup Property
        //private bool _IsReferenceImagepopup;

        //public bool IsReferenceImagepopup
        //{
        //    get { return _IsReferenceImagepopup; }
        //    set { SetProperty(ref _IsReferenceImagepopup, value); }
        //}
        //#endregion

        #region IsQuotePopup Property
        private bool _isQuotePopup;

        public bool IsQuotePopup
        {
            get { return _isQuotePopup; }
            set { SetProperty(ref _isQuotePopup, value); }
        } 
        #endregion
        
        #region IsSendQuoteBtn Property
        private bool _isSendQuoteBtn;

        public bool IsSendQuoteBtn
        {
            get { return _isSendQuoteBtn; }
            set { SetProperty(ref _isSendQuoteBtn, value); }
        } 
        #endregion
        
        #region IsAcceptRejectBtn Property
        private bool _isAcceptRejectBtn;

        public bool IsAcceptRejectBtn
        {
            get { return _isAcceptRejectBtn; }
            set { SetProperty(ref _isAcceptRejectBtn, value); }
        }
        #endregion

        #region JobDetailCommands
        public Command JobDetailCommands
        {
            get
            {
                return new Command(async(btn) =>
                {
                    switch (btn)
                    {
                        case ButtonParameters.startJob:
                            {
                                try
                                {
                                    if (Connectivity.NetworkAccess.Equals(NetworkAccess.Internet) || Connectivity.ConnectionProfiles.Contains(ConnectionProfile.WiFi))
                                    {
                                        UserDialogs.Instance.ShowLoading("");
                                        var requestModel = new UpdateTimerTimeModel()
                                        {
                                            UserId = CurrentUserId,
                                            IsStart = true,
                                            TimerTime = DateTime.Now.TimeOfDay,
                                            JobRequestId = MyBooking.Id
                                        };


                                        TimerResponseModel response;
                                        try
                                        {
                                            response = await webApiRestClient.PostAsync<UpdateTimerTimeModel, TimerResponseModel>(ApiHelpers.StartEndTimer, requestModel, true);
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
                                                            msDuration: MaterialSnackbar.DurationShort);

                                                IsStartTimerBtn = false;
                                                IsCompleteTimerBtn = true;
                                                IsAcceptRejectBtn = false;
                                                IsSendQuoteBtn = false;
                                                sec = 0;
                                                min = 0;
                                                hour = 0;
                                                jobstartTimer = new Timer(_ => UpdateJobTimer(), null, 0, 1000);
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
                            }
                            break;
                        case ButtonParameters.startTimer:
                            {
                                try
                                {
                                    if (Connectivity.NetworkAccess.Equals(NetworkAccess.Internet) || Connectivity.ConnectionProfiles.Contains(ConnectionProfile.WiFi))
                                    {
                                        UserDialogs.Instance.ShowLoading("");
                                        var requestModel = new UpdateTimerTimeModel()
                                        {
                                            UserId = CurrentUserId,
                                            IsStart = true,
                                            TimerTime = DateTime.Now.TimeOfDay,
                                            JobRequestId = MyBooking.Id
                                        };


                                        TimerResponseModel response;
                                        try
                                        {
                                            response = await webApiRestClient.PostAsync<UpdateTimerTimeModel, TimerResponseModel>(ApiHelpers.StartEndTimer, requestModel, true);
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
                                                            msDuration: MaterialSnackbar.DurationShort);

                                                IsStartTimerBtn = false;
                                                IsEndTimerBtn = true;
                                                IsCompleteTimerBtn = false;
                                                IsAcceptRejectBtn = false;
                                                IsSendQuoteBtn = false;
                                                sec = 0;
                                                min = 0;
                                                hour = 0;
                                                jobstartTimer = new Timer(_ => UpdateJobTimer(), null, 0, 1000);
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
                            }
                            break;
                        case ButtonParameters.endTimer:
                            {
                                try
                                {
                                    if (Connectivity.NetworkAccess.Equals(NetworkAccess.Internet) || Connectivity.ConnectionProfiles.Contains(ConnectionProfile.WiFi))
                                    {
                                        UserDialogs.Instance.ShowLoading("");

                                        var item = CheckList.Where(x => x.CheckListCheck == "ic_uncheked.png").ToList();
                                        if(item != null && item.Count > 0)
                                        {
                                            await MaterialDialog.Instance.SnackbarAsync(message: AppResource.CheckListError,
                                                           msDuration: MaterialSnackbar.DurationShort);
                                            UserDialogs.Instance.HideLoading();
                                            return;
                                        }

                                        var requestModel = new UpdateTimerTimeModel()
                                        {
                                            UserId = CurrentUserId,
                                            IsStart = false,
                                            TimerTime = DateTime.Now.TimeOfDay,
                                            JobRequestId = MyBooking.Id
                                        };


                                        TimerResponseModel response;
                                        try
                                        {
                                            response = await webApiRestClient.PostAsync<UpdateTimerTimeModel, TimerResponseModel>(ApiHelpers.StartEndTimer, requestModel, true);
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
                                                            msDuration: MaterialSnackbar.DurationShort);
                                                if (jobstartTimer != null)
                                                {
                                                    jobstartTimer.Dispose();
                                                }
                                                IsStartTimerBtn = false;
                                                IsEndTimerBtn = false;
                                                IsCompleteTimerBtn = true;
                                                IsAcceptRejectBtn = false;
                                                IsSendQuoteBtn = false;
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
                                    Console.WriteLine("EndCommand_Exception:- " + ex.Message);
                                    UserDialogs.Instance.HideLoading();
                                }
                            }
                            break;
                        case ButtonParameters.complete:
                            {
                                try
                                {
                                    if (Connectivity.NetworkAccess.Equals(NetworkAccess.Internet) || Connectivity.ConnectionProfiles.Contains(ConnectionProfile.WiFi))
                                    {
                                        UserDialogs.Instance.ShowLoading("");
                                        if (CheckList != null && CheckList.Count > 0)
                                        {
                                            var item = CheckList.Where(x => x.CheckListCheck == "ic_uncheked.png").ToList();
                                            if (item != null && item.Count > 0)
                                            {
                                                await MaterialDialog.Instance.SnackbarAsync(message: AppResource.CheckListError,
                                                               msDuration: MaterialSnackbar.DurationShort);
                                                UserDialogs.Instance.HideLoading();
                                                return;
                                            }
                                        } 
                                        var requestModel = new CompleteJobRequestModel()
                                        {
                                            UserId = CurrentUserId,
                                            JobRequestId = MyBooking.Id
                                        };


                                        TimerResponseModel response;
                                        try
                                        {
                                            response = await webApiRestClient.PostAsync<CompleteJobRequestModel, TimerResponseModel>(ApiHelpers.CompleteJobRequest, requestModel, true);
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
                                                            msDuration: MaterialSnackbar.DurationShort);
                                                await _navigation.PopAsync();
                                                MessagingCenter.Send("Home_Tab", "HomeTabBar");
                                                IsStartTimerBtn = false;
                                                IsEndTimerBtn = false;
                                                IsCompleteTimerBtn = false;
                                                IsAcceptRejectBtn = false;
                                                IsSendQuoteBtn = false;
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
                                    Console.WriteLine("CompleteCommand_Exception:- " + ex.Message);
                                    UserDialogs.Instance.HideLoading();
                                }
                            }
                            break;
                        case ButtonParameters.accept:
                            {
                                try
                                {
                                    if (Connectivity.NetworkAccess.Equals(NetworkAccess.Internet) || Connectivity.ConnectionProfiles.Contains(ConnectionProfile.WiFi))
                                    {
                                        UserDialogs.Instance.ShowLoading("");
                                        var requestModel = new ApplyJobRequestModel()
                                        {
                                            IsAccept = "true",
                                            JobRequestId = MyBooking.Id,
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
                                                await MaterialDialog.Instance.SnackbarAsync(message: response.message,
                                                            msDuration: MaterialSnackbar.DurationShort);
                                                if (MyBooking.IsShownQuote.Value)
                                                {
                                                    IsStartTimerBtn = false;
                                                    IsEndTimerBtn = false;
                                                    IsCompleteTimerBtn = false;
                                                    IsAcceptRejectBtn = false;
                                                    IsSendQuoteBtn = true;
                                                }
                                                else
                                                {
                                                    IsStartTimerBtn = true;
                                                    IsEndTimerBtn = false;
                                                    IsCompleteTimerBtn = false;
                                                    IsAcceptRejectBtn = false;
                                                    IsSendQuoteBtn = false;
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
                            }
                            break;
                        case ButtonParameters.reject:
                            {
                                try
                                {
                                    if (Connectivity.NetworkAccess.Equals(NetworkAccess.Internet) || Connectivity.ConnectionProfiles.Contains(ConnectionProfile.WiFi))
                                    {
                                        UserDialogs.Instance.ShowLoading("");
                                        var requestModel = new ApplyJobRequestModel()
                                        {
                                            IsAccept = "false",
                                            JobRequestId = MyBooking.Id,
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
                                                await MaterialDialog.Instance.SnackbarAsync(message: response.message,
                                                            msDuration: MaterialSnackbar.DurationShort);
                                                await _navigation.PopAsync();
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
                            }
                            break;
                        case ButtonParameters.sendQuote:
                            IsQuotePopup = true;
                            break;
                    }
                });
            }
        }
        #endregion

        //#region ZoomImageCommand
        //public Command ZoomImageCommand
        //{
        //    get
        //    {
        //        return new Command(() =>
        //        {

        //            IsReferenceImagepopup = true;
        //        });
        //    }
        //}
        //#endregion

        //#region ImagePopupCloseCommand
        //public Command ImagePopupCloseCommand
        //{
        //    get
        //    {
        //        return new Command(() =>
        //        {
        //            IsReferenceImagepopup = false;
        //        });
        //    }
        //}
        //#endregion

        #region TimerStartUpdate
        private void UpdateJobTimer()
        {
            if (sec == 59)
            {
                sec = 0;
                min++;
            }
            else
            {
                sec++;
            }

            if(min == 59)
            {
                min = 0;
                hour++;
            }

            if (sec < 10)
            {
                if (min < 10)
                {
                    if (hour < 10)
                    {
                        TimerStart = string.Format("0{0}:0{1}:0{2}", hour, min, sec);
                    }
                    else
                    {
                        TimerStart = string.Format("{0}:0{1}:0{2}", hour, min, sec);
                    }
                }
                else
                {
                    if (hour < 10)
                    {
                        TimerStart = string.Format("0{0}:{1}:0{2}", hour, min, sec);
                    }
                    else
                    {
                        TimerStart = string.Format("{0}:{1}:0{2}", hour, min, sec);
                    }
                }
            }
            else
            {
                if (min < 10)
                {
                    if (hour < 10)
                    {
                        TimerStart = string.Format("0{0}:0{1}:{2}", hour, min, sec);
                    }
                    else
                    {
                        TimerStart = string.Format("{0}:0{1}:{2}", hour, min, sec);
                    }
                }
                else
                {
                    if (hour < 10)
                    {
                        TimerStart = string.Format("0{0}:{1}:{2}", hour, min, sec);
                    }
                    else
                    {
                        TimerStart = string.Format("{0}:{1}:{2}", hour, min, sec);
                    }
                }
            }
        }
        #endregion

        #region PopupCloseCommand
        public Command PopupCloseCommand
        {
            get
            {
                return new Command((btn) =>
                {
                    IsQuotePopup = false;
                });
            }
        }
        #endregion

        #region Quotataions variables
        private string _Quotataions;

        public string Quotataions
        {
            get { return _Quotataions; }
            set { SetProperty(ref _Quotataions, value); }
        } 
        #endregion

        #region Done Command
        public Command DoneCommand
        {
            get
            {
                return new Command(async(btn) =>
                {
                    try
                    {
                        try
                        {
                            if (Connectivity.NetworkAccess.Equals(NetworkAccess.Internet) || Connectivity.ConnectionProfiles.Contains(ConnectionProfile.WiFi))
                            {
                                UserDialogs.Instance.ShowLoading("");
                                var requestModel = new QuotePriceModel()
                                {
                                    UserId = CurrentUserId,
                                    QuotePrice = Convert.ToDecimal(Quotataions),
                                    JobRequestId = MyBooking.Id
                                };


                                QuotationResponseModel response;
                                try
                                {
                                    response = await webApiRestClient.PostAsync<QuotePriceModel, QuotationResponseModel>(ApiHelpers.SendQuote, requestModel, true);
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
                                                    msDuration: MaterialSnackbar.DurationShort);
                                        try
                                        {

                                            await _navigation.PopAsync();
                                            //MessagingCenter.Send("Notification_Tab", "HomeTabBar");
                                        }
                                        catch (Exception ex)
                                        {

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
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("DoneCommand_Exception:- " + ex.Message);
                    }
                });
            }
        } 
        #endregion
    }
}
