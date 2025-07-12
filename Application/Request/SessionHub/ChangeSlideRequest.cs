using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Request.SessionHub
{
    public class ChangeSlideRequest
    {
        public Guid SessionId {  get; set; }
        public int SlideIndex { get; set; }
        public int SlideId { get; set; }
        public string? Ask { get; set; } 
        public string? AnswerCorrect { get; set; }
        public List<string>? Options { get; set; }
    }
}
