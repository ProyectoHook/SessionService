using Application.Interfaces.Services;
using Microsoft.AspNetCore.SignalR;

namespace Template.Hubs
{
    public class PresentationHub : Hub
    {
        private readonly ISessionService _sessionService;

        public PresentationHub(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        // Este método se invoca desde el server cuando el presentador cambia de slide
        public async Task ChangeSlide(string sessionId, int slideIndex)
        {

            if (Guid.TryParse(sessionId, out Guid result))
            {
                await _sessionService.UpdateCurrentSlideBySessionId(result, slideIndex);

                // Solo deberias enviar el indice del slide activo a todos los participantes de esa sesión
                // Si tenes mas sesiones le cambias el slide a los demas
                // TENES QUE CREAR LOS GRUPOS Y AGREGAR AL PRESENTADOR Y A LOS PARTICIPANTES
                // await Clients.Group(sessionId).SendAsync("ReceiveSlide", slideIndex);

                //envia a TODOS (para control)
                await Clients.All.SendAsync("ReceiveSlide", slideIndex);
            }

        }

        // El participante se une a una sesión (grupo)
        public async Task JoinSession(string sessionId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, sessionId);
        }

        // El participante abandona la sesión (opcional)
        public async Task LeaveSession(string sessionId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, sessionId);
        }
    }

}
