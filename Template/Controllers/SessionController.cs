using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Exceptions;
using Application.Interfaces.Services;
using Application.Request;
using Application.Request.SessionHub;
using Application.Response;
using Application.UseCases;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using Template.Hubs;

namespace WebService.Controllers
{

    [ApiController]
    [Route("api/v1/session")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;
        private readonly IHubContext<SessionHub> _hubContext;

        public SessionController(ISessionService sessionService, IHubContext<SessionHub> hubContext)
        {
            _sessionService = sessionService;
            _hubContext = hubContext;
        }


        [HttpPost("create")]
        //[Authorize]
        [ProducesResponseType(typeof(CreateSessionResponse), 201)]
        public async Task<IActionResult> CreateSession(CreateSessionRequest request)
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


        //ingreso publico
        [AllowAnonymous]
        [HttpGet("join/{accessCode}")]
        public async Task<IActionResult> PublicJoin(string accessCode)
        {

            // Redireccionar al login del microservicio de user
            var returnUrl = Url.Action("PrivateJoin", "Session", new { accessCode }, Request.Scheme);

            //luego quitar hardcodeo
            var loginUrl = $"https://localhost:59542/api/Auth/login?returnUrl={Uri.EscapeDataString(returnUrl)}";

            return Redirect(loginUrl);

            //var returnUrl = await PrivateJoin(accessCode);
            ////luego quitar hardcodeo
            //var loginUrl = $"https://auth.myapp.com/login?returnUrl={returnUrl}";
            //return Redirect(loginUrl);
        }

        //ingreso privado
        //[Authorize]
        [HttpGet("join/private/{accessCode}")]
        public async Task<IActionResult> PrivateJoin(string accessCode, [FromQuery] bool json = false)
        {

            //obtiene user desde el JWT
            Guid userId;

            Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out userId);

            GetSessionResponse response = await _sessionService.GetSessionByAccessCode(accessCode);

            if (response == null)
                return StatusCode(404, "Sesion no encontrada");

            //agrega el participante
            var result = await _sessionService.Join(response.SessionId, userId);

            return StatusCode(200, response);
        }



        [HttpPost("logout/{id}")]
        //[Authorize]
        public async Task<IActionResult> LogoutSession(Guid id)
        {
            try 
            {
                var result = await _sessionService.EndSession(id);

                if (!result){
                    return BadRequest("No se pudo cerrar la sesión");
                }

                await _hubContext.Clients.Group(id.ToString()).SendAsync("SessionClosed");

            }
            catch (ExceptionNotFound ex) { return BadRequest(ex.Message); }
            catch (ExceptionBadRequest ex) { return BadRequest(ex.Message); }



            return Ok(new { message = "Sesión finalizada correctamente." });
        }
        

        [HttpGet("getAll")]
        //[Authorize]
        [ProducesResponseType(typeof(List<GetSessionResponse>), 200)]
        public async Task<ActionResult<GetSessionResponse>> GetAll()
        {
            try
            {
                var result = await _sessionService.GetAllSessions();
                return Ok(result); // Devuelve 200 OK
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{accessCode}")]
        [Authorize]
        [ProducesResponseType(typeof(GetSessionResponse), 200)]
        public async Task<ActionResult<GetSessionResponse>> GetById(string accessCode)
        {
            var response = new GetSessionResponse();
            try
            {
                var result = await _sessionService.GetSessionByAccessCode(accessCode);
                
                return Ok(result); // Devuelve 200 OK
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("change")]
        public async Task<IActionResult> ChangeSlide([FromBody] ChangeSlideRequest request)
        {
            try
            {
                await _sessionService.UpdateCurrentSlide(request.SessionId, request.SlideIndex);
                await _hubContext.Clients.Group(request.SessionId.ToString())
                                         .SendAsync("ChangeSlide", request.SlideIndex);

                return Ok(new { message = $"Slide cambiado a {request.SlideIndex} para la sesión {request.SessionId}" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Status()
        {
            await Task.CompletedTask;
            return StatusCode(200, "Session Service activo");
        }

    }
}
