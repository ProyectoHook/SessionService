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

        /*
        [HttpPost("/logout")]
        public async Task<IActionResult> LogoutSession()
        {
            HttpContext.Session.Clear(); // Elimina todos los datos de sesión

            // si se usa autenticación basada en cookies
            await HttpContext.SignOutAsync();

            return Ok(new { message = "Sesión finalizada correctamente." });
        }
        */
    }
}
