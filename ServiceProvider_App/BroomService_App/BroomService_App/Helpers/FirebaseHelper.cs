using BroomService_App.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroomService_App.Helpers
{
    public static class FirebaseHelper
    {
        //FirebaseClient firebase = new FirebaseClient("https://takemewithyou-a245a.firebaseio.com/");
        static FirebaseClient firebase = new FirebaseClient("https://broomserviceapp-1cf32.firebaseio.com/");

//        static FirebaseDatabase database = FirebaseDatabase.getInstance();
//DatabaseReference ref = database.getReference("server/saving-data/fireblog/posts");

        //public async Task<List<ChatModel>> GetChatForPost(int postId)
        //{
        //    return (await firebase
        //      .Child("Chat").Child(postId.ToString())
        //      .OnceAsync<ChatModel>()).Select(item => new ChatModel
        //      {
        //          Message = item.Object.Message,
        //          SenderUserID = item.Object.SenderUserID,
        //          MessageTime = item.Object.MessageTime,
        //          ReceiverUserID = item.Object.ReceiverUserID,
        //          PostID = item.Object.PostID,
        //          ReceiverImage = item.Object.ReceiverImage,
        //          ImageUrl = item.Object.ImageUrl,
        //          IsImg = item.Object.IsImg,
        //          IsMsg = item.Object.IsMsg,
        //          SenderName = item.Object.SenderName,
        //          IsLocation = item.Object.IsLocation,
        //          Longitude = item.Object.Longitude,
        //          Latitude = item.Object.Latitude,
        //          FilePath = item.Object.FilePath,
        //          IsVoiceNote = item.Object.IsVoiceNote,
        //          VoiceNoteUrl = item.Object.VoiceNoteUrl,
        //          TotalAudioTimeout = item.Object.TotalAudioTimeout
        //      }).Where(a => a.PostID == postId).ToList();
        //}

        public static async Task<List<ChatDetailListModel>> GetChatForUserID(int senderUserId, int recieverUserId)
        {
            return (await firebase
              .Child("Chat").Child(senderUserId.ToString()).Child(recieverUserId.ToString())
              .OnceAsync<ChatDetailListModel>()).Select(item => new ChatDetailListModel
              {
                  IsSender = item.Object.IsSender,
                  RecieverUserId = item.Object.RecieverUserId,
                  SenderUserId = item.Object.SenderUserId,
                  UserMessage = item.Object.UserMessage,
                  UserMessageTime = item.Object.UserMessageTime,
                  TimeStamp = item.Object.TimeStamp
              }).OrderBy(x=>x.TimeStamp).ToList();
        }

        public static async Task<bool> AddChatMessage(ChatDetailListModel chatModel)
        {
            try
            {
                await firebase
                      .Child("Chat").Child(chatModel.SenderUserId.ToString()).Child(chatModel.RecieverUserId.ToString()).PostAsync(chatModel);
                var chatModel1 = new ChatDetailListModel()
                {
                    IsSender = !chatModel.IsSender,
                    RecieverUserId = chatModel.SenderUserId,
                    SenderUserId = chatModel.RecieverUserId,
                    UserMessage = chatModel.UserMessage,
                    UserMessageTime = chatModel.UserMessageTime,
                    TimeStamp = chatModel.TimeStamp
                };
                await firebase
                      .Child("Chat").Child(chatModel1.SenderUserId.ToString()).Child(chatModel1.RecieverUserId.ToString()).PostAsync(chatModel1);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("AddingChatToFirebase_Exception:- " + ex.Message);
                return false;
            }
        }

        //public async Task<string> StoreImages(Stream imageStream, string imageName)
        //{
        //    var stroageImage = await new FirebaseStorage("takemewithyou-a245a.appspot.com")
        //        .Child("ChatImages")
        //        .Child(imageName)
        //        .PutAsync(imageStream);
        //    string imgurl = stroageImage;
        //    return imgurl;
        //}

    }
}