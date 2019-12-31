using Acr.UserDialogs;
using BroomService_App.Helpers;
using BroomService_App.Models;
using BroomService_App.Pages;
using BroomService_App.Resources;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace BroomService_App.ViewModels.CustomerFlow
{
    public class JobRequestWebViewModel : BaseViewModel
    {
        private MultipartFormDataContent MultipartFormData;
        #region TermConditionCheck image
        private string _termConditionCheck;

        public string TermConditionCheck
        {
            get { return _termConditionCheck; }
            set { SetProperty(ref _termConditionCheck, value); }
        }
        #endregion

        #region Constructor
        public JobRequestWebViewModel(INavigation navigation, MultipartFormDataContent multipartFormData) : base(navigation)
        {
            MultipartFormData = multipartFormData;
            TermConditionCheck = "ic_uncheked.png";
        }
        #endregion

        #region TermConditionCheckCommand
        public Command TermConditionCheckCommand
        {
            get
            {
                return new Command(async() =>
                {
                    if (TermConditionCheck == "ic_uncheked.png")
                    {
                        TermConditionCheck = "ic_register_check.png";

                        UserDialogs.Instance.ShowLoading("");
                        JobRequestResponseModel response;
                        try
                        {
                            response = await webApiRestClient.PostAsync<MultipartFormDataContent, JobRequestResponseModel>(ApiHelpers.AddJobRequest, MultipartFormData, true);
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
                                await MaterialDialog.Instance.SnackbarAsync(message: response.message,
                                           msDuration: 1000);
                                App.Current.MainPage = new NavigationPage(new HomeTabPage());
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
                        UserDialogs.Instance.HideLoading();
                    }
                    //else if (TermConditionCheck == "ic_register_check.png")
                    //{
                    //    TermConditionCheck = "ic_uncheked.png";
                    //}
                });
            }
        }
        #endregion
    }
}
