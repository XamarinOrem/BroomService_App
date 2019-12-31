using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BroomService_App.Models
{
    public class ChatListModel
    {
        public int SenderUserId { get; set; }
        [JsonProperty("UserId")]
        public int RecieverUserId { get; set; }
        [JsonProperty("PicturePath")]
        public string UserPic { get; set; }
        [JsonProperty("Name")]
        public string UserName { get; set; }
        //public string UserMessage { get; set; }
        //public string UserMessageTime { get; set; }
        //public ObservableCollection<ChatDetailListModel> UserChatDetails { get; set; }
    }

    public class ChatListResponseModel
    {
        public bool status { get; set; }
        public string message { get; set; }
        public List<ChatListModel> data { get; set; }
    }


    public class ChatDetailListModel
    {
        //[JsonProperty("FromUserId")]
        public int SenderUserId { get; set; }
        //[JsonProperty("ToUserId")]
        public int RecieverUserId { get; set; }
        //[JsonIgnore]
        public string UserMessage { get; set; }
        //[JsonIgnore]
        public string UserMessageTime { get; set; }
        //[JsonIgnore]
        public bool IsSender { get; set; }
        public object TimeStamp { get; set; }
    }

    public class ChatDetailModelApi
    {
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
    }

    public class Chatdetailresponse
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
}
