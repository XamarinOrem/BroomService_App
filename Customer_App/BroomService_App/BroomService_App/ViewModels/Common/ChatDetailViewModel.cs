using Acr.UserDialogs;
using BroomService_App.DependencyInterface;
using BroomService_App.Helpers;
using BroomService_App.Models;
using BroomService_App.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace BroomService_App.ViewModels
{
    public class ChatDetailViewModel : BaseViewModel
    {
        public int RecieverUserID;
        #region Constructor
        public ChatDetailViewModel(INavigation navigation) : base(navigation)
        {
            SubscribeMethod();
        }
        #endregion

        #region Messaging Center Subscribe event
        private void SubscribeMethod()
        {
            MessagingCenter.Subscribe<string, int>(this, "ChatDetailTitle", async (sender, arg1) =>
            {
                ChatUserNameTitle = sender;
                RecieverUserID = arg1;
                chatlistdetail = await FirebaseHelper.GetChatForUserID(CurrentUserId, recieverUserId: RecieverUserID);
                //var _sortedlist = chatlistdetail.OrderBy(x => x.TimeStamp).ToList();
                ChatDetailList = new ObservableCollection<ChatDetailListModel>(chatlistdetail);
                MessagingCenter.Send("", "ScrollToEnd");
                MessagingCenter.Unsubscribe<string, int>(this, "ChatDetailTitle");

                Device.StartTimer(TimeSpan.FromSeconds(1), callback: () =>
                {
                    UpdateChatFromFirebase();
                    return true;
                });
            });

            //var data = await FirebaseHelper.GetChatForUserID(CurrentUserId, 14);
        }
        #endregion

        private async void UpdateChatFromFirebase()
        {
            try
            {
                chatlistdetail = await FirebaseHelper.GetChatForUserID(CurrentUserId, recieverUserId: RecieverUserID);
                //var _sortedlist = chatlistdetail.OrderBy(x => x.TimeStamp).ToList();
                //chatlistdetail = _sortedlist;
                if (chatlistdetail.Count > ChatDetailList.Count)
                {
                    updatechatlistdetail = chatlistdetail.Skip(ChatDetailList.Count).ToList();
                    try
                    {
                        foreach (var item in updatechatlistdetail)
                        {
                            ChatDetailList.Add(item);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    MessagingCenter.Send("", "ScrollToEnd");
                }
            }
            catch (Exception ex)
            {

            }
        }

        #region ChatListView property
        private ObservableCollection<ChatDetailListModel> _chatDetailList = new ObservableCollection<ChatDetailListModel>();

        public ObservableCollection<ChatDetailListModel> ChatDetailList
        {
            get { return _chatDetailList; }
            set { SetProperty(ref _chatDetailList, value); }
        }

        private List<ChatDetailListModel> chatlistdetail = new List<ChatDetailListModel>();
        private List<ChatDetailListModel> updatechatlistdetail = new List<ChatDetailListModel>();
        #endregion

        #region ChatDetail Header Title
        private string _chatUserNameTitle;

        public string ChatUserNameTitle
        {
            get { return _chatUserNameTitle; }
            set { SetProperty(ref _chatUserNameTitle, value); }
        } 
        #endregion
        
        #region Message Entry Property
        private string _messageEntry;

        public string MessageEntry
        {
            get { return _messageEntry; }
            set { SetProperty(ref _messageEntry, value); }
        }
        #endregion

        #region SendMsg Command
        public Command SendMsgCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(MessageEntry) && !string.IsNullOrWhiteSpace(MessageEntry))
                        {
                            if ((Connectivity.ConnectionProfiles.Contains(ConnectionProfile.WiFi) && Connectivity.NetworkAccess.Equals(NetworkAccess.Internet)) || (Connectivity.ConnectionProfiles.Contains(ConnectionProfile.Cellular) && Connectivity.NetworkAccess.Equals(NetworkAccess.Internet)))
                            {
                                var item = new ChatDetailListModel()
                                {
                                    SenderUserId = CurrentUserId,
                                    RecieverUserId = RecieverUserID,
                                    UserMessage = MessageEntry,
                                    UserMessageTime = DateTime.Now.ToString("dd/MM/yyyy, hh:mm:ss tt"),
                                    IsSender = true,
                                    TimeStamp = DependencyService.Get<IGetTimeStamp>().TimeStamp()
                                };
                                var request = new ChatDetailModelApi()
                                {
                                    FromUserId = CurrentUserId,
                                    ToUserId = RecieverUserID
                                };
                                Chatdetailresponse response;
                                try
                                {
                                    response = await webApiRestClient.PostAsync<ChatDetailModelApi, Chatdetailresponse>(ApiHelpers.AddChatRequest, request, true);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("SendMsgApi_Exception:- " + ex.Message);
                                    response = null;
                                }
                                if (response != null)
                                {
                                    if (response.status)
                                    {
                                        //chatlistdetail.Add(item);
                                        //ChatDetailList = new ObservableCollection<ChatDetailListModel>(chatlistdetail);
                                        try
                                        {
                                            ChatDetailList.Add(item);
                                        }
                                        catch (Exception ex)
                                        {
                                        }
                                        MessagingCenter.Send("", "ScrollToEnd");
                                        MessageEntry = string.Empty;
                                        var data = await FirebaseHelper.AddChatMessage(item);
                                    }
                                    else if (response.message == null)
                                    {
                                        chatlistdetail.Add(item);
                                        ChatDetailList = new ObservableCollection<ChatDetailListModel>(chatlistdetail);
                                        MessagingCenter.Send("", "ScrollToEnd");
                                        MessageEntry = string.Empty;
                                        var data = await FirebaseHelper.AddChatMessage(item);
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
                            }
                            else
                            {
                                await MaterialDialog.Instance.SnackbarAsync(message: AppResource.NoInternetError,
                                                        msDuration: 1000);
                            } 
                        }

                    }
                    catch (Exception ex)
                    {
                        await MaterialDialog.Instance.SnackbarAsync(message: ex.Message,
                                                    msDuration: 1000);
                        Console.WriteLine("SendMsgCommand_Exception:- " + ex.Message);
                    }
                });
            }
        }
        #endregion
    }
}
