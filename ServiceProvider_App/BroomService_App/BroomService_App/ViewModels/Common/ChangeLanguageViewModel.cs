using BroomService_App.Helpers;
using BroomService_App.Models;
using BroomService_App.Pages;
using BroomService_App.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using BroomService_App.Popups;

namespace BroomService_App.ViewModels
{
    public class ChangeLanguageViewModel : BaseViewModel
    {
        #region LanguageSelected
        private string _LanguageSelected;
        public string LanguageSelected
        {
            get { return _LanguageSelected; }
            set { SetProperty(ref _LanguageSelected, value); }
        }
        #endregion

        #region IsLanguagePopupVisible
        private bool _IsLanguagePopupVisible;
        public bool IsLanguagePopupVisible
        {
            get { return _IsLanguagePopupVisible; }
            set { SetProperty(ref _IsLanguagePopupVisible, value); }
        }
        #endregion

        #region IsAppAlreadyInstalled
        private bool _IsAppAlreadyInstalled;
        public bool IsAppAlreadyInstalled
        {
            get { return _IsAppAlreadyInstalled; }
            set { SetProperty(ref _IsAppAlreadyInstalled, value); }
        }
        #endregion

        #region ChangeLanguage Picker static value
        //public List<string> _ChangeLanguageList = new List<string> {
        //    AppResource.changelang_English,AppResource.changelang_Russian,AppResource.changelang_Hebrew,AppResource.changelang_French
        //};
        public List<LanguagesModel> AvailableLanguages = new List<LanguagesModel> {
            new LanguagesModel {
                LanguageFullName = AppResource.changelang_English, LanguageCultureName = "en-US"
            },
            new LanguagesModel {
                LanguageFullName = AppResource.changelang_Russian, LanguageCultureName = "ru-RU"
            },
            new LanguagesModel {
                LanguageFullName = AppResource.changelang_Hebrew, LanguageCultureName = "he-IL"
            },
            new LanguagesModel {
                LanguageFullName = AppResource.changelang_French, LanguageCultureName = "fr-FR"
            }
        };
        public List<LanguagesModel> ChangeLanguageList => AvailableLanguages;
        #endregion

        #region ChangeLanguageListSelected SelectedItem property
        private LanguagesModel _ChangeLanguageListSelected;
        public LanguagesModel ChangeLanguageListSelected
        {
            get => _ChangeLanguageListSelected;
            set
            {
                SetProperty(ref _ChangeLanguageListSelected, value);
                if (ChangeLanguageListSelected != null && ChangeLanguageListSelected.LanguageFullName != null && ChangeLanguageListSelected.LanguageCultureName != null)
                {
                    try
                    {
                        App.Setlanguage(ChangeLanguageListSelected.LanguageCultureName);
                        Application.Current.Properties["AppLocale"] = ChangeLanguageListSelected.LanguageCultureName;
                        Application.Current.SavePropertiesAsync();
                        if (IsAppAlreadyInstalled)
                        {
                            if (CurrentUserType == Convert.ToInt32(UserTypeEnum.ServiceProvider))
                            {
                                App.Current.MainPage = new NavigationPage(new Pages.ServiceProviderFlow.HomeTabbedPage());
                            }
                            else
                            {
                                App.Current.MainPage = new NavigationPage(new Pages.WorkerFlow.HomeTabbedPage());
                            }
                        }
                        else
                        {
                            App.Current.MainPage = new NavigationPage(new LoginPage());
                        }
                        Application.Current.Properties["IsAppAlreadyInstalled"] = true;
                        Application.Current.SavePropertiesAsync();
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }
        #endregion

        #region Constructor
        public ChangeLanguageViewModel(INavigation navigation, bool isAppAlreadyInstalled) : base(navigation)
        {
            IsAppAlreadyInstalled = isAppAlreadyInstalled;
            LanguageSelected = AppResource.changelang_PickerPlaceholder;

            MessagingCenter.Subscribe<string, string>(this, "LanguageSelected", (sender,arg1) =>
            {
                LanguageSelected = sender;
                try
                {
                    App.Setlanguage(arg1);
                    Application.Current.Properties["AppLocale"] = arg1;
                    Application.Current.SavePropertiesAsync();
                    if (IsAppAlreadyInstalled)
                    {
                        if (CurrentUserType == Convert.ToInt32(UserTypeEnum.ServiceProvider))
                        {
                            App.Current.MainPage = new NavigationPage(new Pages.ServiceProviderFlow.HomeTabbedPage());
                        }
                        else
                        {
                            App.Current.MainPage = new NavigationPage(new Pages.WorkerFlow.HomeTabbedPage());
                        }
                    }
                    else
                    {
                        App.Current.MainPage = new NavigationPage(new LoginPage());
                    }
                    Application.Current.Properties["IsAppAlreadyInstalled"] = true;
                    Application.Current.SavePropertiesAsync();
                }
                catch (Exception ex)
                {

                }
            });
            //if (Xamarin.Forms.Application.Current.Properties.ContainsKey("AppLocale") && !string.IsNullOrEmpty(Xamarin.Forms.Application.Current.Properties["AppLocale"].ToString()))
            //{
            //    var languageculture = Xamarin.Forms.Application.Current.Properties["AppLocale"].ToString();
            //    switch (languageculture)
            //    {
            //        case "en-US":
            //            LanguagePickerSelectedIndexChanged = 0;
            //            ChangeLanguageListSelected = null;
            //            break;
            //        case "ru-RU":
            //            LanguagePickerSelectedIndexChanged = 1;
            //            ChangeLanguageListSelected = null;
            //            break;
            //        case "he-IL":
            //            LanguagePickerSelectedIndexChanged = 2;
            //            ChangeLanguageListSelected = null;
            //            break;
            //        case "fr-FR":
            //            LanguagePickerSelectedIndexChanged = 3;
            //            ChangeLanguageListSelected = null;
            //            break;
            //    }
            //}
        }
        #endregion

        #region LanguagePopup
        public Command LanguagePopup
        {
            get
            {
                return new Command(() =>
                {
                    _navigation.PushPopupAsync(new LanguagePickerPopup());
                });
            }
        }
        #endregion

        #region CloseCommand
        public Command CloseCommand
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

                    }
                });
            }
        }
        #endregion

        //#region LanguagePicker SelectedIndexChanged
        //private int _LanguagePickerSelectedIndexChanged;

        //public int LanguagePickerSelectedIndexChanged
        //{
        //    get { return _LanguagePickerSelectedIndexChanged; }
        //    set { SetProperty(ref _LanguagePickerSelectedIndexChanged, value); }
        //}
        //#endregion
    }
}
