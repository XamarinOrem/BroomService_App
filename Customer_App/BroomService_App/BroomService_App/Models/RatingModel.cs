using System;
using System.Collections.Generic;
using System.Text;

namespace BroomService_App.Models
{
    public class RatingModel
    {
        public int CustomerId { get; set; }
        public int ToUserId { get; set; }
        public int UserRating { get; set; }
        public string UserReview { get; set; }
        public int JobRating { get; set; }
        public string JobReview { get; set; }
        public int JobRequestId { get; set; }
    }

    public class RatingModelResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
}
