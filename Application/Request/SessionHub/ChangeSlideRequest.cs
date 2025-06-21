using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Request.SessionHub
{
    public class ChangeSlideRequest
    {
        Guid SessionId {  get; set; }
        int SlideIndex { get; set; }
    }
}
