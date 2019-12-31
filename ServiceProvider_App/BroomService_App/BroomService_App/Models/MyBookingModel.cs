using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BroomService_App.Models
{
    public class MyBookingModel
    {
        //New Changes
        public int Id { get; set; }
        public DateTime? JobStartDatetime { get; set; }
        public DateTime? JobEndDatetime { get; set; }
        public string Description { get; set; }
        public List<string> ReferenceImages { get; set; }
        public List<CheckList> CheckList { get; set; }
        public long PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyType { get; set; }
        public int? PropertyFloorNumber { get; set; }
        public int? PropertyApartmentNumber { get; set; }
        public string PropertyBuildingCode { get; set; }
        public string PropertyAddress { get; set; }
        public double? PropertyLatitude { get; set; }
        public double? PropertyLongitude { get; set; }
        public Category Category { get; set; }
        public SubCategory SubCategory { get; set; }
        public PropertyDataModel PropertyDataModel { get; set; }
        public List<SubSubCategory> SubSubCategories { get; set; }
        public string CustomerName { get; set; }
        public int? CustomerId { get; set; }
        public string CustomerImage { get; set; }
        public string ServiceProviderName { get; set; }
        public int? ServiceProviderId { get; set; }
        public string ServiceProviderImage { get; set; }
        public string ServiceProviderProfilePic { get; set; }
        public bool? IsShownQuote { get; set; }
        public bool? ForWorkers { get; set; }
        public int? JobStatus { get; set; }
        public string JobStatusStr { get; set; }
        public double? QuotePrice { get; set; }
        public double? CustomerQuotePrice { get; set; }
        public bool? IsQuoteApproved { get; set; }
        public DateTime? TimerStartTime { get; set; }
        public DateTime? TimerEndTime { get; set; }
        public int? UserRating { get; set; }
        public string UserReview { get; set; }
        public int? UserJobRating { get; set; }
        public string JobReview { get; set; }
        public int PaymentMethod { get; set; }
        public bool IsPaymentDone { get; set; }
        public int? AdminId { get; set; }

        [JsonIgnore]
        public string ServiceStartDate { get; set; }
        [JsonIgnore]
        public string ServiceStartTime { get; set; }
        [JsonIgnore]
        public string ServiceStartDateTime { get; set; }
        [JsonIgnore]
        public string ServiceEndDate { get; set; }
        [JsonIgnore]
        public string ServiceEndTime { get; set; }
        [JsonIgnore]
        public string ServiceEndDateTime { get; set; }
        [JsonIgnore]
        public string ServiceDetailStartDate { get; set; }
        [JsonIgnore]
        public string ServiceDetailEndDate { get; set; }

        [JsonIgnore]
        public bool IsNoJobStatusPending { get; set; }
        [JsonIgnore]
        public string CategoryName { get; set; }
    }

    public class MyBookingResponseModel
    {
        public bool status { get; set; }
        public string message { get; set; }
        public List<MyBookingModel> data { get; set; }
    }

    public class GetJobDetailByRequestIdModel
    {
        public bool status { get; set; }
        public string message { get; set; }
        public MyBookingModel data { get; set; }
    }
}
