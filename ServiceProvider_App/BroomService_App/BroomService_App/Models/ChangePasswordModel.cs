using System;
using System.Collections.Generic;
using System.Text;

namespace BroomService_App.Models
{
    public class ChangePasswordRequestModel
    {
        public string userId { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }

    public class ChangePasswordResponseModel
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
}
