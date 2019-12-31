using System;
using System.Collections.Generic;
using System.Text;

namespace BroomService_App.Models
{
    public class CountryDetailModel
    {
        public bool status { get; set; }
        public string message { get; set; }
        public List<CountryDetailDataModel> Data { get; set; }
    }
    public class CountryDetailDataModel
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public object DisplayOrder { get; set; }
        public object IsActive { get; set; }
        public object CurrencyCode { get; set; }
        public string CountryCode { get; set; }
        public List<object> Users { get; set; }
    }
}
