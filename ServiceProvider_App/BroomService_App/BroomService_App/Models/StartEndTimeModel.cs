using System;
using System.Collections.Generic;
using System.Text;

namespace BroomService_App.Models
{
    public class UpdateTimerTimeModel
    {
        public long UserId { get; set; }
        public long JobRequestId { get; set; }
        public TimeSpan TimerTime { get; set; }
        public bool IsStart { get; set; }
    }
    public class CompleteJobRequestModel
    {
        public long UserId { get; set; }
        public long JobRequestId { get; set; }
    }
    public class TimerResponseModel
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
}
