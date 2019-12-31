using Acr.UserDialogs;
using Rg.Plugins.Popup.Extensions;
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
using BroomService_App.Popups;
using BroomService_App.Pages.CustomerFlow;
using BroomService_App.Pages.CommonPages;

namespace BroomService_App.ViewModels
{
    public class JobDetailViewModel : BaseViewModel
    {
        private string languageculture;

        #region PaymentMode Field
        private bool _IsPaymentBtnVisible;

        public bool IsPaymentBtnVisible
        {
            get { return _IsPaymentBtnVisible; }
            set { SetProperty(ref _IsPaymentBtnVisible, value); }
        }

        private bool _IsPaymentBtnEnable;

        public bool IsPaymentBtnEnable
        {
            get { return _IsPaymentBtnEnable; }
            set { SetProperty(ref _IsPaymentBtnEnable, value); }
        }

        private Color _PaymentBgColor;

        public Color PaymentBgColor
        {
            get { return _PaymentBgColor; }
            set { SetProperty(ref _PaymentBgColor, value); }
        }

        private string _PaymentBtnText;

        public string PaymentBtnText
        {
            get { return _PaymentBtnText; }
            set { SetProperty(ref _PaymentBtnText, value); }
        }

        public Command PaymentCommand
        {
            get
            {
                return new Command(() =>
                {
                    StaticHelpers.CustomNavigation(_navigation, new PaymentWebPage(String.Format(ApiHelpers.PaymentWebUrl, CurrentUserId, MyBooking.Id, MyBooking.CustomerQuotePrice)));
                    //var paymenturl = ApiHelpers.ApiBaseUrl + "/Payment/DoPayment?userId=" + CurrentUserId + "&requestId=" + MyBooking.Id + "&price=" + MyBooking.CustomerQuotePrice;
                    //StaticHelpers.CustomNavigation(_navigation, new PaymentWebPage(paymenturl));
                });
            }
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

        #region IsCheckList Property
        private bool _IsCheckList;
        public bool IsCheckList
        {
            get { return _IsCheckList; }
            set { SetProperty(ref _IsCheckList, value); }
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

        #region IsImageScrolling Property
        private bool _IsImageScrolling;

        public bool IsImageScrolling
        {
            get { return _IsImageScrolling; }
            set { SetProperty(ref _IsImageScrolling, value); }
        }
        #endregion
        
        #region IsRatingPopup Property
        private bool _IsRatingPopup;

        public bool IsRatingPopup
        {
            get { return _IsRatingPopup; }
            set { SetProperty(ref _IsRatingPopup, value); }
        }
        #endregion

        #region IsSubsubCategoryVisible Property
        private bool _IsSubsubCategoryVisible;

        public bool IsSubsubCategoryVisible
        {
            get { return _IsSubsubCategoryVisible; }
            set { SetProperty(ref _IsSubsubCategoryVisible, value); }
        }
        #endregion

        #region RatingDescription Property
        private string _RatingDescription;

        public string RatingDescription
        {
            get { return _RatingDescription; }
            set { SetProperty(ref _RatingDescription, value); }
        } 
        #endregion

        #region JobRatingIcon Property
        private string _JobRatingIcon;

        public string JobRatingIcon
        {
            get { return _JobRatingIcon; }
            set { SetProperty(ref _JobRatingIcon, value); }
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
        public JobDetailViewModel(INavigation navigation, Models.MyBookingModel bookingListTap) :base(navigation)
        {
            if (Xamarin.Forms.Application.Current.Properties.ContainsKey("AppLocale") && !string.IsNullOrEmpty(Xamarin.Forms.Application.Current.Properties["AppLocale"].ToString()))
            {
                languageculture = Xamarin.Forms.Application.Current.Properties["AppLocale"].ToString();
            }
            else
            {
                languageculture = "en-US";
            }

            //IsRatingPopup = false;
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


            //MyBooking.ServiceProviderProfilePic = IsImagesValid(MyBooking.ServiceProviderProfilePic, ApiHelpers.ApiImageBaseUrl);
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

            if ((string.IsNullOrEmpty(MyBooking.Description) || string.IsNullOrWhiteSpace(MyBooking.Description)) && (MyBooking.ReferenceImages == null || MyBooking.ReferenceImages.Count == 0))
            {
                IsRefDescriptionVisible = false;
            }
            else
            {
                IsRefDescriptionVisible = true;
                if(MyBooking.ReferenceImages != null && MyBooking.ReferenceImages.Count > 0)
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


            if(MyBooking.JobStatus == Convert.ToInt32(RequestStatus.Completed))
            {
                if (MyBooking.UserJobRating != null && MyBooking.UserRating != null && MyBooking.UserJobRating.Value != 0 && MyBooking.UserRating.Value != 0)
                {
                    JobRatingIcon = string.Empty;
                }
                else
                {
                    JobRatingIcon = "ic_customer_rating.png";
                }

                //if(MyBooking.PaymentMethod == Convert.ToInt32(PaymentMethod.ByCreditCard))
                //{
                //    IsPaymentBtnVisible = true;
                //    if(MyBooking.IsPaymentDone != null && MyBooking.IsPaymentDone.Value)
                //    {
                //        PaymentBtnText = AppResource.PaymentBtn1;
                //        PaymentBgColor = Color.FromHex(StaticHelpers.GrayColor);
                //        IsPaymentBtnEnable = false;
                //    }
                //    else
                //    {
                //        PaymentBtnText = AppResource.PaymentBtn;
                //        PaymentBgColor = Color.FromHex(StaticHelpers.BlueColor);
                //        IsPaymentBtnEnable = true;
                //    }
                //}
                //else
                //{
                //    IsPaymentBtnVisible = false;
                //}
                IsPaymentBtnVisible = true;
                if (MyBooking.IsPaymentDone != null && MyBooking.IsPaymentDone.Value)
                {
                    PaymentBtnText = AppResource.PaymentBtn1;
                    PaymentBgColor = Color.FromHex(StaticHelpers.GrayColor);
                    IsPaymentBtnEnable = false;
                }
                else
                {
                    PaymentBtnText = AppResource.PaymentBtn;
                    PaymentBgColor = Color.FromHex(StaticHelpers.BlueColor);
                    IsPaymentBtnEnable = true;
                }
            }
            else
            {
                JobRatingIcon = string.Empty;
            }

            if(MyBooking.CheckList != null && MyBooking.CheckList.Count > 0)
            {
                IsCheckList = true;
                foreach(var item in MyBooking.CheckList)
                {
                    CheckList.Add(new CheckListModel()
                    {
                        CheckListValue = item.TaskDetail,
                        CheckListCheck = item.IsDone.HasValue && item.IsDone.Value == true ? "ic_register_check.png" : "ic_uncheked.png",
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

        #region ReferenceImageSelected
        private ReferenceImagesModel _ReferenceImageSelected = new ReferenceImagesModel();

        public ReferenceImagesModel ReferenceImageSelected
        {
            get { return _ReferenceImageSelected; }
            set
            {
                SetProperty(ref _ReferenceImageSelected, value);
                if (ReferenceImageSelected != null && ReferenceImageSelected.ReferenceImages != null)
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

        #region MapOpenCommand
        public Command MapOpenCommand
        {
            get
            {
                return new Command(async() =>
                {
                    try
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
                    }
                    catch (Exception ex)
                    {
                    }
                });
            }
        }
        #endregion
        
        #region ChatCommand
        public Command ChatCommand
        {
            get
            {
                return new Command(() =>
                {
                    try
                    {
                        StaticHelpers.CustomNavigation(_navigation, new ChatDetailPage());
                        //MessagingCenter.Send(MyBooking.ServiceProviderName, "ChatDetailTitle", MyBooking.ServiceProviderId.HasValue && MyBooking.ServiceProviderId.Value > 0 ? MyBooking.ServiceProviderId.Value : MyBooking.AdminId.Value);
                        if (MyBooking.ServiceProviderId.HasValue && MyBooking.ServiceProviderId.Value > 0)
                        {
                            MessagingCenter.Send(MyBooking.ServiceProviderName, "ChatDetailTitle", MyBooking.ServiceProviderId.Value);
                        }
                        else
                        {
                            MessagingCenter.Send(MyBooking.ServiceProviderName, "ChatDetailTitle", MyBooking.AdminId.Value);
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                });
            }
        }
        #endregion

        #region IsCancelBtnEnable
        private bool _IsCancelBtnEnable;

        public bool IsCancelBtnEnable
        {
            get { return _IsCancelBtnEnable; }
            set { SetProperty(ref _IsCancelBtnEnable, value); }
        }
        #endregion

        #region RightIconCommand
        public Command RightIconCommand
        {
            get
            {
                return new Command(() =>
                {
                    try
                    {
                        _navigation.PushPopupAsync(new RatingPopup(CurrentUserId, MyBooking), true);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("RightIcon_Exception:- " + ex.Message);
                    }
                });
            }
        }
        #endregion

        #region SubmitCommand
        //public Command SubmitCommand
        //{
        //    get
        //    {
        //        return new Command(async() =>
        //        {
        //            if ((Connectivity.ConnectionProfiles.Contains(ConnectionProfile.WiFi) && Connectivity.NetworkAccess.Equals(NetworkAccess.Internet)) || (Connectivity.ConnectionProfiles.Contains(ConnectionProfile.Cellular) && Connectivity.NetworkAccess.Equals(NetworkAccess.Internet)))
        //            {
        //                var requestData = new RatingModel()
        //                {
        //                    CustomerId = CurrentUserId,
        //                    ToUserId = MyBooking.ServiceProviderId.Value,
        //                    Rating = 0,
        //                    Review = RatingDescription
        //                };
        //                RatingModelResponse response;
        //                try
        //                {
        //                    response = await webApiRestClient.PostAsync<RatingModel, RatingModelResponse>(ApiHelpers.SubmitUserReview, requestData, true);
        //                }
        //                catch (Exception ex)
        //                {
        //                    Console.WriteLine("SignupApi_Exception:- " + ex.Message);
        //                    response = null;
        //                }
        //                if (response != null)
        //                {
        //                    if (response.status)
        //                    {
        //                        await MaterialDialog.Instance.SnackbarAsync(message: response.message,
        //                                   msDuration: 1000);
        //                        App.Current.MainPage = new NavigationPage(new HomeTabPage());
        //                    }
        //                    else
        //                    {
        //                        await MaterialDialog.Instance.SnackbarAsync(message: response.message,
        //                                    msDuration: 1000);
        //                    }
        //                }
        //                else
        //                {
        //                    await MaterialDialog.Instance.SnackbarAsync(message: AppResource.ServerError,
        //                                    msDuration: 1000);
        //                }
        //            }
        //            else
        //            {
        //                await MaterialDialog.Instance.SnackbarAsync(message: AppResource.NoInternetError,
        //                                        msDuration: 1000);
        //            }
        //        });
        //    }
        //}
        #endregion
        
        #region PopupCloseCommand
        public Command PopupCloseCommand
        {
            get
            {
                return new Command((btn) =>
                {
                    IsRatingPopup = false;
                });
            }
        }
        #endregion

        #region CancelCommand
        public Command CancelCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsCancelBtnEnable = false;
                });
            }
        }
        #endregion
    }
}
