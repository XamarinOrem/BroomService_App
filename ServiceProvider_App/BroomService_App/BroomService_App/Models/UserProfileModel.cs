using System;
using System.Collections.Generic;
using System.Text;

namespace BroomService_App.Models
{
    public class UserProfileModel
    {
        public bool status { get; set; }
        public string message { get; set; }
        public UserData Data { get; set; }
    }
}
