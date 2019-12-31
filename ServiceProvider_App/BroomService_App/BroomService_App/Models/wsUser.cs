using BroomService_App.Pages;
using BroomService_App.Repository;
using BroomService_App.ViewModels; 
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms; 

namespace BroomService_App.Models
{
    
    public class User  
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public int DeviceId { get; set; }
        public string DeviceToken { get; set; }
        public string Email { get; set; }
        public int? GenderID { get; set; }
        public string Gender { get; set; }
        public DateTime? DOB { get; set; }
        public int? Age { get; set; }
        public string PicturePath { get; set; }
        public string ProfilePicture { get; set; }
        public int? UserLoginType { get; set; }
        public bool? IsHotListAdded { get; set; }
        public bool? IsBlocked { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int Requests { get; set; }
        public string  Latitude { get; set; }
        public string  Longitude { get; set; }
        public bool? IsOnline { get; set; }
        public double? Distance { get; set; }
        public decimal? ShownDistance { get { return Distance != null ? decimal.Round(Convert.ToDecimal(Distance)) : 0; } }
        public bool? IsShownDistance { get; set; }
    }
    public class UserViewModel : INotifyPropertyChanged
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public int DeviceId { get; set; }
        public string DeviceToken { get; set; }
        public string Email { get; set; }
        public int? GenderID { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }
        public string Age { get; set; }
        public string Image { get; set; } 
        public int? UserLoginType { get; set; }
        public bool IsHotListAdded { get; set; }
        public int Requests { get; set; }
        public bool HasMoreThen10 { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public bool? IsOnline { get; set; }
        public double? Distance { get; set; } 
        public string ShownDistance { get { return Distance != null ? decimal.Round(Convert.ToDecimal(Distance))+" km" : "0 km"; } }
        public bool? IsShownDistance { get; set; }
        public ImageSource ImageGender { get; set; }
        public bool IsNonAdmin { get; set; }
        public ImageSource HotUserImage { get; set; }
        public ImageSource LoginTypeImage { get; set; }
        public bool IsShownLoginTypeImage { get; set; }
        public bool IsShownBanner { get; set; }
        public string OnlineStatus { get; set; }
        public Color OnlineStatusColor { get { return IsOnline == true ? Color.Green : Color.Red; } }
        public bool isSelected { get; set; }
        public bool HasNoData { get; set; } = false;
        public bool HasData { get; set; } = true;

        private ImageSource _groupImage;
        public ImageSource GroupImage
        {
            get { return _groupImage; }
            set
            {
                _groupImage = value;

                OnPropertyChanged(); // Notify, that Title has been changed
            }
        }
        private bool  _isShownCell=true;
        public bool IsShownCell
        {
            get { return _isShownCell; }
            set
            {
                _isShownCell = value;

                OnPropertyChanged(); // Notify, that Title has been changed
            }
        }
        public bool? IsBlocked { get; set; }
        public DateTime? CreatedDate { get; set; }
        private int _unReadCount = 0;
        public int UnReadCount
        {
            get { return _unReadCount; }
            set
            {
                _unReadCount = value;

                OnPropertyChanged(); // Notify, that Title has been changed
            }
        }

        private bool _showChatCount = false;
        public bool ShowChatCount
        {
            get { return _showChatCount; }
            set
            {
                _showChatCount = value;

                OnPropertyChanged(); // Notify, that Title has been changed
            }
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class wsUser : wsBase
    { 
        public User userData { get; set; }
        public bool? IsGenderFilled { get; set; }
    } 
    public class postLoginData
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int DeviceId { get; set; }
        public string DeviceToken { get; set; }
    }
    public class postSocialLoginData
    {
        public string Name { get; set; }
        public string Email { get; set; } 
        public string SocialId { get; set; }
        public string Password { get; set; }
        public int DeviceId { get; set; }
        public string DeviceToken { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int LoginType { get; set; }
        public string PicturePath { get; set; }
        public string BgPicturePath { get; set; }
        public int GenderId { get; set; }
    }



     

    #region user profile Data
 

    
    public class wsProfile : wsBase
    { 
        public ProfileData ProfileData { get; set; }
    }
    public class ProfileData
    {
        public long? ProfileID { get; set; }
        public long? UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Age { get; set; }
        public string Height { get; set; }
        public int? InterestedIn { get; set; }
        public int? BloodGroup { get; set; }
        public string Ethinecity { get; set; }
        public string EyeColor { get; set; }
        public string HairColor { get; set; }
        public int? Smoking { get; set; }
        public int? Drinking { get; set; }
        public int? Relationship { get; set; }
        public string Hobbies { get; set; }
        public string Bio { get; set; }
        public int? GenderId { get; set; }
        public string Gender { get; set; }
        public string ProfilePicture { get; set; }
        public string BgPicture { get; set; }
        public string Country { get; set; }
        public long? CountryId { get; set; }
        public DateTime? DOB { get; set; }  
        public int? LoginType { get; set; }
        public bool? IsHotListAdded { get; set; }
        public bool? IsBlocked { get; set; }
        public bool? IsOnline { get; set; }
        public double? Distance { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int? Points { get; set; }
        public int? Viewers { get; set; }
        public int? ViewedProfilePoints { get; set; }
        public string Mins { get; set; }
    }
    #endregion

    public class postAddToHotList
    {
        public string FromUserId { get; set; }
        public string ToUserId { get; set; }
    }

    public class wsPeople : wsBase
    {
        public bool? adsAllowed { get; set; }
        public int? totalCount { get; set; }
        public List<User> userData { get; set; }
    }
    public class Filters
    {
        public int  fromAge { get; set; }
        public int  fromDistance { get; set; }
        public int toAge { get; set; }
        public int toDistance { get; set; }
        public int gender { get; set; }
        public int status { get; set; }
    }
   

    
}
