using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Interfaces.Services;
using Application.Request;
using Application.Response;
using Application.UseCases;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace WebService.Controllers
{

    [ApiController]
    [Route("session")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpGet]
        public async Task<ActionResult<IList<Session>>> GetAllSessions()
        {
            var sessions = await _sessionService.GetAllSession();
            if (sessions == null || sessions.Count == 0)
            {
                return NotFound(new { message = "No se encontraron sesiones." });
            }
            return Ok(sessions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Session>> GetSession(int id)
        {
            var session = await _sessionService.GetSessionById(id);
            if (session == null)
            {
                return NotFound(new { message = "Sesión no encontrada." });
            }
            return Ok(session);
        }

        [HttpPost]
        [ProducesResponseType(typeof(SessionResponse), 201)]
        public async Task<IActionResult> CreateSession(SessionRequest request)
        {
            try
            {
                var result = await _sessionService.CreateSession(request);
                return Ok(result); // Devuelve 200 OK
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }



        [HttpPost("join")]
        //[Authorize]
        public async Task<IActionResult> UnirSession([FromBody] SessionRequest request)
        {
            try
            {
                /*
                // Obtener el ID del usuario desde el token (ya validado por middleware)
                string? userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
                {
                    return Unauthorized(new { message = "ID de usuario inválido en el token." });
                }
                */

                var session = await _sessionService.GetSessionById(request.idSession);
                if (session == null)
                {
                    return NotFound(new { message = "Sesión no encontrada." });
                }

                //session.idParticipant = userId;
                //await _sessionService.UpdateSession(session);

                //Cambiar Request Join
                //Buscar el participante 
                //Cambiar estado de participante


                return Ok(new { message = "Usuario agregado a la sesión correctamente." });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Error interno del servidor." });
            }
        }


        [HttpPost("/logout")]
        public async Task<IActionResult> LogoutSession()
        {
            HttpContext.Session.Clear(); // Elimina todos los datos de sesión
            
            // si se usa autenticación basada en cookies
            await HttpContext.SignOutAsync();

            return Ok(new { message = "Sesión finalizada correctamente." });
        }

    }
}
