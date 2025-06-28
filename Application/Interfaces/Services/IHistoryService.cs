using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Request.SessionHub;
using Application.Response;

namespace Application.Interfaces.Services
{
    public interface IHistoryService
    {
        
        Task<HttpResponseMessage> SlideChange(ChangeSlideRequest newSlide);
        Task<SlideStatsResponse> RecordAnswer(AnswerRequest answer);
    }
}
