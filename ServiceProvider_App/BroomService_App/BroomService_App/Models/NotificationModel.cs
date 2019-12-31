using System;
using System.Collections.Generic;
using System.Text;

namespace BroomService_App.Models
{
    public class NotificationModel
    {
        public int ID { get; set; }
        public string UserPic { get; set; }
        public string UserName { get; set; }
        public string UserMessage { get; set; }
        public string UserNotificationTime { get; set; }
        public bool IsButtonVisible { get; set; }
    }
}
