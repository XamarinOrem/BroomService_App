using BroomService_App.Models;
using BroomService_App.Pages;
using BroomService_App.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BroomService_App.ViewModels
{
    public class ChangeLanguageViewModel : BaseViewModel
    {
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
                            App.Current.MainPage = new NavigationPage(new HomeTabPage());
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

            //if (Xamarin.Forms.Application.Current.Properties.ContainsKey("AppLocale") && !string.IsNullOrEmpty(Xamarin.Forms.Application.Current.Properties["AppLocale"].ToString()))
            //{
            //    var languageculture = Xamarin.Forms.Application.Current.Properties["AppLocale"].ToString();
            //    switch (languageculture)
            //    {
            //        case "en-US":
            //            LanguagePickerSelectedIndexChanged = 0;
            //            break;
            //        case "ru-RU":
            //            LanguagePickerSelectedIndexChanged = 1;
            //            break;
            //        case "he-IL":
            //            LanguagePickerSelectedIndexChanged = 2;
            //            break;
            //        case "fr-FR":
            //            LanguagePickerSelectedIndexChanged = 3;
            //            break;
            //    }
            //}
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
