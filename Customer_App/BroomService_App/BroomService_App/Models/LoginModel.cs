using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BroomService_App.Models
{
    public class LoginRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponseModel
    {
        public bool status { get; set; }
        public string message { get; set; }
        public UserData userData { get; set; }
    }

    public class UserData
    {
        [JsonIgnore]
        public int ID { get; set; }

        public int UserId { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public string PhoneNumber { get; set; }
        public int? CountryId { get; set; }
        public string CountryCode { get; set; }
        public string Location { get; set; }
        public string Image { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public int? UserType { get; set; }
        public bool? IsActive { get; set; }
        public int? DeviceId { get; set; }
        public string DeviceToken { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string Country { get; set; }
        public string UserTypeText { get; set; }
        public string CountryName { get; set; }
        public string PicturePath { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Zipcode { get; set; }
    }

    public class LogoutModel
    {
        public int UserId { get; set; }
    }
    public class LogoutResponseModel
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
}