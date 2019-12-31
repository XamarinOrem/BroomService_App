using Acr.UserDialogs;
using BroomService_App.Helpers;
using BroomService_App.Models;
using BroomService_App.Pages.CustomerFlow;
using BroomService_App.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace BroomService_App.ViewModels.CustomerFlow
{
    public class ServiceCategoryViewModel : BaseViewModel
    {
        private string languageculture;
        private string Propertytype;
        private long? Propertyid;
        public GetCategoryModel response;

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

        #region Constructor
        public ServiceCategoryViewModel(INavigation navigation, string propertytype, long? propertyid) : base(navigation)
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
            GetJobCategoryListApi();
        }
        #endregion

        #region GetJobCategoryList Api
        private async void GetJobCategoryListApi()
        {
            UserDialogs.Instance.ShowLoading("");

            try
            {
                response = await webApiRestClient.GetAsync<GetCategoryModel>(ApiHelpers.GetCategories, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetCategoryApi_Exception:- " + ex.Message);
                response = null;
            }
            if (response != null)
            {
                if (response.status)
                {
                    if (response.data.Count > 0 && response.data != null)
                    {
                        var categorylistdata = new List<Category>();
                        foreach (var item in response.data)
                        {
                            try
                            {
                                var categoryitem = new Category()
                                {
                                    HasPrice = item.HasPrice,
                                    Picture = item.Picture != null ? ApiHelpers.CategoryImageBaseUrl + item.Picture : "user_img.png",
                                    Icon = ApiHelpers.CategoryImageBaseUrl + item.Icon,
                                    Id = item.Id,
                                    SubCategories = item.SubCategories
                                };
                                switch (languageculture)
                                {
                                    case "en-US":
                                        categoryitem.Name = item.Name;
                                        categoryitem.Description = item.Description;
                                        break;
                                    case "fr-FR":
                                        categoryitem.Name = item.Name_French;
                                        categoryitem.Description = item.Description_French;
                                        break;
                                    case "he-IL":
                                        categoryitem.Name = item.Name_Hebrew;
                                        categoryitem.Description = item.Description_Hebrew;
                                        break;
                                    case "ru-RU":
                                        categoryitem.Name = item.Name_Russian;
                                        categoryitem.Description = item.Description_Russian;
                                        break;
                                }
                                categorylistdata.Add(categoryitem);
                                //categorylistdata.Add(new Category()
                                //{
                                //    HasPrice = item.HasPrice,
                                //    Picture = item.Picture != null ? ApiHelpers.CategoryImageBaseUrl + item.Picture : "user_img.png",
                                //    Icon = ApiHelpers.CategoryImageBaseUrl + item.Icon,
                                //    Id = item.Id,
                                //    Name = item.Name,
                                //    SubCategories = item.SubCategories
                                //});
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Exception on ServiceCategoryList::-->> "+ex.Message);
                            }
                            CategoryList = new ObservableCollection<Category>(categorylistdata);
                        }
                    }
                    else
                    {
                        await MaterialDialog.Instance.SnackbarAsync(message: response.message,
                                msDuration: 1000);
                    }
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
        #endregion

        #region CategoryList
        private ObservableCollection<Category> _CategoryList = new ObservableCollection<Category>();

        public ObservableCollection<Category> CategoryList
        {
            get { return _CategoryList; }
            set { SetProperty(ref _CategoryList, value); }
        }
        #endregion

        #region CategorySelected
        public Category CategoryInfo = new Category();
        private Category _CategorySelected = new Category();

        public Category CategorySelected
        {
            get { return _CategorySelected; }
            set
            {
                SetProperty(ref _CategorySelected, value);
                if (CategorySelected != null && !string.IsNullOrEmpty(CategorySelected.Name))
                {
                    UserDialogs.Instance.ShowLoading("");
                    StaticHelpers.CustomNavigation(_navigation, new ServiceSubCategoryPage(Propertytype, Propertyid, CategorySelected.Id, CategorySelected.HasPrice));
                    MessagingCenter.Send(CategorySelected.SubCategories, "SubcategoryList");
                    UserDialogs.Instance.HideLoading();
                }
            }
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
                        CategoryInfo = (Category)e;
                        InfoPopup(true, CategoryInfo);
                    }
                    catch (Exception ex)
                    {

                    }
                });
            }
        } 
        #endregion

        private void InfoPopup(bool infovisible, Category item = null)
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
    }
}