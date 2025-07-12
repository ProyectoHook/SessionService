using Application.Interfaces.Services;
using Microsoft.AspNetCore.SignalR;
using Application.Request.SessionHub;
using Application.Response;
using Azure.Core;
using Domain.Entities;

namespace Template.Hubs
{
    public class SessionHub : Hub
    {
        private readonly ISessionService _sessionService;
        private readonly IHistoryService _historyService;



        public SessionHub(ISessionService sessionService, IHistoryService historyService)
        {
            _sessionService = sessionService;
            _historyService = historyService;
        }

        //Cambiar slide
        public async Task ChangeSlide(string sessionId, ChangeSlideRequest slide)
        {
            if (Guid.TryParse(sessionId, out Guid result))
            {
                if (slide.Ask != null)
                {
                    var Response = await _historyService.SlideChange(slide);
                }
                var response = await _sessionService.UpdateCurrentSlide(result, slide.SlideIndex);
                await Clients.Group(sessionId).SendAsync("ReceiveSlide", slide.SlideIndex); 
            }
        }
        public async Task RaiseHand(string sessionId, string userId, string userName, bool status_btn)
        {
            if (Guid.TryParse(sessionId, out Guid session_result) && Guid.TryParse(userId, out Guid user_result))
            {
               await Clients.Group(sessionId).SendAsync("ChangeRaiseHandTail", userId,userName,status_btn);
            }
        }

        public async Task SubmitAnswer(string sessionId, AnswerRequest answer)
        {
            if (Guid.TryParse(sessionId, out Guid result))
            {
                // 1. Registrar la respuesta y obtener estadísticas
                SlideStatsResponse stats = await _historyService.RecordAnswer(answer);

               
                // 3. Enviar a TODOS los usuarios del grupo
                // (El frontend filtrará por rol)
                await Clients.Group(sessionId).SendAsync("UpdateStatistics", stats);
            }
        }

        // Este método se invoca desde el server cuando el presentador cambia de slide
        //public async Task ChangeSlide(string sessionId, int slideIndex)
        //{

        //    if (Guid.TryParse(sessionId, out Guid result))
        //    {
        //        await _sessionService.UpdateCurrentSlideBySessionId(result, slideIndex);

        //        // Solo deberias enviar el indice del slide activo a todos los participantes de esa sesión
        //        // Si tenes mas sesiones le cambias el slide a los demas
        //        // TENES QUE CREAR LOS GRUPOS Y AGREGAR AL PRESENTADOR Y A LOS PARTICIPANTES
        //        // await Clients.Group(sessionId).SendAsync("ReceiveSlide", slideIndex);

        //        //envia a TODOS (para control)
        //        await Clients.All.SendAsync("ReceiveSlide", slideIndex);
        //    }

        //}

        public async Task JoinSession(string sessionId)
        {
            if (Guid.TryParse(sessionId, out Guid result))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, sessionId);
            }
        }

        //Salir del grupo
        public async Task LeaveSession(string sessionId)
        {
            if (Guid.TryParse(sessionId, out Guid result))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, sessionId);
            }
        }

        public async Task EndSession(string sessionId)
        {
            if (Guid.TryParse(sessionId, out Guid result))
            {
                // Avisar a todos que la sesión se cerró
                await Clients.Group(sessionId).SendAsync("SessionClosed");
            }

        }

        public async Task SendMessageToGroup(int sessionId, string sender, string message)
        {
            await Clients.Group(sessionId.ToString())
                .SendAsync("ReceiveMessage", sender, message);
        }


    }

}
