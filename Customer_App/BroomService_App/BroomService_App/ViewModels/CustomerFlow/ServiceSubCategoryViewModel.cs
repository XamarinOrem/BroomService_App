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
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace BroomService_App.ViewModels.CustomerFlow
{
    public class ServiceSubCategoryViewModel : BaseViewModel
    {
        private string languageculture;
        private string Propertytype;
        private long? Propertyid;
        private int SelectedCategoryId;
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
                        SubCategoryInfo = (SubCategory)e;
                        InfoPopup(true, SubCategoryInfo);
                    }
                    catch (Exception ex)
                    {

                    }
                });
            }
        }
        #endregion

        private void InfoPopup(bool infovisible, SubCategory item = null)
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
        public ServiceSubCategoryViewModel(INavigation navigation, string propertytype, long? propertyid, int selectedcategoryid, bool hasPrice) : base(navigation)
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
            SelectedCategoryId = selectedcategoryid;
            HasPrice = hasPrice;

            MessagingCenter.Subscribe<List<SubCategory>>(this, "SubcategoryList", (sender) =>
            {
                ListSubCategory.Clear();
                foreach (var item in sender)
                {
                    var subcategoryitem = new SubCategory()
                    {
                        Id = item.Id,                        
                        Picture = item.Picture != null ? item.Picture.StartsWith("http") ? item.Picture : ApiHelpers.SubCategoryImageBaseUrl + item.Picture : "user_img.png",
                        Icon = item.Icon != null ? item.Icon.StartsWith("http") ? item.Icon : ApiHelpers.SubCategoryImageBaseUrl + item.Icon : "user_img.png",
                        HasSubSubCategories = item.HasSubSubCategories,
                        Price = item.Price,
                        SubSubCategories = item.SubSubCategories,
                        ClientPrice = item.ClientPrice
                    };
                    switch (languageculture)
                    {
                        case "en-US":
                            subcategoryitem.Name = item.Name;
                            subcategoryitem.Description = item.Description;
                            break;
                        case "fr-FR":
                            subcategoryitem.Name = item.Name_French;
                            subcategoryitem.Description = item.Description_French;
                            break;
                        case "he-IL":
                            subcategoryitem.Name = item.Name_Hebrew;
                            subcategoryitem.Description = item.Description_Hebrew;
                            break;
                        case "ru-RU":
                            subcategoryitem.Name = item.Name_Russian;
                            subcategoryitem.Description = item.Description_Russian;
                            break;
                    }
                    ListSubCategory.Add(subcategoryitem);
                    //ListSubCategory.Add(new SubCategory()
                    //{
                    //    Id = item.Id,
                    //    Name = item.Name,
                    //    Picture = item.Picture != null ? item.Picture.StartsWith("http") ? item.Picture : ApiHelpers.SubCategoryImageBaseUrl + item.Picture : "user_img.png",
                    //    Icon = item.Icon != null ? item.Icon.StartsWith("http") ? item.Icon : ApiHelpers.SubCategoryImageBaseUrl + item.Icon : "user_img.png",
                    ////Icon = ApiHelpers.SubCategoryImageBaseUrl + item.Icon,
                    //    HasSubSubCategories = item.HasSubSubCategories,
                    //    Price = item.Price,
                    //    SubSubCategories = item.SubSubCategories,
                    //    ClientPrice = item.ClientPrice
                    //});
                    SubCategoryList = new ObservableCollection<SubCategory>(ListSubCategory);
                }
                MessagingCenter.Unsubscribe<Category>(this, "SubcategoryList");
            });

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

            //                        //SubCategoryList[index].SubCategorySelectedColor = StaticHelpers.WhiteColor;
            //                        //SubCategoryList[index].SubCategoryTextColor = StaticHelpers.Black1Color;
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

            //                //SubCategoryList[index].SubCategorySelectedColor = StaticHelpers.BlueColor;
            //                //SubCategoryList[index].SubCategoryTextColor = StaticHelpers.WhiteColor;
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

            //                        //SubCategoryList[index].SubCategorySelectedColor = StaticHelpers.WhiteColor;
            //                        //SubCategoryList[index].SubCategoryTextColor = StaticHelpers.Black1Color;
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

        #region SubCategoryList
        private List<SubCategory> ListSubCategory = new List<SubCategory>();
        private ObservableCollection<SubCategory> _SubCategoryList = new ObservableCollection<SubCategory>();

        public ObservableCollection<SubCategory> SubCategoryList
        {
            get { return _SubCategoryList; }
            set { SetProperty(ref _SubCategoryList, value); }
        }
        #endregion

        #region SubCategorySelected
        
        public SubCategory SubCategoryInfo = new SubCategory();
        private SubCategory _SubCategorySelected = new SubCategory();

        public SubCategory SubCategorySelected
        {
            get { return _SubCategorySelected; }
            set
            {
                SetProperty(ref _SubCategorySelected, value);
                if (SubCategorySelected != null && !string.IsNullOrEmpty(SubCategorySelected.Name))
                {
                    if (SubCategorySelected.SubSubCategories == null || SubCategorySelected.SubSubCategories.Count <=0)
                    {
                        StaticHelpers.CustomNavigation(_navigation, new JobRequestPage(Propertytype, Propertyid, SelectedCategoryId, SubCategorySelected.Id, HasPrice, SubCategorySelected.Price, SubCategorySelected.ClientPrice));
                    }
                    else
                    {
                        StaticHelpers.CustomNavigation(_navigation, new ServiceSubSubCategoryPage(Propertytype, Propertyid, SelectedCategoryId, SubCategorySelected.Id, HasPrice));
                        MessagingCenter.Send(SubCategorySelected.SubSubCategories, "SubSubcategoryList");
                    }
                }
            }
        }
        #endregion
    }
}
