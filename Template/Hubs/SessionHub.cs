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
        public async Task ChangeSlide(int sessionId, int slideIndex)
        {
            var response = await _sessionService.UpdateCurrentSlide(sessionId, slideIndex);
            await Clients.Group(sessionId.ToString()).SendAsync("ChangeSlide", slideIndex);

        }

        public async Task JoinSession(string sessionId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, sessionId.ToString());
        }

        //Salir del grupo
        public async Task LeaveSession(int sessionId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, sessionId.ToString());
        }

        public async Task EndSession(int sessionId)
        {
            // Avisar a todos que la sesión se cerró
            await Clients.Group(sessionId.ToString()).SendAsync("SessionClosed");

        }

        public async Task SendMessageToGroup(int sessionId, string sender, string message)
        {
            await Clients.Group(sessionId.ToString())
                .SendAsync("ReceiveMessage", sender, message);
        }


    }

}
