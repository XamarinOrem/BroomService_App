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
using Xamarin.Forms.Maps;
using XF.Material.Forms.UI.Dialogs;

namespace BroomService_App.ViewModels.CustomerFlow
{
    public class UpdatePropertyViewModel : BaseViewModel
    {
        #region PropertyDataModel
        private PropertyDataModel _propertyDataModel = new PropertyDataModel();
        public PropertyDataModel PropertyDataModel
        {
            get { return _propertyDataModel; }
            set { SetProperty(ref _propertyDataModel, value); }
        }
        #endregion

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
        public UpdatePropertyViewModel(INavigation navigation, PropertyDataModel propertyDataModel) : base(navigation)
        {
            PropertyDataModel = propertyDataModel;
            if (Application.Current.Properties.ContainsKey("CurrentUserId"))
            {
                CurrentUserId = Convert.ToInt32(Application.Current.Properties["CurrentUserId"]);
            }

            // Short Term Apt
            if (PropertyDataModel.ShortTermApartment == true)
            {
                RadioYesImageAirbnb = "ic_cheked.png";
                RadioNoImageAirbnb = "ic_uncheked.png";
            }
            else if(PropertyDataModel.ShortTermApartment == false)
            {
                RadioNoImageAirbnb = "ic_cheked.png";
                RadioYesImageAirbnb = "ic_uncheked.png";
            }


            // Doorman
            if (PropertyDataModel.Doorman == true)
            {
                RadioYesImageDoorman = "ic_cheked.png";
                RadioNoImageDoorman = "ic_uncheked.png";
            }
            else if (PropertyDataModel.Doorman == false)
            {
                RadioNoImageDoorman = "ic_cheked.png";
                RadioYesImageDoorman = "ic_uncheked.png";
            }

            // Parking
            if (PropertyDataModel.Parking == true)
            {
                RadioYesImageParking = "ic_cheked.png";
                RadioNoImageParking = "ic_uncheked.png";
            }
            else if (PropertyDataModel.Parking == false)
            {
                RadioNoImageParking = "ic_cheked.png";
                RadioYesImageParking = "ic_uncheked.png";
            }

            // Balcony
            if (PropertyDataModel.Balcony == true)
            {
                RadioYesImageBalcony = "ic_cheked.png";
                RadioNoImageBalcony = "ic_uncheked.png";
            }
            else if (PropertyDataModel.Balcony == false)
            {
                RadioNoImageBalcony = "ic_cheked.png";
                RadioYesImageBalcony = "ic_uncheked.png";
            }

            // Dishwasher
            if (PropertyDataModel.Dishwasher == true)
            {
                RadioYesImageDishwasher = "ic_cheked.png";
                RadioNoImageDishwasher = "ic_uncheked.png";
            }
            else if (PropertyDataModel.Dishwasher == false)
            {
                RadioNoImageDishwasher = "ic_cheked.png";
                RadioYesImageDishwasher = "ic_uncheked.png";
            }

            // Pool
            if (PropertyDataModel.Pool == true)
            {
                RadioYesImagePool = "ic_cheked.png";
                RadioNoImagePool = "ic_uncheked.png";
            }
            else if (PropertyDataModel.Pool == false)
            {
                RadioNoImagePool = "ic_cheked.png";
                RadioYesImagePool = "ic_uncheked.png";
            }

            // Garden
            if (PropertyDataModel.Garden == true)
            {
                RadioYesImageGarden = "ic_cheked.png";
                RadioNoImageGarden = "ic_uncheked.png";
            }
            else if (PropertyDataModel.Garden == false)
            {
                RadioNoImageGarden = "ic_cheked.png";
                RadioYesImageGarden = "ic_uncheked.png";
            }


            // Elevator
            if (PropertyDataModel.Elevator == true)
            {
                RadioYesImageElevator = "ic_cheked.png";
                RadioNoImageElevator = "ic_uncheked.png";
            }
            else if (PropertyDataModel.Elevator == false)
            {
                RadioNoImageElevator = "ic_cheked.png";
                RadioYesImageElevator = "ic_uncheked.png";
            }

            // Property Type
            switch (PropertyDataModel.Type)
            {
                case "Apartment":
                    PropertyTypeSelectedIndexChanged = 0;
                    break;
                case "Villa":
                    PropertyTypeSelectedIndexChanged = 1;
                    break;
                case "Duplex":
                    PropertyTypeSelectedIndexChanged = 2;
                    break;
                case "Penthouse":
                    PropertyTypeSelectedIndexChanged = 3;
                    break;
            }

            QueenBedSelectedIndexChanged = PropertyDataModel.NoOfQueenBeds.Value - 1;
            SingleBedSelectedIndexChanged = PropertyDataModel.NoOfSingleBeds.Value - 1;
            DoubleBedSelectedIndexChanged = PropertyDataModel.NoOfDoubleBeds.Value - 1;
            SingleSophaBedSelectedIndexChanged = PropertyDataModel.NoOfSingleSofaBeds.Value - 1;
            DoubleSophaBedSelectedIndexChanged = PropertyDataModel.NoOfDoubleSofaBeds.Value - 1;
            
            switch (PropertyDataModel.DuvetSize)
            {
                case "Small":
                    DuvetSizeSelectedIndexChanged = 0;
                    break;
                case "Medium":
                    DuvetSizeSelectedIndexChanged = 1;
                    break;
                case "Large":
                    DuvetSizeSelectedIndexChanged = 2;
                    break;
                case null:
                    DuvetSizeSelectedIndexChanged = -1;
                    break;
            }


            MessagingCenter.Subscribe<string>(this, "IsNavigationFromRegisterPage", (sender) =>
            {
                if (sender.Equals("Register"))
                {
                    IsNavigationFromRegisterPage = true;
                }
                else
                {
                    IsNavigationFromRegisterPage = false;
                }
            });
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

        #region QueenBedPicker SelectedIndexChanged
        private int _QueenBedSelectedIndexChanged;

        public int QueenBedSelectedIndexChanged
        {
            get { return _QueenBedSelectedIndexChanged; }
            set { SetProperty(ref _QueenBedSelectedIndexChanged, value); }
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

        #region DoubleBedPicker SelectedIndexChanged
        private int _DoubleBedSelectedIndexChanged;

        public int DoubleBedSelectedIndexChanged
        {
            get { return _DoubleBedSelectedIndexChanged; }
            set { SetProperty(ref _DoubleBedSelectedIndexChanged, value); }
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

        #region SingleBedPicker SelectedIndexChanged
        private int _SingleBedSelectedIndexChanged;

        public int SingleBedSelectedIndexChanged
        {
            get { return _SingleBedSelectedIndexChanged; }
            set { SetProperty(ref _SingleBedSelectedIndexChanged, value); }
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

        #region SingleSophaBedPicker SelectedIndexChanged
        private int _SingleSophaBedSelectedIndexChanged;

        public int SingleSophaBedSelectedIndexChanged
        {
            get { return _SingleSophaBedSelectedIndexChanged; }
            set { SetProperty(ref _SingleSophaBedSelectedIndexChanged, value); }
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

        #region DoubleSophaBedPicker SelectedIndexChanged
        private int _DoubleSophaBedSelectedIndexChanged;

        public int DoubleSophaBedSelectedIndexChanged
        {
            get { return _DoubleSophaBedSelectedIndexChanged; }
            set { SetProperty(ref _DoubleSophaBedSelectedIndexChanged, value); }
        }
        #endregion

        #region DuvetSizeList Picker static value
        public List<string> _DuvetSizeList = new List<string> {
            "Small","Medium","Large"
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

        #region DuvetSizePicker SelectedIndexChanged
        private int _DuvetSizeSelectedIndexChanged;

        public int DuvetSizeSelectedIndexChanged
        {
            get { return _DuvetSizeSelectedIndexChanged; }
            set { SetProperty(ref _DuvetSizeSelectedIndexChanged, value); }
        }
        #endregion

        #region PropertyType Picker static value
        public List<string> _propertyTypeList = new List<string> {
            "Apartment","Villa","Duplex","Penthouse"
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

        #region PropertyTypePicker SelectedIndexChanged
        private int _PropertyTypeSelectedIndexChanged;

        public int PropertyTypeSelectedIndexChanged
        {
            get { return _PropertyTypeSelectedIndexChanged; }
            set { SetProperty(ref _PropertyTypeSelectedIndexChanged, value); }
        }
        #endregion

        #region CloseCommand
        public Command CloseCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (!IsNavigationFromRegisterPage)
                    {
                        await _navigation.PopAsync();
                    }
                    else
                    {
                        if (userDataDbService.IsUserDbPresentInDB())
                        {
                            var data = userDataDbService.ReadAllItems().FirstOrDefault();
                            BsonValue id = data.ID;
                            userDataDbService.DeleteItemFromDB(id, data);
                        }
                        App.Current.MainPage = new NavigationPage(new LoginPage());
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
                });
            }
        }
        #endregion

        #region UpdateCommand
        public Command UpdateCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {

                        if ((Connectivity.ConnectionProfiles.Contains(ConnectionProfile.WiFi) && Connectivity.NetworkAccess.Equals(NetworkAccess.Internet)) || (Connectivity.ConnectionProfiles.Contains(ConnectionProfile.Cellular) && Connectivity.NetworkAccess.Equals(NetworkAccess.Internet)))
                        {
                            if (!string.IsNullOrEmpty(PropertyDataModel.Name) && !string.IsNullOrEmpty(PropertyTypeListSelected) && PropertyDataModel.FloorNumber != null && PropertyDataModel.ApartmentNumber != null && !string.IsNullOrEmpty(PropertyDataModel.BuildingCode) && !string.IsNullOrEmpty(PropertyDataModel.Address))
                            {
                                //if (string.IsNullOrEmpty(BathroomCount))
                                //{
                                //    BathroomCount = "0";
                                //}
                                //if (string.IsNullOrEmpty(PillowCount))
                                //{
                                //    PillowCount = "0";
                                //}
                                //if (string.IsNullOrEmpty(DuvetsCount))
                                //{
                                //    DuvetsCount = "0";
                                //}
                                //await _navigation.PushPopupAsync(new LoaderPopup());
                                UserDialogs.Instance.ShowLoading("");

                                Geocoder gc = new Geocoder();
                                var locationdata = await gc.GetPositionsForAddressAsync(PropertyDataModel.Address);

                                PropertyDataModel.CreatedBy = CurrentUserId;
                                PropertyDataModel.ShortTermApartment = RadioYesImageAirbnb.Equals("ic_cheked.png") ? true : false;
                                PropertyDataModel.Balcony = RadioYesImageBalcony.Equals("ic_cheked.png") ? true : false;
                                PropertyDataModel.Dishwasher = RadioYesImageDishwasher.Equals("ic_cheked.png") ? true : false;
                                PropertyDataModel.Doorman = RadioYesImageDoorman.Equals("ic_cheked.png") ? true : false;
                                PropertyDataModel.Elevator = RadioYesImageElevator.Equals("ic_cheked.png") ? true : false;
                                PropertyDataModel.Garden = RadioYesImageGarden.Equals("ic_cheked.png") ? true : false;
                                PropertyDataModel.Parking = RadioYesImageParking.Equals("ic_cheked.png") ? true : false;
                                PropertyDataModel.Pool = RadioYesImagePool.Equals("ic_cheked.png") ? true : false;
                                PropertyDataModel.NoOfDoubleBeds = DoubleBedsListSelected;
                                PropertyDataModel.NoOfDoubleSofaBeds = DoubleSophaBedsListSelected;
                                PropertyDataModel.DuvetSize = DuvetSizeListSelected;
                                PropertyDataModel.NoOfQueenBeds = QueenBedsListSelected;
                                PropertyDataModel.NoOfSingleBeds = SingleBedsListSelected;
                                PropertyDataModel.NoOfSingleSofaBeds = SingleSophaBedsListSelected;
                                PropertyDataModel.Type = PropertyTypeListSelected;
                                PropertyDataModel.Latitude = locationdata.FirstOrDefault().Latitude;
                                PropertyDataModel.Longitude = locationdata.FirstOrDefault().Longitude;


                                PropertyResponse response;
                                try
                                {
                                    response = await webApiRestClient.PostAsync<PropertyDataModel, PropertyResponse>(ApiHelpers.AddUpdateProperty, PropertyDataModel, true);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("UpdatepropertyApi_Exception:- " + ex.Message);
                                    response = null;
                                }
                                if (response != null)
                                {
                                    if (response.status)
                                    {
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
                                UserDialogs.Instance.HideLoading();
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
                        Console.WriteLine("UpdateCommand_Exception:- " + ex.Message);
                        UserDialogs.Instance.HideLoading();
                    }
                });
            }
        }
        #endregion
    }
}
