using System;
using System.Collections.Generic;
using System.Text;

namespace BroomService_App.Models
{
    public class UpdateDeviceInfoModel
    {
        public int UserId { get; set; }
        public int DeviceId { get; set; }
        public string DeviceToken { get; set; }
    }
    public class UpdateDeviceInfoResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
}
