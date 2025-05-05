using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Request
{
    public class CreateSessionRequest
    {
        //public Guid acces_code { get; set; }
        public string description { get; set; }
        public int max_participants { get; set; }
        public int presentation_id { get; set; }

        public int user_id { get; set; }
    }
}
