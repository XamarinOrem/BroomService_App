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
using Xamarin.Essentials;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace BroomService_App.ViewModels
{
    public class ChatListViewModel:BaseViewModel
    {
        public List<ChatListModel> chatlistdata = new List<ChatListModel>();

        //#region IsChatAvailable
        //private bool _IsChatAvailable;

        //public bool IsChatAvailable
        //{
        //    get { return _IsChatAvailable; }
        //    set { SetProperty(ref _IsChatAvailable, value); }
        //} 
        //#endregion

        #region Constructor
        public ChatListViewModel(INavigation navigation):base(navigation)
        {
            GetLatestChat();
        }
        #endregion

        #region GetLatestChat List APi
        private async void GetLatestChat()
        {
            if (Connectivity.NetworkAccess.Equals(NetworkAccess.Internet) || Connectivity.ConnectionProfiles.Contains(ConnectionProfile.WiFi))
            {
                UserDialogs.Instance.ShowLoading("");
                ChatListResponseModel response;
                try
                {
                    response = await webApiRestClient.GetAsync<ChatListResponseModel>(string.Format(ApiHelpers.GetChat, CurrentUserId), true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ChatListApi_Exception:-" + ex.Message);
                    response = null;
                }
                if (response != null)
                {
                    if (response.status)
                    {
                        if (response.data != null && response.data.Count > 0)
                        {
                            //IsChatAvailable = true;
                            chatlistdata.Clear();
                            foreach (var item in response.data)
                            {
                                item.SenderUserId = CurrentUserId;
                                //item.UserMessage = AppResource.ChatUserMsg1;
                                item.UserPic = IsImagesValid(item.UserPic, ApiHelpers.ApiImageBaseUrl);
                                //item.UserMessageTime = AppResource.ChatUserMsgTime1;
                                chatlistdata.Add(item);
                            }
                            //chatlistdata.Reverse();
                            ChatList = new ObservableCollection<ChatListModel>(chatlistdata);
                        }
                        else
                        {
                            //IsChatAvailable = false;
                        }
                    }
                    else
                    {
                        await MaterialDialog.Instance.SnackbarAsync(message: response.message,
                                    msDuration: MaterialSnackbar.DurationShort);
                    }
                }
                else
                {
                    await MaterialDialog.Instance.SnackbarAsync(message: AppResource.ServerError,
                                    msDuration: MaterialSnackbar.DurationShort);
                }
                UserDialogs.Instance.HideLoading();
            }
            else
            {
                await MaterialDialog.Instance.SnackbarAsync(message: AppResource.NoInternetError,
                                        msDuration: MaterialSnackbar.DurationShort);
            }
        } 
        #endregion

        #region ChatListView property
        private ObservableCollection<ChatListModel> _chatList = new ObservableCollection<ChatListModel>();

        public ObservableCollection<ChatListModel> ChatList
        {
            get { return _chatList; }
            set { SetProperty(ref _chatList, value); }
        }
        #endregion

        #region SelectedUserChat Property
        private ChatListModel _selectedUserChat;

        public ChatListModel SelectedUserChat
        {
            get { return _selectedUserChat; }
            set
            {
                SetProperty(ref _selectedUserChat, value);
                if (SelectedUserChat != null)
                {
                    StaticHelpers.CustomNavigation(_navigation, new ChatDetailPage());
                    MessagingCenter.Send(SelectedUserChat.UserName, "ChatDetailTitle", SelectedUserChat.RecieverUserId);
                }
            }
        }
        #endregion

    }
}