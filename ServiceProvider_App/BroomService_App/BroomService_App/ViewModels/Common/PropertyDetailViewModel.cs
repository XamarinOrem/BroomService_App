using Acr.UserDialogs;
using BroomService_App.Helpers;
using BroomService_App.Models;
using BroomService_App.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BroomService_App.ViewModels
{
    public class PropertyDetailViewModel : BaseViewModel
    {
        #region IsAccessProperty Field
        private bool _IsAccessProperty;

        public bool IsAccessProperty
        {
            get { return _IsAccessProperty; }
            set { SetProperty(ref _IsAccessProperty, value); }
        }
        #endregion

        #region IsPropertySize Field
        private bool _IsPropertySize;

        public bool IsPropertySize
        {
            get { return _IsPropertySize; }
            set { SetProperty(ref _IsPropertySize, value); }
        } 
        #endregion

        #region IsDuvetSize Field
        private bool _IsDuvetSize;

        public bool IsDuvetSize
        {
            get { return _IsDuvetSize; }
            set { SetProperty(ref _IsDuvetSize, value); }
        } 
        #endregion

        #region PropertyDataModel
        private PropertyDataModel _propertyDataModel = new PropertyDataModel();
        public PropertyDataModel PropertyDataModel
        {
            get { return _propertyDataModel; }
            set { SetProperty(ref _propertyDataModel, value); }
        } 
        #endregion

        #region Constructor
        public PropertyDetailViewModel(INavigation navigation, PropertyDataModel propertyTapCommand) : base(navigation)
        {
            PropertyDataModel = propertyTapCommand;

            if(!string.IsNullOrEmpty(PropertyDataModel.DuvetSize) && !string.IsNullOrWhiteSpace(PropertyDataModel.DuvetSize))
            {
                IsDuvetSize = true;
            }
            else
            {
                IsDuvetSize = false;
            }
            if (!string.IsNullOrEmpty(PropertyDataModel.AccessToProperty) && !string.IsNullOrWhiteSpace(PropertyDataModel.AccessToProperty))
            {
                IsAccessProperty = true;
            }
            else
            {
                IsAccessProperty = false;
            }
            if (!string.IsNullOrEmpty(PropertyDataModel.Size) && !string.IsNullOrWhiteSpace(PropertyDataModel.Size))
            {
                IsPropertySize = true;
            }
            else
            {
                IsPropertySize = false;
            }
        }
        #endregion
    }
}
