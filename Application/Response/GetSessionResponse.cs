using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Response
{
    public class GetSessionResponse
    {
        public Guid idSession { get; set; }
        public string access_code { get; set; }
        public string url { get; set; }

        public int presentation_id { get; set; }
        public PresentationResponseDTO presentation { get; set; }

        //los que estan aca no se si se utilizan
        public required string description { get; set; }
        public int interation_count { get; set; }
        public required bool active_status { get; set; }
        public required int max_participants { get; set; }
        public required DateTime start_time { get; set; }
        public DateTime? end_time { get; set; }
        public int CurrentSlide { get; set; }

    }
}
