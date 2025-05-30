using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response
{
    public class PresentationResponseDTO
    {
        public string title { get; set; }
        public bool activityStatus { get; set; }
        public DateTime modifiedAt { get; set; }
        public DateTime createdAt { get; set; }
        public Guid idUserCreat { get; set; }
        public List<SlideResponseDTO> Slides { get; set; }

    }
}
