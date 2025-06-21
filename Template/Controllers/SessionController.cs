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

        
        [HttpPost("logout/{id}")]
        //[Authorize]
        public async Task<IActionResult> LogoutSession(int id)
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
    }
}
