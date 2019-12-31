using System;
using System.Collections.Generic;
using System.Text;

namespace BroomService_App.Models
{
    public class RegisterRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CountryCode { get; set; }
        public string PhoneNumber { get; set; }
        public int CountryId { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Zipcode { get; set; }
        public int UserType { get; set; }
    }

    public class RegisterResponseModel
    {
        public bool status { get; set; }
        public string message { get; set; }
        public UserData userData { get; set; }
    }
}
