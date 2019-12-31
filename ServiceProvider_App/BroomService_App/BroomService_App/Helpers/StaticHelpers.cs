using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace BroomService_App.Helpers
{
    public static partial class StaticHelpers
    {
        //Static Functions
        public static async void CustomNavigation(INavigation navigation, Page page)
        {
            try
            {
                //var loadingDialogConfiguration = new MaterialLoadingDialogConfiguration()
                //{
                //    TintColor = Color.FromHex("#1A8DFF")
                //};
                //using (await MaterialDialog.Instance.LoadingDialogAsync(message: "Loading...",configuration: loadingDialogConfiguration))
                //{
                //    await Task.Delay(1000);
                //    await navigation.PushModalAsync(page);
                //}
                await navigation.PushAsync(page);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("static function exception: "+ex.Message);
            }
        }

        public static ObservableCollection<T> ConvertintoObservable<T>(IEnumerable original)
        {
            return new ObservableCollection<T>(original.Cast<T>());
        }

        public static bool NotEmptyFieldCheck(string[] fieldname)
        {
            List<bool> boolData = new List<bool>();
            foreach(var stringitem in fieldname)
            {
                if (!string.IsNullOrEmpty(stringitem) && !string.IsNullOrWhiteSpace(stringitem))
                {
                    boolData.Add(true);
                }
                else
                {
                    boolData.Add(false);
                }
            }

            return boolData.Contains(false) ? false : true;
        }
    }

    public static partial class StaticHelpers
    {
        //Static Colors
        public static readonly string MainColor = "#3f0956";
        public static readonly string ButtonTextColor = "#fecc73";
        public static readonly string WhiteColor = "#FFFFFF";
        public static readonly string Black1Color = "#000000";
        public static readonly string Black2Color = "#959595";
        public static readonly string GrayColor = "#d2d2d2";
        public static readonly string RejectColor = "#FF4141";
        public static readonly string BGColor = "#F2F5FC";
        public static readonly string BlueColor = "#1A8DFF";
        public static readonly string TransparentColor = "#00000000";
    }

    public enum AppFlow
    {
        customer_flow,
        service_provide_flow,
        worker_flow
    }

    public enum ButtonParameters
    {
        startJob,
        startTimer,
        endTimer,
        complete,
        accept,
        reject,
        sendQuote,
    }

    //Server side
    public enum NotificationStatus
    {
        Pending = 1,
        Accepted = 2,
        Rejected = 3,
        SentQuotation = 4,
        AcceptedQuotation = 5,
        RejectedQuotation = 6,
        TimerStarted = 7,
        TimerEnded = 8,
        Completed = 9,
        Assigned = 10
    }

    //Server side
    public enum UserTypeEnum
    {
        Admin = 1,
        ServiceProvider = 2,
        Worker = 3,
        Customer = 4
    }

    //server side
    public enum RequestStatus
    {
        Pending = 1,
        InProgress = 2,
        Completed = 3,
        Canceled = 4,
        QuoteCanceled = 5
    }

    public static class ApiHelpers
    {
        //public static readonly string AccountBaseUrl = "http://appmantechnologies.com:8063/api/Account/";
        //public static readonly string PropertyBaseUrl = "http://appmantechnologies.com:8063/api/Property/";
        //public static readonly string ApiImageBaseUrl = "http://appmantechnologies.com:8063/Images/User/";
        //public static readonly string ReferenceImageBaseUrl = "http://appmantechnologies.com:8063/Images/JobRequest/";
        //public static readonly string CategoryImageBaseUrl = "http://appmantechnologies.com:8063/Images/Category/";
        //public static readonly string SubCategoryImageBaseUrl = "http://appmantechnologies.com:8063/Images/SubCategory/";
        //public static readonly string SubSubCategoryImageBaseUrl = "http://appmantechnologies.com:8063/Images/SubSubCategory/";

        //public static readonly string ApiBaseUrl = "http://122.160.233.58:8063";
        //public static readonly string ApiBaseUrl = "http://appmantechnologies.com:8063";
        //public static readonly string ApiBaseUrl = "http://52.47.59.50";
        //public static readonly string ApiBaseUrl = "https://broomservice.oremtechnologies.com";
        //public static readonly string ApiBaseUrl = "http://app.broomservice.co.il";
        public static readonly string ApiBaseUrl = "https://app.broomservice.co.il";

        public static readonly string AccountBaseUrl = ApiBaseUrl + "/api/Account/";
        public static readonly string PropertyBaseUrl = ApiBaseUrl + "/api/Property/";
        public static readonly string ApiImageBaseUrl = ApiBaseUrl + "/Images/User/";
        public static readonly string ReferenceImageBaseUrl = ApiBaseUrl + "/Images/JobRequest/";
        public static readonly string CategoryImageBaseUrl = ApiBaseUrl + "/Images/Category/";
        public static readonly string SubCategoryImageBaseUrl = ApiBaseUrl + "/Images/SubCategory/";
        public static readonly string SubSubCategoryImageBaseUrl = ApiBaseUrl + "/Images/SubSubCategory/";



        public static readonly string LoginApi = "Login";
        public static readonly string Logout = "Logout";
        public static readonly string SignUpApi = "SignUp";
        public static readonly string ForgetPasswordApi = "ForgetPassword";
        public static readonly string ChangePasswordApi = "ChangePassword";
        public static readonly string GetTermsConditionsApi = "GetTermsConditions";
        public static readonly string GetAboutusApi = "GetAboutus";
        public static readonly string GetCountries = "GetCountries";
        public static readonly string GetProfile = "GetProfile?UserId={0}";
        public static readonly string ContactUs = "ContactUs";
        public static readonly string EditProfile = "EditProfile";


        public static readonly string AddUpdateProperty = "AddUpdateProperty";
        public static readonly string GetPropertiesByUserId = "GetPropertiesByUserId?userid={0}";
        public static readonly string GetCategories = "GetCategories";
        public static readonly string AddJobRequest = "AddJobRequest";
        public static readonly string GetCustomerJobRequests = "GetCustomerJobRequests?userid={0}";

        public static readonly string AddChatRequest = "AddChatRequest";
        public static readonly string GetChat = "GetChat?userId={0}";

        public static readonly string UpdateDeviceInfo = "UpdateDeviceInfo";
        public static readonly string GetNotifications = "GetNotifications?userId={0}";

        public static readonly string AcceptRejectRequest = "AcceptRejectRequest";
        public static readonly string GetJobRequests = "GetJobRequests?userId={0}";

        public static readonly string SendQuote = "SendQuote";
        public static readonly string AcceptRejectQuote = "AcceptRejectQuote";
        public static readonly string StartEndTimer = "StartEndTimer";
        public static readonly string CompleteJobRequest = "CompleteJobRequest";

        public static readonly string GetJobRequestDetail = "GetJobRequestDetail?requestId={0}&userId={1}";
    }
}