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
        public int idSession { get; set; }
        public Guid? acces_code { get; set; }
        public required string description { get; set; }
        public int interation_count { get; set; }
        public required bool active_status { get; set; }
        public required int max_participants { get; set; }
        public required DateTime start_time { get; set; }
        public DateTime? end_time { get; set; }
        public required int presentation_id { get; set; }

    }
}
