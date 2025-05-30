using Microsoft.AspNetCore.SignalR;

namespace Template.Hubs
{
  
    public class PresentationHub : Hub
    {
        // Este método se invoca desde el server cuando el presentador cambia de slide
        public async Task ChangeSlide(string sessionId, int slideIndex)
        {
            // Envía el indice del slide activo a todos los participantes de esa sesión
            //await Clients.Group(sessionId).SendAsync("ReceiveSlide", slideIndex);

            //envia a TODOS (para control)
            await Clients.All.SendAsync("ReceiveSlide", slideIndex);
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
