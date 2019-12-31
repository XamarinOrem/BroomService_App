using System;
using System.Collections.Generic;
using System.Text;

namespace BroomService_App.Models
{
   public class wsBase
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
    public class wsUrl
    {
        public bool status { get; set; }
        public string url { get; set; }
    }
}

