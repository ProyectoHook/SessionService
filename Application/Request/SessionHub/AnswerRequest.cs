using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Request.SessionHub
{
    public class AnswerRequest
    {
        public Guid SessionId { get; set; }
        public int SlideId { get; set; }
        public Guid UserId { get; set; }
        public string Answer { get; set; }
    }
}
