using Application.Interfaces.Services;
using Application.Request;
using Application.Response;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Template.Controllers
{
    [ApiController]
    [Route("participant")]
    public class ParticipantController : ControllerBase
    {
        private readonly IParticipantService _participantService;

        public ParticipantController(IParticipantService participantService)
        {
            _participantService = participantService;
        }

        /*
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
        */

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateParticipant([FromBody]CreateParticipantRequest request)
        {
            try
            {
                var result = await _participantService.CreateParticipant(request);
                return Ok("Usuario ingresado a la sesión"); // Devuelve 201 Created sin contenido
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
