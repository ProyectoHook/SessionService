using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Request;
using Application.Request.SessionHub;


namespace Application.Interfaces.Services
{
    public interface IHistoryServiceClient
    {
        Task<HttpResponseMessage> RegisterSlideChange(Guid sessionId,SlideSnapshotDto slide);
        Task<HttpResponseMessage> RecordAnswerHistory(AnswerRequest answer);
    }
}
