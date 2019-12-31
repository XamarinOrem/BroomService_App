using System;
using System.Collections.Generic;
using System.Text;

namespace BroomService_App.Models
{
    public class ForgotPasswordRequestModel
    {
        public string Email { get; set; }
    }

    public class ForgotPasswordResponseModel
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
}
