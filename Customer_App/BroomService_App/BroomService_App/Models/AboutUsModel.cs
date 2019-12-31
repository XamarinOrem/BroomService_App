using System;
using System.Collections.Generic;
using System.Text;

namespace BroomService_App.Models
{
    public class AboutUsResponseModel
    {
        public bool status { get; set; }
        public string message { get; set; }
        public AboutUsData AboutUsData { get; set; }
    }

    public class AboutUsData
    {
        public int AboutUsId { get; set; }
        public string Text { get; set; }
    }
}
