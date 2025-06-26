using Application.Interfaces.Services;
using Microsoft.AspNetCore.SignalR;

namespace Template.Hubs
{
    public class SessionHub : Hub
    {
        private readonly ISessionService _sessionService;

        public SessionHub(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        //Cambiar slide
        public async Task ChangeSlide(string sessionId, int slideIndex)
        {
            if (Guid.TryParse(sessionId, out Guid result))
            {
                var response = await _sessionService.UpdateCurrentSlide(result, slideIndex);
                await Clients.Group(sessionId).SendAsync("ReceiveSlide", slideIndex);
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
