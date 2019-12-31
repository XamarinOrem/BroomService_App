using Acr.UserDialogs;
using BroomService_App.Helpers;
using BroomService_App.Models;
using BroomService_App.Pages;
using BroomService_App.Pages.CustomerFlow;
using BroomService_App.Resources;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace BroomService_App.ViewModels
{
    public class JobRequestViewModel : BaseViewModel
    {
        private double? Price;
        private double? ClientPrice;

        public TermConditionResponseModel response;
        private long propertyId;
        private int SelectedCategoryId;
        private int SubCategorySelected;

        #region CheckListEntry Property
        private string _CheckListEntry;

        public string CheckListEntry
        {
            get { return _CheckListEntry; }
            set { SetProperty(ref _CheckListEntry, value); }
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

        #region Private Variables
        //private string ImagePicker1;
        //private MediaFile _mediaFile;
        #endregion

        #region Constructor
        public JobRequestViewModel(INavigation navigation, string propertytype, long propertyid, int selectedCategoryId, int selectedSubCategoryId, List<int> selectedSubSubCategoryId, bool hasPrice, double? price, double? clientprice) : base(navigation)
        {
            MessagingCenter.Subscribe<App, List<string>>((App)Xamarin.Forms.Application.Current, "ImagesSelected", (s, images) =>
            {
                var abc = images;
            });


            IsCheckList = false;
            IsAddButtonVisible = true;
            propertyId = propertyid;
            SelectedCategoryId = selectedCategoryId;
            SubCategorySelected = selectedSubCategoryId;
            SubSubCategorySelected = selectedSubSubCategoryId;
            Price = price;
            ClientPrice = clientprice;

            if(price!= null && price.HasValue && price.Value > 0)
            {
                IsNoPriceAvailable = false;
            }
            else
            {
                IsNoPriceAvailable = true;
            }

            selectedstartDateTime = selectedendDateTime = DateTime.Now.AddHours(+1);
            StartDateTimeValue = selectedstartDateTime.ToString("dd/MM/yyyy") + " at " + selectedstartDateTime.ToString("hh:mm tt");
            EndDateTimeValue = selectedendDateTime.ToString("dd/MM/yyyy") + " at " + selectedendDateTime.ToString("hh:mm tt");
            // Property Type
            switch (propertytype)
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

            //GetJobCategoryListApi();

            //MessagingCenter.Subscribe<IEnumerable<object>>(this, "SubcategorySelected", (sender) =>
            //{
            //    var data = StaticHelpers.ConvertintoObservable<SubCategory>(sender);

            //    if (data.Count > 0)
            //    {

            //        if (SubCategorySelected != null && SubCategorySelected.Count > 0)
            //        {
            //            try
            //            {
            //                foreach (var item in SubCategorySelected)
            //                {
            //                    var aa = data.Where(a => a.Id == item).FirstOrDefault();
            //                    if (aa == null)
            //                    {
            //                        var itemfromid = SubCategoryList.Where(a => a.Id == item).FirstOrDefault();

            //                        var index = SubCategoryList.IndexOf(itemfromid);
            //                        var indexselected = SubCategorySelected.IndexOf(item);

            //                        SubCategoryList[index].SubCategorySelectedColor = StaticHelpers.WhiteColor;
            //                        SubCategoryList[index].SubCategoryTextColor = StaticHelpers.Black1Color;
            //                        SubCategoryList[index].IsSelected = false;

            //                        SubCategorySelected.RemoveAt(indexselected);
            //                        break;
            //                    }

            //                }
            //            }
            //            catch (Exception ex)
            //            {

            //            }
            //        }

            //        foreach (var item in data)
            //        {
            //            if (!item.IsSelected)
            //            {
            //                var _data = SubCategoryList.Where(a => a.Id == item.Id).FirstOrDefault();
            //                var index = SubCategoryList.IndexOf(_data);

            //                SubCategoryList[index].SubCategorySelectedColor = StaticHelpers.BlueColor;
            //                SubCategoryList[index].SubCategoryTextColor = StaticHelpers.WhiteColor;
            //                SubCategoryList[index].IsSelected = true;

            //                //SubCategoryList.Insert(index, item);
            //                SubCategorySelected.Add(item.Id);
            //            }
            //        }
            //    }
            //    else if (data.Count == 0)
            //    {
            //        if (SubCategorySelected != null && SubCategorySelected.Count > 0)
            //        {
            //            try
            //            {
            //                foreach (var item in SubCategorySelected)
            //                {
            //                    var aa = data.Where(a => a.Id == item).FirstOrDefault();
            //                    if (aa == null)
            //                    {
            //                        var itemfromid = SubCategoryList.Where(a => a.Id == item).FirstOrDefault();

            //                        var index = SubCategoryList.IndexOf(itemfromid);
            //                        var indexselected = SubCategorySelected.IndexOf(item);

            //                        SubCategoryList[index].SubCategorySelectedColor = StaticHelpers.WhiteColor;
            //                        SubCategoryList[index].SubCategoryTextColor = StaticHelpers.Black1Color;
            //                        SubCategoryList[index].IsSelected = false;

            //                        SubCategorySelected.RemoveAt(indexselected);
            //                        break;
            //                    }
            //                }
            //            }
            //            catch (Exception ex)
            //            {

            //            }
            //        }
            //    }
            //});
        }
        #endregion

        #region JobDescription
        private string _JobDescription;

        public string JobDescription
        {
            get { return _JobDescription; }
            set { SetProperty(ref _JobDescription, value); }
        }
        #endregion

        #region IsCategorySelected
        private bool _IsCategorySelected;

        public bool IsCategorySelected
        {
            get { return _IsCategorySelected; }
            set { SetProperty(ref _IsCategorySelected, value); }
        }
        #endregion

        #region IsAddButtonVisible
        private bool _IsAddButtonVisible;

        public bool IsAddButtonVisible
        {
            get { return _IsAddButtonVisible; }
            set { SetProperty(ref _IsAddButtonVisible, value); }
        }
        #endregion

        #region IsNoPriceAvailable
        private bool _IsNoPriceAvailable;

        public bool IsNoPriceAvailable
        {
            get { return _IsNoPriceAvailable; }
            set { SetProperty(ref _IsNoPriceAvailable, value); }
        }
        #endregion

        #region SubCategorySelected
        private List<int> _SubSubCategorySelected = new List<int>();
        public List<int> SubSubCategorySelected
        {
            get { return _SubSubCategorySelected; }
            set { SetProperty(ref _SubSubCategorySelected, value); }
        }
        #endregion

        #region CloseCommand
        public Command CloseCommand
        {
            get
            {
                return new Command((e) =>
                {
                    try
                    {
                        var item = ((CheckListModel)e);
                        CheckList.Remove(item);
                        CheckListHeight = (50 * CheckList.Count) + 10;
                        if (CheckList == null || CheckList.Count == 0)
                        {
                            IsCheckList = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("SendCommand_Exception:- " + ex.Message);
                    }
                });
            }
        }
        #endregion

        #region AddCheckListCommand
        public Command AddCheckListCommand
        {
            get
            {
                return new Command(() =>
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(CheckListEntry) && !string.IsNullOrWhiteSpace(CheckListEntry))
                        {
                            CheckList.Add(new CheckListModel
                            {
                                CheckListValue = FirstCharToUpper(CheckListEntry)
                            });
                            IsCheckList = true;
                            CheckListEntry = string.Empty;
                            CheckListHeight = (50 * CheckList.Count) + 10;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("SendCommand_Exception:- " + ex.Message);
                    }
                });
            }
        }
        #endregion
        
        #region SendCommand
        public Command SendCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        if ((Connectivity.ConnectionProfiles.Contains(ConnectionProfile.WiFi) && Connectivity.NetworkAccess.Equals(NetworkAccess.Internet)) || (Connectivity.ConnectionProfiles.Contains(ConnectionProfile.Cellular) && Connectivity.NetworkAccess.Equals(NetworkAccess.Internet)))
                        {
                            if (selectedendDateTime > selectedstartDateTime)
                            {
                                var startdatetime = selectedstartDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                                var enddatetime = selectedendDateTime.ToString("yyyy-MM-dd HH:mm:ss");

                                string boundary = "---8d0f01e6b3b5dafaaadaad";
                                MultipartFormDataContent multipartContent = new MultipartFormDataContent(boundary);
                                multipartContent.Add(new StringContent(CurrentUserId.ToString()), "UserId");
                                multipartContent.Add(new StringContent(SelectedCategoryId.ToString()), "CategoryId");
                                multipartContent.Add(new StringContent(startdatetime), "JobStartDatetime");
                                multipartContent.Add(new StringContent(enddatetime), "JobEndDatetime");
                                multipartContent.Add(new StringContent(propertyId.ToString()), "PropertyId");

                                //multipartContent.Add(new StringContent(SubCategorySelected.Id.ToString()), "SubCategories");

                                multipartContent.Add(new StringContent(SubCategorySelected.ToString()), "SubCategoryId");

                                if (Price.HasValue && ClientPrice.HasValue)
                                {
                                    multipartContent.Add(new StringContent(Price.ToString()), "Price"); 
                                    multipartContent.Add(new StringContent(ClientPrice.ToString()), "ClientPrice"); 
                                    multipartContent.Add(new StringContent(true.ToString()), "HasPrice");
                                }
                                else
                                {
                                    multipartContent.Add(new StringContent(false.ToString()), "HasPrice");
                                }
                                try
                                {
                                    string subsubcategories;

                                    var ints = SubSubCategorySelected;
                                    var stringsArray = ints.Select(i => i.ToString()).ToArray();
                                    subsubcategories = string.Join(",", stringsArray);


                                    multipartContent.Add(new StringContent(subsubcategories.ToString()), "SubSubCategories");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("AddingSubSubCategory in Multipart_Exception:-" + ex.Message);
                                }
                                //multipartContent.Add(new StringContent(""), "Description");
                                //multipartContent.Add(new StringContent(UserPic), "PicturePath");
                                try
                                {
                                    if (CheckList != null && CheckList.Count > 0)
                                    {
                                        ArrayList paramList = new ArrayList();
                                        foreach (var item in CheckList)
                                        {
                                            paramList.Add(item.CheckListValue); 
                                        }
                                        string checklistcontents = JsonConvert.SerializeObject(paramList);

                                        multipartContent.Add(new StringContent(checklistcontents), "CheckList");
                                    }
                                    //else
                                    //{
                                    //    await MaterialDialog.Instance.SnackbarAsync(message: AppResource.CheckListError,
                                    //           msDuration: 1000);
                                    //    UserDialogs.Instance.HideLoading();
                                    //    return;
                                    //}
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Adding Checklist Exception:- " + ex.Message);
                                }


                                try
                                {
                                    if (IsNoPriceAvailable)
                                    {
                                        //if (!string.IsNullOrEmpty(JobDescription) && !string.IsNullOrWhiteSpace(JobDescription))
                                        //{

                                        //}
                                        //else
                                        //{
                                        //    await MaterialDialog.Instance.SnackbarAsync(message: AppResource.JobDescriptionError,
                                        //           msDuration: 1000);
                                        //    UserDialogs.Instance.HideLoading();
                                        //    return;
                                        //}

                                        multipartContent.Add(new StringContent(JobDescription), "Description");


                                        //if (ReferenceImagesList != null && ReferenceImagesList.Count > 0)
                                        //{

                                        //}
                                        //else
                                        //{
                                        //    await MaterialDialog.Instance.SnackbarAsync(message: AppResource.ReferenceImagesError,
                                        //           msDuration: 1000);
                                        //    UserDialogs.Instance.HideLoading();
                                        //    return;
                                        //}
                                        for (int i = 1; i <= ReferenceImagesList.Count; i++)
                                        {
                                            var item = ReferenceImagesList[i - 1];
                                            if (item.ImageBytes != null)
                                            {
                                                //StreamImageSource streamImageSource = (StreamImageSource)item.ReferenceImages;
                                                //System.Threading.CancellationToken cancellationToken = System.Threading.CancellationToken.None;
                                                //System.Threading.Tasks.Task<System.IO.Stream> task =
                                                //    streamImageSource.Stream(cancellationToken);
                                                //System.IO.Stream stream = task.Result;

                                                //bitmapData = ReadFully(stream);
                                                var fileContent = new ByteArrayContent(item.ImageBytes);

                                                fileContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/octet-stream");
                                                fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
                                                {
                                                    Name = string.Format("Image{0}", i),
                                                    FileName = string.Format("ReferenceImage{0}", i) + ".png",
                                                };

                                                multipartContent.Add(fileContent);
                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }

                                try
                                {
                                    response = await webApiRestClient.GetAsync<TermConditionResponseModel>(ApiHelpers.GetTermsConditionsApi);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Api_Exception:- " + ex.Message);
                                    response = null;
                                }
                                if (response != null)
                                {
                                    if (response.status)
                                    {
                                        StaticHelpers.CustomNavigation(_navigation, new JobRequestWebView(multipartContent,response.TermsConditionsData.TermsConditionText));
                                    }
                                    else
                                    {
                                        await MaterialDialog.Instance.SnackbarAsync(message: response.message,
                                                    msDuration: 1000);
                                        await _navigation.PopAsync();
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
                                await MaterialDialog.Instance.SnackbarAsync(message: AppResource.EndStartTimeError, msDuration: 1000);
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
                        UserDialogs.Instance.HideLoading();
                        Console.WriteLine("SendCommand_Exception:- " + ex.Message);
                    }
                    finally
                    {
                        UserDialogs.Instance.HideLoading();
                    }
                });
            }
        }
        #endregion

        #region DateTimePickerCommand
        public Command DateTimePickerCommand
        {
            get
            {
                return new Command((e) =>
                {
                    try
                    {
                        switch (e.ToString().ToLower())
                        {
                            case "start":
                                MessagingCenter.Send(e.ToString(), "DateTimePicker");
                                break;
                            case "end":
                                MessagingCenter.Send(e.ToString(), "DateTimePicker");
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("DateTimePickerCommand_Exception:- " + ex.Message);
                    }
                });
            }
        }
        #endregion

        #region CameraOptionCommand
        public Command CameraOptionCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        var action = await App.Current.MainPage.DisplayActionSheet("Add Photo", "Cancel", null, "Camera", "Gallery");
                        switch (action)
                        {
                            case "Camera":
                                var Camdata = await CameraCommand();
                                if (Camdata != null)
                                {
                                    ReferenceImagesList.Add(Camdata);
                                    if (ReferenceImagesList != null && ReferenceImagesList.Count > 0)
                                    {
                                        IsReferenceImagesVisible = true;
                                        IsAddButtonVisible = true;
                                    }
                                    else
                                    {
                                        IsReferenceImagesVisible = false;
                                        IsAddButtonVisible = true;
                                    }
                                }
                                else
                                {
                                    IsAddButtonVisible = true;
                                }
                                break;
                            case "Gallery":
                                var Galdata = await GalleryCommand();

                                if (Galdata != null)
                                {
                                    ReferenceImagesList.Add(Galdata);
                                    if (ReferenceImagesList != null && ReferenceImagesList.Count > 0)
                                    {
                                        IsReferenceImagesVisible = true;
                                        IsAddButtonVisible = true;
                                    }
                                    else
                                    {
                                        IsReferenceImagesVisible = false;
                                        IsAddButtonVisible = true;
                                    }
                                }
                                else
                                {
                                    IsAddButtonVisible = true;
                                }
                                //var data = await App.MultiMediaPickerService.PickPhotosAsync();
                                break;
                            default:
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("CameraOptionCommand_Exception:- " + ex.Message);
                    }
                });
            }
        }
        #endregion

        #region ReferenceImagesList Property
        private ObservableCollection<ReferenceImagesModel> _referenceImagesList = new ObservableCollection<ReferenceImagesModel>();

        public ObservableCollection<ReferenceImagesModel> ReferenceImagesList
        {
            get { return _referenceImagesList; }
            set { SetProperty(ref _referenceImagesList, value); }
        }
        #endregion

        #region IsReferenceImagesVisible property
        private bool _isReferenceImagesVisible;

        public bool IsReferenceImagesVisible
        {
            get { return _isReferenceImagesVisible; }
            set { SetProperty(ref _isReferenceImagesVisible, value); }
        }
        #endregion

        #region EndDateTimeValue property
        private string _enddateTimeValue;

        public string EndDateTimeValue
        {
            get { return _enddateTimeValue; }
            set { SetProperty(ref _enddateTimeValue, value); }
        }

        public DateTime selectedendDateTime;
        #endregion
        
        #region StartDateTimeValue property
        private string _startdateTimeValue;

        public string StartDateTimeValue
        {
            get { return _startdateTimeValue; }
            set { SetProperty(ref _startdateTimeValue, value); }
        }

        public DateTime selectedstartDateTime;
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
        private byte[] bitmapData;

        public int PropertyTypeSelectedIndexChanged
        {
            get { return _PropertyTypeSelectedIndexChanged; }
            set { SetProperty(ref _PropertyTypeSelectedIndexChanged, value); }
        }
        #endregion
    }
}
