using System;
using System.Collections.Generic;
using System.Text;

namespace BroomService_App.Models
{
    public class QuotePriceModel
    {
        public long UserId { get; set; }
        public long JobRequestId { get; set; }
        public decimal QuotePrice { get; set; }
    }
    public class AcceptRejectQuoteModel
    {
        public long UserId { get; set; }
        public long JobRequestId { get; set; }
        public long NotificationId { get; set; }
        public bool IsAccept { get; set; }
    }
    public class QuotationResponseModel
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
}
