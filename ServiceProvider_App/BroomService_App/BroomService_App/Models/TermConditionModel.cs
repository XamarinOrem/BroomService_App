using System;
using System.Collections.Generic;
using System.Text;

namespace BroomService_App.Models
{
    public class TermConditionResponseModel
    {
        public bool status { get; set; }
        public string message { get; set; }
        public TermsConditionsData TermsConditionsData { get; set; }
    }

    public class TermsConditionsData
    {
        public int Id { get; set; }
        public string TermsConditionText { get; set; }
    }
}
