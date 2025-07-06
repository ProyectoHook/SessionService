using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.Services;
using Application.Request;
using Application.Request.SessionHub;
using Application.Response;

namespace Application.UseCases
{
    public class HistoryService : IHistoryService
    {
        private readonly IHistoryServiceClient _historyServiceClient;
        private readonly IParticipantService _participantService;
        private readonly ISessionService _sessionService;
        public HistoryService(IHistoryServiceClient historyServiceClient, IParticipantService participantService, ISessionService sessionService)
        {
            _historyServiceClient = historyServiceClient;
            _participantService = participantService;
            _sessionService = sessionService;
        }

        public async Task<List<GetParticipantResponse>> SessionPaticipants(Guid sessionId)
        {
            var participants = await _participantService.GetAllParticipants();

            if (participants == null) return new List<GetParticipantResponse>();
            else
            {
                var sessionParticipants = participants
                    .Where(p => p.SessionId == sessionId && p.activityStatus == true)
                    .ToList();

                return sessionParticipants;
            }
        }

        public async Task<HttpResponseMessage> SlideChange(ChangeSlideRequest newSlide)
        {
            var participants = await SessionPaticipants(newSlide.SessionId);

            if (participants.Count == 0) return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent);
            var session = _sessionService.GetAllSessions()
                .Result
                .FirstOrDefault(s => s.SessionId == newSlide.SessionId);
            var userCreate = session.created_by;
            var presentationId = session.presentation_id;

            var slideSnaphot = new SlideSnapshotDto
            {

                SlideId = newSlide.SlideId,
                SlideIndex = newSlide.SlideIndex,
                Ask = newSlide.Ask,
                AnswerCorrect = newSlide.AnswerCorrect,
                Options = newSlide.Options,
                ConnectedUserIds = participants.Select(p => new ParticipantHistoryDto
                {
                    UserId = p.idUser,
                    Name = "Desconocido",
                }).ToList(),
                UserCreateId = userCreate,
                presentationId = presentationId
            };

            return await _historyServiceClient.RegisterSlideChange(newSlide.SessionId, slideSnaphot);

        }

        public async Task<SlideStatsResponse> RecordAnswer(AnswerRequest answer)
        {
            HttpResponseMessage response = await _historyServiceClient.RecordAnswerHistory(answer);

            SlideStatsResponse stats = await response.Content.ReadFromJsonAsync<SlideStatsResponse>();

            return stats;
        }


    }
}
