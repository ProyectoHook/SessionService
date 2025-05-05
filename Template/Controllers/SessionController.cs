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
                return Ok(result); // Devuelve 200 OK
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        
        [HttpPost("logout/{id}")]
        public async Task<IActionResult> LogoutSession(int id)
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
    }
}
