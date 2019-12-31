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

namespace BroomService_App.ViewModels
{
    public class HomeViewModel:BaseViewModel
    {
        public static int? UserId;

        #region IsPropertyFound Property
        private bool _IsPropertyFound;

        public bool IsPropertyFound
        {
            get { return _IsPropertyFound; }
            set { SetProperty(ref _IsPropertyFound, value); }
        } 
        #endregion

        #region SearchBarText Property
        private string _SearchBarText;

        public string SearchBarText
        {
            get { return _SearchBarText; }
            set
            {
                SetProperty(ref _SearchBarText, value);
                try
                {
                    if (searchpropertylist != null && searchpropertylist.Count > 0)
                    {
                        if (string.IsNullOrEmpty(SearchBarText) || string.IsNullOrWhiteSpace(SearchBarText))
                        {
                            PropertyList = searchpropertylist;
                        }
                        else
                        {
                            PropertyList = new ObservableCollection<PropertyDataModel>(searchpropertylist.Where(x => x.Name.ToLower().StartsWith(SearchBarText.ToLower())).ToList());
                        }
                    }
                }
                catch (Exception)
                {
                    PropertyList = null;
                }
            }
        } 
        #endregion

        #region Constructor
        public HomeViewModel(INavigation navigation):base(navigation)
        {
            if (Application.Current.Properties.ContainsKey("CurrentUserId"))
            {
                UserId = CurrentUserId = Convert.ToInt32(Application.Current.Properties["CurrentUserId"]);
            }
            if (Application.Current.Properties.ContainsKey("CurrentUserType"))
            {
                CurrentUserType = Convert.ToInt32(Application.Current.Properties["CurrentUserType"]);
            }

            MessagingCenter.Subscribe<List<PropertyDataModel>>(this, "PropertyListUpdate", (sender) =>
            {
                try
                {
                    searchpropertylist = new ObservableCollection<PropertyDataModel>();
                    SearchBarText = string.Empty;
                    PropertyList = new ObservableCollection<PropertyDataModel>();
                    foreach (var propertydata in sender)
                    {
                        propertydata.Name = FirstCharToUpper(propertydata.Name);
                        propertydata.Address = FirstCharToUpper(propertydata.Address);
                        searchpropertylist.Add(propertydata);
                    }

                    PropertyList = StaticHelpers.ConvertintoObservable<PropertyDataModel>(searchpropertylist.OrderByDescending(x=>x.CreatedDate));
                }
                catch (Exception ex)
                {

                }
            });
        }
        #endregion

        #region PropertyList property
        private ObservableCollection<PropertyDataModel> searchpropertylist = new ObservableCollection<PropertyDataModel>();
        private ObservableCollection<PropertyDataModel> _propertyList = new ObservableCollection<PropertyDataModel>();
        
        public ObservableCollection<PropertyDataModel> PropertyList
        {
            get { return _propertyList; }
            set { SetProperty(ref _propertyList, value); }
        }
        #endregion

        #region Property Tap Command
        private PropertyDataModel _propertyTapCommand;

        public PropertyDataModel PropertyTapCommand
        {
            get { return _propertyTapCommand; }
            set
            {
                SetProperty(ref _propertyTapCommand, value);
            }
        }
        #endregion

        #region ViewDetailCommand
        public Command ViewDetailCommand
        {
            get
            {
                return new Command((e) =>
                {
                    try
                    {
                        UserDialogs.Instance.ShowLoading("");
                        var propertydetail = ((PropertyDataModel)e);
                        StaticHelpers.CustomNavigation(_navigation, new PropertyDetailPage(propertydetail));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("PropertyDetailCommand_Exception:- " + ex.Message);
                    }
                    finally
                    {
                        UserDialogs.Instance.HideLoading();
                    }
                });
            }
        } 
        #endregion
        
        #region AddPropertyCommand
        public Command AddPropertyCommand
        {
            get
            {
                return new Command(() =>
                {
                    try
                    {
                        UserDialogs.Instance.ShowLoading("");
                        StaticHelpers.CustomNavigation(_navigation, new AddPropertyPage());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("AddPropertyCommand_Exception:- " + ex.Message);
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
