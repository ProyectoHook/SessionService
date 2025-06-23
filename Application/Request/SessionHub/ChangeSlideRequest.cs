using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Request.SessionHub
{
    public class ChangeSlideRequest
    {
        public int SessionId {  get; set; }
        public int SlideIndex { get; set; }
    }
}
