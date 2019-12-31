using System;
using System.Collections.Generic;
using System.Text;

namespace BroomService_App.Models
{
    public class ContactUsRequestModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }
    }
    public class ContactUsResponseModel
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
}
