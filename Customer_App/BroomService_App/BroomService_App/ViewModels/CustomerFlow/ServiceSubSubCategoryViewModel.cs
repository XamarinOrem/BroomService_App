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
using XF.Material.Forms.UI.Dialogs;

namespace BroomService_App.ViewModels.CustomerFlow
{
    public class ServiceSubSubCategoryViewModel : BaseViewModel
    {
        private double? price;
        private double? clientprice;

        private string languageculture;
        private string Propertytype;
        private long? Propertyid;
        private int SelectedCategoryId;
        private int SelectedSubCategoryid;
        private bool HasPrice;

        #region Popup Data
        #region CategoryInfoPicture
        private string _CategoryInfoPicture;

        public string CategoryInfoPicture
        {
            get { return _CategoryInfoPicture; }
            set { SetProperty(ref _CategoryInfoPicture, value); }
        }
        #endregion

        #region CategoryInfoName
        private string _CategoryInfoName;

        public string CategoryInfoName
        {
            get { return _CategoryInfoName; }
            set { SetProperty(ref _CategoryInfoName, value); }
        }
        #endregion

        #region CategoryInfoIcon
        private string _CategoryInfoIcon;

        public string CategoryInfoIcon
        {
            get { return _CategoryInfoIcon; }
            set { SetProperty(ref _CategoryInfoIcon, value); }
        }
        #endregion

        #region CategoryInfoDescription
        private string _CategoryInfoDescription;

        public string CategoryInfoDescription
        {
            get { return _CategoryInfoDescription; }
            set { SetProperty(ref _CategoryInfoDescription, value); }
        }
        #endregion

        #region IsInfoPopupVisible
        private bool _IsInfoPopupVisible;

        public bool IsInfoPopupVisible
        {
            get { return _IsInfoPopupVisible; }
            set { SetProperty(ref _IsInfoPopupVisible, value); }
        }
        #endregion

        #region PopupCloseCommand
        public Command PopupCloseCommand
        {
            get
            {
                return new Command((e) =>
                {
                    InfoPopup(false);
                });
            }
        }
        #endregion

        #region ExpandContractCommand
        public Command ExpandContractCommand
        {
            get
            {
                return new Command((e) =>
                {
                    try
                    {
                        SubSubCategoryInfo = (SubSubCategory)e;
                        InfoPopup(true, SubSubCategoryInfo);
                    }
                    catch (Exception ex)
                    {

                    }
                });
            }
        }
        #endregion

        private void InfoPopup(bool infovisible, SubSubCategory item = null)
        {
            IsInfoPopupVisible = infovisible;
            switch (infovisible)
            {
                case true:
                    CategoryInfoPicture = item.Picture;
                    CategoryInfoIcon = item.Icon;
                    CategoryInfoName = item.Name;
                    CategoryInfoDescription = item.Description;
                    break;
                case false:
                    CategoryInfoPicture = string.Empty;
                    CategoryInfoIcon = string.Empty;
                    CategoryInfoName = string.Empty;
                    CategoryInfoDescription = string.Empty;
                    break;
            }
        }

        #endregion

        #region Constructor
        public ServiceSubSubCategoryViewModel(INavigation navigation, string propertytype, long? propertyid, int selectedCategoryId, int selectedSubCategoryid, bool hasPrice) : base(navigation)
        {
            IsInfoPopupVisible = false;
            if (Xamarin.Forms.Application.Current.Properties.ContainsKey("AppLocale") && !string.IsNullOrEmpty(Xamarin.Forms.Application.Current.Properties["AppLocale"].ToString()))
            {
                languageculture = Xamarin.Forms.Application.Current.Properties["AppLocale"].ToString();
            }
            else
            {
                languageculture = "en-US";
            }
            Propertytype = propertytype;
            Propertyid = propertyid;
            SelectedCategoryId = selectedCategoryId;
            SelectedSubCategoryid = selectedSubCategoryid;
            HasPrice = hasPrice;
            if (HasPrice)
            {
                price = 0;
                clientprice = 0;
            }
            else
            {
                price = null;
                clientprice = null;
            }
            MessagingCenter.Subscribe<List<SubSubCategory>>(this, "SubSubcategoryList", (sender) =>
            {
                NewSubSubCategoryList = new ObservableCollection<SubSubCategory>();
                subsubcategorylist = new ObservableCollection<SubSubCategory>();
                SubSubCategorySelected = new List<int>();
                foreach (var item in sender)
                {

                    item.IsSelected = false;
                    item.IsSelectedImage = false;
                    item.Picture = item.Picture != null ? item.Picture.StartsWith("http") ? item.Picture : ApiHelpers.SubSubCategoryImageBaseUrl + item.Picture : "user_img.png";
                    item.Icon = item.Icon != null ? item.Icon.StartsWith("http") ? item.Icon : ApiHelpers.SubSubCategoryImageBaseUrl + item.Icon : "user_img.png";
                    //item.Icon = ApiHelpers.SubSubCategoryImageBaseUrl + item.Icon;

                    switch (languageculture)
                    {
                        case "en-US":
                            item.Name = item.Name;
                            item.Description = item.Description;
                            break;
                        case "fr-FR":
                            item.Name = item.Name_French;
                            item.Description = item.Description_French;
                            break;
                        case "he-IL":
                            item.Name = item.Name_Hebrew;
                            item.Description = item.Description_Hebrew;
                            break;
                        case "ru-RU":
                            item.Name = item.Name_Russian;
                            item.Description = item.Description_Russian;
                            break;
                    }

                    NewSubSubCategoryList.Add(item);
                }
                subsubcategorylist = NewSubSubCategoryList;
                MessagingCenter.Unsubscribe<List<SubSubCategory>>(this, "SubSubcategoryList");
            });

            //MessagingCenter.Subscribe<IEnumerable<object>>(this, "SubcategorySelected", async (sender) =>
            //{
            //    try
            //    {
            //        var data = StaticHelpers.ConvertintoObservable<SubSubCategory>(sender);

            //        if (data.Count > 0)
            //        {

            //            if (SubSubCategorySelected != null && SubSubCategorySelected.Count > 0)
            //            {
            //                try
            //                {
            //                    foreach (var item in SubSubCategorySelected)
            //                    {
            //                        var aa = data.Where(a => a.Id == item).FirstOrDefault();
            //                        if (aa == null)
            //                        {
            //                            var itemfromid = NewSubSubCategoryList.Where(a => a.Id == item).FirstOrDefault();

            //                            var index = NewSubSubCategoryList.IndexOf(itemfromid);
            //                            var indexselected = SubSubCategorySelected.IndexOf(item);

            //                            //SubSubCategoryList[index].SubCategorySelectedColor = StaticHelpers.WhiteColor;
            //                            //SubSubCategoryList[index].SubCategoryTextColor = StaticHelpers.Black1Color;
            //                            try
            //                            {
            //                                NewSubSubCategoryList[index].IsSelected = false;
            //                                NewSubSubCategoryList[index].IsSelectedImage = false;
            //                            }
            //                            catch (Exception ex)
            //                            {
            //                                await MaterialDialog.Instance.SnackbarAsync(message: "Unselected:-" + ex.Message,
            //                                               msDuration: 1000);
            //                            }

            //                            SubSubCategorySelected.RemoveAt(indexselected);
            //                            break;
            //                        }

            //                    }
            //                }
            //                catch (Exception ex)
            //                {

            //                }
            //            }

            //            foreach (var item in data)
            //            {
            //                if (!item.IsSelected)
            //                {
            //                    var _data = NewSubSubCategoryList.Where(a => a.Id == item.Id).FirstOrDefault();
            //                    var index = NewSubSubCategoryList.IndexOf(_data);

            //                    //SubSubCategoryList[index].SubCategorySelectedColor = StaticHelpers.BlueColor;
            //                    //SubSubCategoryList[index].SubCategoryTextColor = StaticHelpers.WhiteColor;
            //                    try
            //                    {
            //                        NewSubSubCategoryList[index].IsSelected = true;
            //                        NewSubSubCategoryList[index].IsSelectedImage = true;
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        await MaterialDialog.Instance.SnackbarAsync(message: "Selected:-" + ex.Message,
            //                                               msDuration: 1000);
            //                    }

            //                    //SubSubCategoryList.Insert(index, item);
            //                    SubSubCategorySelected.Add(item.Id);
            //                    if (HasPrice && item.Price != null && item.ClientPrice != null)
            //                    {
            //                        price = price + item.Price;
            //                        clientprice = clientprice + item.ClientPrice;
            //                    }
            //                    else
            //                    {
            //                        price = null;
            //                        clientprice = null;
            //                    }
            //                }
            //            }
            //        }
            //        else if (data.Count == 0)
            //        {
            //            if (SubSubCategorySelected != null && SubSubCategorySelected.Count > 0)
            //            {
            //                try
            //                {
            //                    foreach (var item in SubSubCategorySelected)
            //                    {
            //                        var aa = data.Where(a => a.Id == item).FirstOrDefault();
            //                        if (aa == null)
            //                        {
            //                            var itemfromid = NewSubSubCategoryList.Where(a => a.Id == item).FirstOrDefault();

            //                            var index = NewSubSubCategoryList.IndexOf(itemfromid);
            //                            var indexselected = SubSubCategorySelected.IndexOf(item);

            //                            //SubSubCategoryList[index].SubCategorySelectedColor = StaticHelpers.WhiteColor;
            //                            //SubSubCategoryList[index].SubCategoryTextColor = StaticHelpers.Black1Color;
            //                            try
            //                            {
            //                                NewSubSubCategoryList[index].IsSelected = false;
            //                                NewSubSubCategoryList[index].IsSelectedImage = false;
            //                            }
            //                            catch (Exception ex)
            //                            {
            //                                await MaterialDialog.Instance.SnackbarAsync(message: "Count 0, unselected:-" + ex.Message,
            //                                               msDuration: 1000);
            //                            }

            //                            SubSubCategorySelected.RemoveAt(indexselected);
            //                            break;
            //                        }
            //                    }
            //                }
            //                catch (Exception ex)
            //                {

            //                }
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        await MaterialDialog.Instance.SnackbarAsync(message: "Subscribe:-" + ex.Message,
            //                                               msDuration: 1000);
            //    }
            //});
        }
        #endregion

        #region NewSubSubCategoryList
        private ObservableCollection<SubSubCategory> subsubcategorylist = new ObservableCollection<SubSubCategory>();
        private ObservableCollection<SubSubCategory> _NewSubSubCategoryList = new ObservableCollection<SubSubCategory>();

        public ObservableCollection<SubSubCategory> NewSubSubCategoryList
        {
            get { return _NewSubSubCategoryList; }
            set { SetProperty(ref _NewSubSubCategoryList, value); }
        }
        #endregion

        #region SingleSubSubCategorySelected
        public SubSubCategory SubSubCategoryInfo = new SubSubCategory();
        private SubSubCategory _SingleSubSubCategorySelected = new SubSubCategory();

        public SubSubCategory SingleSubSubCategorySelected
        {
            get { return _SingleSubSubCategorySelected; }
            set
            {
                SetProperty(ref _SingleSubSubCategorySelected, value);
                if (SingleSubSubCategorySelected != null && !string.IsNullOrEmpty(SingleSubSubCategorySelected.Name))
                {
                    try
                    {
                        UserDialogs.Instance.ShowLoading("");
                        SubSubCategorySelected.Add(SingleSubSubCategorySelected.Id);
                        StaticHelpers.CustomNavigation(_navigation, new JobRequestPage(Propertytype, Propertyid, SelectedCategoryId, SelectedSubCategoryid, HasPrice, price, clientprice, SubSubCategorySelected));
                        UserDialogs.Instance.HideLoading();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("NextBtn_SubSubCategory_Exception:- " + ex.Message);
                        UserDialogs.Instance.HideLoading();
                    }
                }
            }
        }
#endregion

        #region SubSubCategorySelected List
        private List<int> _SubSubCategorySelected = new List<int>();

        public List<int> SubSubCategorySelected
        {
            get { return _SubSubCategorySelected; }
            set { SetProperty(ref _SubSubCategorySelected, value); }
        }
        #endregion

        #region NextCommand
        //public Command NextCommand
        //{
        //    get
        //    {
        //        return new Command(async () =>
        //        {
        //            try
        //            {
        //                UserDialogs.Instance.ShowLoading("");
        //                if (SubSubCategorySelected == null || SubSubCategorySelected.Count <= 0)
        //                {
        //                    await MaterialDialog.Instance.SnackbarAsync(message: AppResource.SubCategoryError,
        //                                        msDuration: 1000);
        //                    UserDialogs.Instance.HideLoading();
        //                    return;
        //                }
        //                StaticHelpers.CustomNavigation(_navigation, new JobRequestPage(Propertytype, Propertyid, SelectedCategoryId, SelectedSubCategoryid, HasPrice, price, clientprice, SubSubCategorySelected));
        //                UserDialogs.Instance.HideLoading();
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine("NextBtn_SubSubCategory_Exception:- " + ex.Message);
        //                UserDialogs.Instance.HideLoading();
        //            }
        //        });
        //    }
        //}
        #endregion
    }
}