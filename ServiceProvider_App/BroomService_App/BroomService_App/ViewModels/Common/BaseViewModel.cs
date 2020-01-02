
using BroomService_App.CustomControls;
using BroomService_App.Helpers;
using BroomService_App.Models;
using BroomService_App.Repository;
using BroomService_App.Resources;
using BroomService_App.Services.ApiService;
using BroomService_App.Services.DBService.LiteDB.ModelDB;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BroomService_App.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected static int CurrentUserId;
        protected static int CurrentUserType;
        
        protected HtmlToText htmlToText;

        public static int userTypeEnum;

        protected static bool IsNotificationRecieved;

        #region DB_Variables
        protected UserDataDbService userDataDbService;
        #endregion

        #region Constructor
        public BaseViewModel(INavigation navigation)
        {
            httpClientBase = new HttpClientBase();
            webApiRestClient = new WebApiRestClient();
            htmlToText = new HtmlToText();

            userDataDbService = new UserDataDbService();
            IsNotificationRecieved = App.IsNotificationRecieved;
            if (navigation != null)
            {
                _navigation = navigation;
            }
        }
        #endregion

        #region Navigation Property
        public INavigation _navigation;
        #endregion

        #region HttpClientBase Property
        protected readonly HttpClientBase httpClientBase;
        protected readonly WebApiRestClient webApiRestClient;
        #endregion

        #region SetProperty
        public bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        } 
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region MenuIconCommand
        public Command MenuIconCommand
        {
            get
            {
                return new Command(() =>
                {
                    try
                    {
                        _navigation.PushAsync(new Pages.MenuListPage());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("MenuIconCommand_Exception:- " + ex.Message);
                    }
                });
            }
        }
        #endregion 

        #region BackIconCommand
        public Command BackIconCommand
        {
            get
            {
                return new Command(() =>
                {
                    try
                    {
                        _navigation.PopAsync();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("BackIconCommand_Exception:- " + ex.Message);
                    }
                });
            }
        }
        #endregion

        private byte[] myfile;

        #region Gallery/Camera command
        protected async Task<ImageSource> GalleryCommand()
        {
            try
            {
                MediaFile _mediaFile;
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await App.Current.MainPage.DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                    return null;
                }
                _mediaFile = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,

                });


                if (_mediaFile == null)
                    return null;


                using (var memoryStream = new MemoryStream())
                {
                    _mediaFile.GetStream().CopyTo(memoryStream);
                    myfile = memoryStream.ToArray();
                    _mediaFile.Dispose();
                }

                return ImageSource.FromStream(() => new MemoryStream(myfile));
                //ImagePicker = ImageSource.FromStream(() =>
                //{
                //    var stream = _mediaFile.GetStream();
                //    _mediaFile.Dispose();
                //    return stream;
                //});
            }
            catch (Exception ex)
            {
                Console.WriteLine("GalleryCommand_Exception:-->" + ex.Message);
                return null;
            }
        }

        protected async Task<ImageSource> CameraCommand()
        {
            try
            {
                MediaFile _mediaFile;
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await App.Current.MainPage.DisplayAlert("No Camera", ":( No camera available.", "OK");
                    return null;
                }

                _mediaFile = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Profile Photo",
                    SaveToAlbum = true,
                    PhotoSize = PhotoSize.Medium,
                    MaxWidthHeight = 2000,
                    DefaultCamera = CameraDevice.Rear
                });


                if (_mediaFile == null)
                    return null;

                //await App.Current.MainPage.DisplayAlert("File Location", file.Path, "OK");

                using (var memoryStream = new MemoryStream())
                {
                    _mediaFile.GetStream().CopyTo(memoryStream);
                    myfile = memoryStream.ToArray();
                    _mediaFile.Dispose();
                }
                
                return ImageSource.FromStream(() => new MemoryStream(myfile));
                //CameraPicker = ImageSource.FromStream(() =>
                // {
                //     var stream = _mediaFile.GetStream();
                //     _mediaFile.Dispose();
                //     return stream;
                // });
            }
            catch (Exception ex)
            {
                Console.WriteLine("CameraCommand_Exception:-->" + ex.Message);
                return null;
            }
        }
        #endregion


        /// <summary>
        /// Make first char of input to upper case
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FirstCharToUpper(string input)
        {
            switch (input)
            {
                case null: return null;
                case "": return "";
                default: return input.First().ToString().ToUpper() + input.Substring(1);
            }
        }

        /// <summary>
        /// Converting Stream into byte array
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms)
;
                return ms.ToArray();
            }
        }

        public static string IsImagesValid(string input, string apiImageBaseUrl)
        {
            if (!string.IsNullOrEmpty(input) && !string.IsNullOrWhiteSpace(input))
            {
                if (!input.StartsWith("http"))
                {
                    return apiImageBaseUrl + input;
                }
                else
                {
                    return input;
                }
            }
            else
            {
                return "ic_profile.png";
            }
        }

        /// <summary>
        /// Checks the valid email.
        /// </summary>
        /// <returns><c>true</c>, if valid email was checked, <c>false</c> otherwise.</returns>
        /// <param name="Email">Email.</param>
        public static bool CheckValidEmail(string Email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(Email);
            if (match.Success)
                return true;
            else
                return false;
        }

        #region BadgeCount
        private string _BadgeCount;

        public string BadgeCount
        {
            get { return _BadgeCount; }
            set { SetProperty(ref _BadgeCount, value); }
        }
        #endregion

        /// <summary>
        /// converting date to time ago function
        /// </summary>
        /// <param name="theDate"></param>
        /// <returns></returns>
        public static string RelativeDate(DateTime dtEvent)
        {
            TimeSpan TS = DateTime.Now - dtEvent;
            int intYears = DateTime.Now.Year - dtEvent.Year;
            int intMonths = DateTime.Now.Month - dtEvent.Month;
            int intDays = DateTime.Now.Day - dtEvent.Day;
            int intHours = DateTime.Now.Hour - dtEvent.Hour;
            int intMinutes = DateTime.Now.Minute - dtEvent.Minute;
            int intSeconds = DateTime.Now.Second - dtEvent.Second;
            if (intYears > 0) return String.Format("{0} {1} " + AppResource.ago, intYears, (intYears == 1) ? AppResource.year : AppResource.years);
            else if (intMonths > 0) return String.Format("{0} {1} " + AppResource.ago, intMonths, (intMonths == 1) ? AppResource.month : AppResource.months);
            else if (intDays > 0) return String.Format("{0} {1} " + AppResource.ago, intDays, (intDays == 1) ? AppResource.day : AppResource.days);
            else if (intHours > 0) return String.Format("{0} {1} " + AppResource.ago, intHours, (intHours == 1) ? AppResource.hour : AppResource.hours);
            else if (intMinutes > 0) return String.Format("{0} {1} " + AppResource.ago, intMinutes, (intMinutes == 1) ? AppResource.minute : AppResource.minutes);
            else if (intSeconds > 0) return String.Format("{0} {1} " + AppResource.ago, intSeconds, (intSeconds == 1) ? AppResource.second : AppResource.seconds);
            else
            {
                return String.Format("{0} {1} " + AppResource.ago, dtEvent.ToShortDateString(), dtEvent.ToShortTimeString());
            }
        }
    }
}
