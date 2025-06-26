using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response
{
    public class CreateSessionResponse
    {
        public Guid SessionId { get; set; }
        public string acces_code { get; set; }
        public PresentationResponseDTO presentation { get; set; }
    }
}
