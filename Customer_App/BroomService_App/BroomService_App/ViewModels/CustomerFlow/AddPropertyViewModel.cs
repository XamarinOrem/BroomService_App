using Acr.UserDialogs;
using BroomService_App.Helpers;
using BroomService_App.Models;
using BroomService_App.Pages;
using BroomService_App.Resources;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Plugin.Geolocator;
using Xamarin.Forms.Maps;
using XF.Material.Forms.UI.Dialogs;

namespace BroomService_App.ViewModels
{
    public class AddPropertyViewModel : BaseViewModel
    {
        /// <summary>
        /// Public/Private Properties
        /// </summary>
        private bool IsNavigationFromRegisterPage;
        public static List<int> NumberData = new List<int> {
            1,2,3,4,5,6,7,8,9,10
        };

        #region PropertyName Property
        private string _propertyName;

        public string PropertyName
        {
            get { return _propertyName; }
            set { SetProperty(ref _propertyName, value); }
        }
        #endregion

        #region FloorNumber Property
        private string _floorNumber;

        public string FloorNumber
        {
            get { return _floorNumber; }
            set { SetProperty(ref _floorNumber, value); }
        }
        #endregion

        #region ApartmentNumber Property
        private string _apartmentNumber;

        public string ApartmentNumber
        {
            get { return _apartmentNumber; }
            set { SetProperty(ref _apartmentNumber, value); }
        }
        #endregion

        #region BuildingCode Property
        private string _buildingCode;

        public string BuildingCode
        {
            get { return _buildingCode; }
            set { SetProperty(ref _buildingCode, value); }
        }
        #endregion

        #region Address Property
        private string _address;

        public string Address
        {
            get { return _address; }
            set { SetProperty(ref _address, value); }
        }
        #endregion

        #region AccessProperty Property
        private string _accessProperty;

        public string AccessProperty
        {
            get { return _accessProperty; }
            set { SetProperty(ref _accessProperty, value); }
        }
        #endregion

        #region BathroomCount Property
        private string _BathroomCount;

        public string BathroomCount
        {
            get { return _BathroomCount; }
            set { SetProperty(ref _BathroomCount, value); }
        }
        #endregion

        #region ToiletsCount Property
        private string _ToiletsCount;

        public string ToiletsCount
        {
            get { return _ToiletsCount; }
            set { SetProperty(ref _ToiletsCount, value); }
        }
        #endregion

        #region BedroomCount Property
        private string _BedroomCount;

        public string BedroomCount
        {
            get { return _BedroomCount; }
            set { SetProperty(ref _BedroomCount, value); }
        }
        #endregion

        #region PropertySize Property
        private string _PropertySize;

        public string PropertySize
        {
            get { return _PropertySize; }
            set { SetProperty(ref _PropertySize, value); }
        }
        #endregion

        #region DuvetsCount Property
        private string _DuvetsCount;

        public string DuvetsCount
        {
            get { return _DuvetsCount; }
            set { SetProperty(ref _DuvetsCount, value); }
        }
        #endregion

        #region PillowCount Property
        private string _PillowCount;

        public string PillowCount
        {
            get { return _PillowCount; }
            set { SetProperty(ref _PillowCount, value); }
        }
        #endregion

        #region RadioYesImageAirbnb Property
        private string _radioYesImageAirbnb;

        public string RadioYesImageAirbnb
        {
            get { return _radioYesImageAirbnb; }
            set { SetProperty(ref _radioYesImageAirbnb, value); }
        }
        #endregion

        #region RadioNoImageAirbnb Property
        private string _radioNoImageAirbnb;

        public string RadioNoImageAirbnb
        {
            get { return _radioNoImageAirbnb; }
            set { SetProperty(ref _radioNoImageAirbnb, value); }
        }
        #endregion

        #region RadioYesImageDoorman Property
        private string _radioYesImageDoorman;

        public string RadioYesImageDoorman
        {
            get { return _radioYesImageDoorman; }
            set { SetProperty(ref _radioYesImageDoorman, value); }
        }
        #endregion

        #region RadioNoImageDoorman Property
        private string _radioNoImageDoorman;

        public string RadioNoImageDoorman
        {
            get { return _radioNoImageDoorman; }
            set { SetProperty(ref _radioNoImageDoorman, value); }
        }
        #endregion

        #region RadioYesImageParking Property
        private string _radioYesImageParking;

        public string RadioYesImageParking
        {
            get { return _radioYesImageParking; }
            set { SetProperty(ref _radioYesImageParking, value); }
        }
        #endregion

        #region RadioNoImageParking Property
        private string _radioNoImageParking;

        public string RadioNoImageParking
        {
            get { return _radioNoImageParking; }
            set { SetProperty(ref _radioNoImageParking, value); }
        }
        #endregion

        #region RadioYesImageBalcony Property
        private string _radioYesImageBalcony;

        public string RadioYesImageBalcony
        {
            get { return _radioYesImageBalcony; }
            set { SetProperty(ref _radioYesImageBalcony, value); }
        }
        #endregion

        #region RadioNoImageBalcony Property
        private string _radioNoImageBalcony;

        public string RadioNoImageBalcony
        {
            get { return _radioNoImageBalcony; }
            set { SetProperty(ref _radioNoImageBalcony, value); }
        }
        #endregion

        #region RadioYesImageDishwasher Property
        private string _radioYesImageDishwasher;

        public string RadioYesImageDishwasher
        {
            get { return _radioYesImageDishwasher; }
            set { SetProperty(ref _radioYesImageDishwasher, value); }
        }
        #endregion

        #region RadioNoImageDishwasher Property
        private string _radioNoImageDishwasher;

        public string RadioNoImageDishwasher
        {
            get { return _radioNoImageDishwasher; }
            set { SetProperty(ref _radioNoImageDishwasher, value); }
        }
        #endregion
        
        #region RadioYesImagePool Property
        private string _radioYesImagePool;

        public string RadioYesImagePool
        {
            get { return _radioYesImagePool; }
            set { SetProperty(ref _radioYesImagePool, value); }
        }
        #endregion

        #region RadioNoImagePool Property
        private string _radioNoImagePool;

        public string RadioNoImagePool
        {
            get { return _radioNoImagePool; }
            set { SetProperty(ref _radioNoImagePool, value); }
        }
        #endregion
        
        #region RadioYesImageGarden Property
        private string _radioYesImageGarden;

        public string RadioYesImageGarden
        {
            get { return _radioYesImageGarden; }
            set { SetProperty(ref _radioYesImageGarden, value); }
        }
        #endregion

        #region RadioNoImageGarden Property
        private string _radioNoImageGarden;

        public string RadioNoImageGarden
        {
            get { return _radioNoImageGarden; }
            set { SetProperty(ref _radioNoImageGarden, value); }
        }
        #endregion

        #region RadioYesImageElevator Property
        private string _radioYesImageElevator;

        public string RadioYesImageElevator
        {
            get { return _radioYesImageElevator; }
            set { SetProperty(ref _radioYesImageElevator, value); }
        }
        #endregion

        #region RadioNoImageElevator Property
        private string _radioNoImageElevator;

        public string RadioNoImageElevator
        {
            get { return _radioNoImageElevator; }
            set { SetProperty(ref _radioNoImageElevator, value); }
        }
        #endregion

        #region Constructor
        public AddPropertyViewModel(INavigation navigation):base(navigation)
        {
            try
            {
                if (Application.Current.Properties.ContainsKey("CurrentUserId"))
                {
                    CurrentUserId = Convert.ToInt32(Application.Current.Properties["CurrentUserId"]);
                }

                RadioYesImageAirbnb = "ic_cheked.png";
                RadioNoImageAirbnb = "ic_uncheked.png";

                RadioYesImageDoorman = "ic_cheked.png";
                RadioNoImageDoorman = "ic_uncheked.png";

                RadioYesImageParking = "ic_cheked.png";
                RadioNoImageParking = "ic_uncheked.png";

                RadioYesImageBalcony = "ic_cheked.png";
                RadioNoImageBalcony = "ic_uncheked.png";

                RadioYesImageDishwasher = "ic_cheked.png";
                RadioNoImageDishwasher = "ic_uncheked.png";

                RadioYesImagePool = "ic_cheked.png";
                RadioNoImagePool = "ic_uncheked.png";

                RadioYesImageGarden = "ic_cheked.png";
                RadioNoImageGarden = "ic_uncheked.png";

                RadioYesImageElevator = "ic_cheked.png";
                RadioNoImageElevator = "ic_uncheked.png";
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region QueenBedsList Picker static value
        public List<int> _QueenBedsList = NumberData;
        public List<int> QueenBedsList => _QueenBedsList;
        #endregion

        #region QueenBedsListSelected SelectedItem property
        private int _QueenBedsListSelected;
        public int QueenBedsListSelected
        {
            get => _QueenBedsListSelected;
            set { SetProperty(ref _QueenBedsListSelected, value); }
        }
        #endregion

        #region DoubleBedsList Picker static value
        public List<int> _DoubleBedsList = NumberData;
        public List<int> DoubleBedsList => _DoubleBedsList;
        #endregion

        #region DoubleBedsListSelected SelectedItem property
        private int _DoubleBedsListSelected;
        public int DoubleBedsListSelected
        {
            get => _DoubleBedsListSelected;
            set { SetProperty(ref _DoubleBedsListSelected, value); }
        }
        #endregion

        #region SingleBedsList Picker static value
        public List<int> _SingleBedsList = NumberData;
        public List<int> SingleBedsList => _SingleBedsList;
        #endregion

        #region SingleBedsListSelected SelectedItem property
        private int _SingleBedsListSelected;
        public int SingleBedsListSelected
        {
            get => _SingleBedsListSelected;
            set { SetProperty(ref _SingleBedsListSelected, value); }
        }
        #endregion

        #region SingleSophaBedsList Picker static value
        public List<int> _SingleSophaBedsList = NumberData;
        public List<int> SingleSophaBedsList => _SingleSophaBedsList;
        #endregion

        #region SingleSophaBedsListSelected SelectedItem property
        private int _SingleSophaBedsListSelected;
        public int SingleSophaBedsListSelected
        {
            get => _SingleSophaBedsListSelected;
            set { SetProperty(ref _SingleSophaBedsListSelected, value); }
        }
        #endregion

        #region DoubleSophaBedsList Picker static value
        public List<int> _DoubleSophaBedsList = NumberData;
        public List<int> DoubleSophaBedsList => _DoubleSophaBedsList;
        #endregion

        #region DoubleSophaBedsListSelected SelectedItem property
        private int _DoubleSophaBedsListSelected;
        public int DoubleSophaBedsListSelected
        {
            get => _DoubleSophaBedsListSelected;
            set { SetProperty(ref _DoubleSophaBedsListSelected, value); }
        }
        #endregion

        #region DuvetSizeList Picker static value
        public List<string> _DuvetSizeList = new List<string> {
            AppResource.propertyDuvetType_Small, AppResource.propertyDuvetType_Medium, AppResource.propertyDuvetType_Large
        };
        public List<string> DuvetSizeList => _DuvetSizeList;
        #endregion

        #region DuvetSizeListSelected SelectedItem property
        private string _DuvetSizeListSelected;
        public string DuvetSizeListSelected
        {
            get => _DuvetSizeListSelected;
            set { SetProperty(ref _DuvetSizeListSelected, value); }
        }
        #endregion

        #region PropertyType Picker static value
        public List<string> _propertyTypeList = new List<string> {
            AppResource.propertyType_Apartment,AppResource.propertyType_Villa,AppResource.propertyType_Duplex,AppResource.propertyType_Penthouse
        };
        public List<string> PropertyTypeList => _propertyTypeList;
        #endregion

        #region PropertyTypeListSelected SelectedItem property
        private string _PropertyTypeListSelected;
        public string PropertyTypeListSelected
        {
            get => _PropertyTypeListSelected;
            set { SetProperty(ref _PropertyTypeListSelected, value); }
        }
        #endregion

        #region CloseCommand
        public Command CloseCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        await _navigation.PopAsync();
                    }
                    catch (Exception)
                    {
                    }
                });
            }
        }
        #endregion

        #region AirbnbRadioCommand
        public Command AirbnbRadioCommand
        {
            get
            {
                return new Command((a) =>
                {
                    try
                    {
                        switch (a.ToString().ToLower())
                        {
                            case "yes":
                                RadioYesImageAirbnb = "ic_cheked.png";
                                RadioNoImageAirbnb = "ic_uncheked.png";
                                break;
                            case "no":
                                RadioNoImageAirbnb = "ic_cheked.png";
                                RadioYesImageAirbnb = "ic_uncheked.png";
                                break;
                        }
                    }
                    catch (Exception)
                    {
                    }
                });
            }
        }
        #endregion

        #region DoormanRadioCommand
        public Command DoormanRadioCommand
        {
            get
            {
                return new Command((a) =>
                {
                    try
                    {
                        switch (a.ToString().ToLower())
                        {
                            case "yes":
                                RadioYesImageDoorman = "ic_cheked.png";
                                RadioNoImageDoorman = "ic_uncheked.png";
                                break;
                            case "no":
                                RadioNoImageDoorman = "ic_cheked.png";
                                RadioYesImageDoorman = "ic_uncheked.png";
                                break;
                        }
                    }
                    catch (Exception)
                    {}
                });
            }
        }
        #endregion

        #region ParkingRadioCommand
        public Command ParkingRadioCommand
        {
            get
            {
                return new Command((a) =>
                {
                    try
                    {
                        switch (a.ToString().ToLower())
                        {
                            case "yes":
                                RadioYesImageParking = "ic_cheked.png";
                                RadioNoImageParking = "ic_uncheked.png";
                                break;
                            case "no":
                                RadioNoImageParking = "ic_cheked.png";
                                RadioYesImageParking = "ic_uncheked.png";
                                break;
                        }
                    }
                    catch (Exception)
                    {
                    }
                });
            }
        }
        #endregion

        #region BalconyRadioCommand
        public Command BalconyRadioCommand
        {
            get
            {
                return new Command((a) =>
                {
                    try
                    {
                        switch (a.ToString().ToLower())
                        {
                            case "yes":
                                RadioYesImageBalcony = "ic_cheked.png";
                                RadioNoImageBalcony = "ic_uncheked.png";
                                break;
                            case "no":
                                RadioNoImageBalcony = "ic_cheked.png";
                                RadioYesImageBalcony = "ic_uncheked.png";
                                break;
                        }
                    }
                    catch (Exception)
                    {
                    }
                });
            }
        }
        #endregion

        #region DishwasherRadioCommand
        public Command DishwasherRadioCommand
        {
            get
            {
                return new Command((a) =>
                {
                    try
                    {
                        switch (a.ToString().ToLower())
                        {
                            case "yes":
                                RadioYesImageDishwasher = "ic_cheked.png";
                                RadioNoImageDishwasher = "ic_uncheked.png";
                                break;
                            case "no":
                                RadioNoImageDishwasher = "ic_cheked.png";
                                RadioYesImageDishwasher = "ic_uncheked.png";
                                break;
                        }
                    }
                    catch (Exception)
                    {

                    }
                });
            }
        }
        #endregion

        #region PoolRadioCommand
        public Command PoolRadioCommand
        {
            get
            {
                return new Command((a) =>
                {
                    try
                    {
                        switch (a.ToString().ToLower())
                        {
                            case "yes":
                                RadioYesImagePool = "ic_cheked.png";
                                RadioNoImagePool = "ic_uncheked.png";
                                break;
                            case "no":
                                RadioNoImagePool = "ic_cheked.png";
                                RadioYesImagePool = "ic_uncheked.png";
                                break;
                        }
                    }
                    catch (Exception)
                    {

                    }
                });
            }
        }
        #endregion

        #region GardenRadioCommand
        public Command GardenRadioCommand
        {
            get
            {
                return new Command((a) =>
                {
                    try
                    {
                        switch (a.ToString().ToLower())
                        {
                            case "yes":
                                RadioYesImageGarden = "ic_cheked.png";
                                RadioNoImageGarden = "ic_uncheked.png";
                                break;
                            case "no":
                                RadioNoImageGarden = "ic_cheked.png";
                                RadioYesImageGarden = "ic_uncheked.png";
                                break;
                        }
                    }
                    catch (Exception)
                    {
                    }
                });
            }
        }
        #endregion

        #region ElevatorRadioCommand
        public Command ElevatorRadioCommand
        {
            get
            {
                return new Command((a) =>
                {
                    try
                    {
                        switch (a.ToString().ToLower())
                        {
                            case "yes":
                                RadioYesImageElevator = "ic_cheked.png";
                                RadioNoImageElevator = "ic_uncheked.png";
                                break;
                            case "no":
                                RadioNoImageElevator = "ic_cheked.png";
                                RadioYesImageElevator = "ic_uncheked.png";
                                break;
                        }
                    }
                    catch (Exception)
                    {
                    }
                });
            }
        }
        #endregion

        #region DoneCommand
        public Command DoneCommand
        {
            get
            {
                return new Command(async() =>
                {
                    try
                    {
                        
                        if ((Connectivity.ConnectionProfiles.Contains(ConnectionProfile.WiFi) && Connectivity.NetworkAccess.Equals(NetworkAccess.Internet)) || (Connectivity.ConnectionProfiles.Contains(ConnectionProfile.Cellular) && Connectivity.NetworkAccess.Equals(NetworkAccess.Internet)))
                        {
                            if (!string.IsNullOrEmpty(PropertyName) && !string.IsNullOrEmpty(PropertyTypeListSelected) && !string.IsNullOrEmpty(FloorNumber) && !string.IsNullOrEmpty(ApartmentNumber) && !string.IsNullOrEmpty(BuildingCode) && !string.IsNullOrEmpty(Address))
                            {
                                if (string.IsNullOrEmpty(BathroomCount))
                                {
                                    BathroomCount = "0";
                                }
                                if (string.IsNullOrEmpty(PillowCount))
                                {
                                    PillowCount = "0";
                                }
                                if (string.IsNullOrEmpty(DuvetsCount))
                                {
                                    DuvetsCount = "0";
                                }
                                if (string.IsNullOrEmpty(BedroomCount))
                                {
                                    BedroomCount = "0";
                                }
                                if (string.IsNullOrEmpty(ToiletsCount))
                                {
                                    ToiletsCount = "0";
                                }
                                //await _navigation.PushPopupAsync(new LoaderPopup());
                                UserDialogs.Instance.ShowLoading("");

                                Geocoder gc = new Geocoder();
                                var locationdata = await gc.GetPositionsForAddressAsync(Address);

                                var requestModel = new PropertyDataModel()
                                {
                                    CreatedBy = CurrentUserId,
                                    Name = FirstCharToUpper(PropertyName),
                                    Type = FirstCharToUpper(PropertyTypeListSelected),
                                    FloorNumber = int.Parse(FloorNumber),
                                    ApartmentNumber = int.Parse(ApartmentNumber),
                                    BuildingCode = FirstCharToUpper(BuildingCode),
                                    Address = Address,
                                    AccessToProperty = FirstCharToUpper(AccessProperty),
                                    ShortTermApartment = RadioYesImageAirbnb.Equals("ic_cheked.png") ? true : false,
                                    Balcony = RadioYesImageBalcony.Equals("ic_cheked.png") ? true : false,
                                    Dishwasher = RadioYesImageDishwasher.Equals("ic_cheked.png") ? true :false,
                                    Doorman = RadioYesImageDoorman.Equals("ic_cheked.png") ? true : false,
                                    Elevator = RadioYesImageElevator.Equals("ic_cheked.png") ? true :false,
                                    Garden = RadioYesImageGarden.Equals("ic_cheked.png") ? true : false,
                                    Parking = RadioYesImageParking.Equals("ic_cheked.png") ? true :false,
                                    Pool = RadioYesImagePool.Equals("ic_cheked.png") ? true : false,
                                    NoOfDoubleBeds = DoubleBedsListSelected,
                                    NoOfDoubleSofaBeds = DoubleSophaBedsListSelected,
                                    DuvetSize = FirstCharToUpper(DuvetSizeListSelected),
                                    NoOfQueenBeds = QueenBedsListSelected,
                                    NoOfSingleBeds = SingleBedsListSelected,
                                    NoOfSingleSofaBeds = SingleSophaBedsListSelected,
                                    NoOfBathrooms = int.Parse(BathroomCount),
                                    NoOfPillows = int.Parse(PillowCount),
                                    NoOfDuvet = int.Parse(DuvetsCount),
                                    NoOfBedRooms = int.Parse(BedroomCount),
                                    NoOfToilets = int.Parse(ToiletsCount),
                                    Size = PropertySize,
                                    Latitude = locationdata.FirstOrDefault().Latitude,
                                    Longitude = locationdata.FirstOrDefault().Longitude
                                };

                                PropertyResponse response;
                                try
                                {
                                    response = await webApiRestClient.PostAsync<PropertyDataModel, PropertyResponse>(ApiHelpers.AddUpdateProperty, requestModel,true);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("AddpropertyApi_Exception:- " + ex.Message);
                                    response = null;
                                }
                                if (response != null)
                                {
                                    if (response.status)
                                    {
                                        //if (IsNavigationFromRegisterPage)
                                        //{
                                        //    if (Application.Current.Properties.ContainsKey("CurrentUserId"))
                                        //    {
                                        //        CurrentUserId = Convert.ToInt32(Application.Current.Properties["CurrentUserId"]);
                                        //    }

                                        //    var deviceRequestmodel = new UpdateDeviceInfoModel()
                                        //    {
                                        //        DeviceId = Device.RuntimePlatform == Device.Android ? 1 : Device.RuntimePlatform == Device.iOS ? 2 : 0,
                                        //        UserId = CurrentUserId,
                                        //        DeviceToken = App.FirebaseToken
                                        //    };
                                        //    UpdateDeviceInfoResponse deviceInfoResponse;
                                        //    try
                                        //    {
                                        //        deviceInfoResponse = await webApiRestClient.PostAsync<UpdateDeviceInfoModel, UpdateDeviceInfoResponse>(ApiHelpers.UpdateDeviceInfo, deviceRequestmodel);
                                        //    }
                                        //    catch (Exception ex)
                                        //    {
                                        //        deviceInfoResponse = null;
                                        //    }
                                        //    if (deviceInfoResponse != null)
                                        //    {
                                        //        if (deviceInfoResponse.status)
                                        //        {
                                        //            await MaterialDialog.Instance.SnackbarAsync(message: AppResource.DeviceInfoupdated,
                                        //                msDuration: 1000);
                                        //        }
                                        //        else
                                        //        {
                                        //            await MaterialDialog.Instance.SnackbarAsync(message: deviceInfoResponse.message,
                                        //                msDuration: 1000);
                                        //        }
                                        //    }
                                        //    else
                                        //    {
                                        //        await MaterialDialog.Instance.SnackbarAsync(message: AppResource.ExceptionDeviceInfo,
                                        //                msDuration: 1000);
                                        //    } 
                                        //}

                                        await MaterialDialog.Instance.SnackbarAsync(message: response.message,
                                                    msDuration: 1000);
                                        App.Current.MainPage = new NavigationPage(new HomeTabPage());
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
                                await MaterialDialog.Instance.SnackbarAsync(message: AppResource.RequiredFieldError,
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
                        Console.WriteLine("DoneCommand_Exception:- " + ex.Message);
                        UserDialogs.Instance.HideLoading();
                    }
                    finally
                    {
                        UserDialogs.Instance.HideLoading();
                    }
                });
            }
        }
        #endregion
    }
}