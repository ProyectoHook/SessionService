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
        public Guid SessionId { get; set; }
        public int? acces_code { get; set; }
        public string description { get; set; }
        public int interation_count { get; set; }
        public  bool active_status { get; set; }
        public  int max_participants { get; set; }
        public  DateTime start_time { get; set; }
        public DateTime? end_time { get; set; }
        public  int presentation_id { get; set; }

    }
}
