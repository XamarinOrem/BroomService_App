using System;
using System.Collections.Generic;
using BroomService_App.Models;
using BroomService_App.Resources;
using Xamarin.Forms;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System.Linq;
using System.Collections.ObjectModel;
using BroomService_App.ViewModels;

namespace BroomService_App.Popups
{
    public partial class LanguagePickerPopup : PopupPage
    {
        BaseViewModel baseViewModel;
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

        public LanguagePickerPopup()
        {
            InitializeComponent();
            languageListView.ItemsSource = ChangeLanguageList;
            baseViewModel = new BaseViewModel(Navigation);
            this.BindingContext = this;
        }

        private LanguagesModel _LanguageSelected;
        public LanguagesModel LanguageSelected
        {
            get { return _LanguageSelected; }
            set
            {
                baseViewModel.SetProperty(ref _LanguageSelected, value);
                if(LanguageSelected != null && LanguageSelected.LanguageFullName != null)
                {
                    MessagingCenter.Send(LanguageSelected.LanguageFullName, "LanguageSelected", LanguageSelected.LanguageCultureName);
                    Navigation.PopPopupAsync();
                }
            }
        }



        private void LanguageListView_ItemSelected(object sender, SelectionChangedEventArgs e)
        {
            languageListView.SelectedItem = null;
            //var selecteditem = e.CurrentSelection.ToList();
            //var item = new ObservableCollection<LanguagesModel>(selecteditem);
            //if (item != null)
            //{
            //    //MessagingCenter.Send(item.FirstOrDefault().LanguageFullName, "LanguageSelected", item.LanguageCultureName);

            //}
            //Navigation.PopPopupAsync();
        }
    }
}
