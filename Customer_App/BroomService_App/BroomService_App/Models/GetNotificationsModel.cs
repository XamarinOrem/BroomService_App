using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BroomService_App.Models
{
    public class GetNotificationsModel
    {
        public bool status { get; set; }
        public string message { get; set; }
        public List<GetNotifications> data { get; set; }
    }

    public class GetNotifications
    {
        public int? Id { get; set; }
        public int? JobRequestId { get; set; }
        public string Text { get; set; }
        public int? FromUserId { get; set; }
        public string FromUserName { get; set; }
        public string FromUserImage { get; set; }
        public int?  ToUserId { get; set; }
        public string ToUserName { get; set; }
        public string ToUserImage { get; set; }
        public int? NotificationStatus { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string QuotePrice { get; set; }

        [JsonIgnore]
        public string UserPic { get; set; }
        [JsonIgnore]
        public string UserNotificationTime { get; set; }
        [JsonIgnore]
        public bool? IsButtonVisible { get; set; }
    }

    public class ApplyJobRequestModel
    {
        public int UserId { get; set; }
        public int JobRequestId { get; set; }
        public string IsAccept { get; set; }
    }

    public class ApplyJobRequestResponseModel
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
}
