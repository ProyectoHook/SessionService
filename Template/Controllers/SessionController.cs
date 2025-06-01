using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Exceptions;
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


        [HttpPost]
        [ProducesResponseType(typeof(CreateSessionResponse), 201)]
        public async Task<IActionResult> CreateSession(CreateSessionRequest request)
        {
            try
            {
                var result = await _sessionService.CreateSession(request);
                
                //quitar hardcodeo y obtener del launchsettings o de algo 
                result.url = "https://localhost:6662/session/join/" + result.access_code;
                
                return StatusCode(201,result);
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
            
            Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,out userId);

            var response = await _sessionService.GetSessionByAccessCode(accessCode);

            if (response == null)
                return StatusCode(404, "Sesion no encontrada");

            //agrega el participante
            var result = await _sessionService.Join(response.idSession,userId);

            return StatusCode(200,response);
        }
           

        [HttpPost("logout/{id}")]
        public async Task<IActionResult> LogoutSession(Guid id)
        {
            try 
            {
                await _sessionService.EndSession(id);
            }
            catch (ExceptionNotFound ex) { return BadRequest(ex.Message); }

            return Ok(new { message = "Sesión finalizada correctamente." });
        }
        

        [HttpGet]
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

        [HttpGet("status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Status()
        {
            await Task.CompletedTask;
            return StatusCode(200,"Session Service activo");
        }


        [Authorize]
        [HttpGet("secure-test")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetSeguro()
        {
            var usuario = User.Identity.Name;
            

            var userId = User.FindFirst("sub")?.Value;
            var email = User.FindFirst("email")?.Value;
            var username = User.Identity?.Name; // A veces viene del claim "name" o "preferred_username"

            return StatusCode(200, $"Hola {usuario}, estás autenticado. Datos {userId} - {email} - {username}");

        }

    }
}
