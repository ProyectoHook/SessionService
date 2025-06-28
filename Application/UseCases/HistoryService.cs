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
        public HistoryService(IHistoryServiceClient historyServiceClient, IParticipantService participantService)
        {
            _historyServiceClient = historyServiceClient;
            _participantService = participantService;
        }

        public async Task<List<GetParticipantResponse>> SessionPaticipants(Guid sessionId)
        {
            var participants = await _participantService.GetAllParticipants();
            foreach(var i in participants)
            {
                Console.WriteLine(i.idUser);
                Console.WriteLine(i.SessionId);
                Console.WriteLine("hola");
            }
            if (participants == null) return new List<GetParticipantResponse>();
            else
            {
                var sessionParticipants = participants
                    .Where(p => p.SessionId == sessionId && p.activityStatus == true)
                    .ToList();
                foreach (var i in sessionParticipants)
                {
                    Console.WriteLine(i.idUser);
                    Console.WriteLine(i.SessionId);
                    Console.WriteLine("hola2");
                }
                return sessionParticipants;
            }
        }

        public async Task<HttpResponseMessage> SlideChange(ChangeSlideRequest newSlide)
        {
            var participants= await SessionPaticipants(newSlide.SessionId);
            foreach(var i in newSlide.Options)
            {
                Console.WriteLine(i);
            }
            var slideSnaphot = new SlideSnapshotDto
            {
                
                SlideId = newSlide.SlideId,
                SlideIndex = newSlide.SlideIndex,
                Ask = newSlide.Ask ?? null,
                AnswerCorrect = newSlide.AnswerCorrect ?? null,
                Options = newSlide.Options ?? null,
                ConnectedUserIds = participants.Select(p => new ParticipantHistoryDto
                {
                    UserId=p.idUser,
                    Name = "Desconocido",
                }).ToList()
            };

            return await _historyServiceClient.RegisterSlideChange(newSlide.SessionId,slideSnaphot);

        }

        public async Task<SlideStatsResponse> RecordAnswer(AnswerRequest answer)
        {
            HttpResponseMessage response = await _historyServiceClient.RecordAnswerHistory(answer);

            SlideStatsResponse stats = await response.Content.ReadFromJsonAsync<SlideStatsResponse>();

            return stats;
        }


    }
}
