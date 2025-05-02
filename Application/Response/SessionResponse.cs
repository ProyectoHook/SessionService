using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response
{
    public class SessionResponse
    {
        public int idSession { get; set; }
        public Guid acces_code { get; set; }
        public Guid idParticipant { get; set; }
        public string description { get; set; }
        public int interation_count { get; set; }
        public bool active_status { get; set; }
        public int max_participants { get; set; }
        public DateTime start_time { get; set; }
        public DateTime? end_time { get; set; }
        public int presentation_id { get; set; }


    }
}
