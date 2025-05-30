using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response
{
    public class CreateSessionResponse
    {
        public Guid idSession { get; set; }
        public string access_code { get; set; }
        public string url { get; set; }
        public PresentationResponseDTO presentation { get; set; }

    }
}
