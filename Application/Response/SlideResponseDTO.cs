using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response
{
    public class SlideResponseDTO
    {
        public int IdSlide { get; set; }
        public int IdPresentation { get; set; }
        public string Title { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public int Position { get; set; }
        public string BackgroundColor { get; set; }
        public int? IdAsk { get; set; }
        public int IdContentType { get; set; }
        public string Content { get; set; }

        //public AskResponseDTO Ask { get; set; }
        //public ContentTypeResponseDTO ContentType { get; set; }

    }
}
