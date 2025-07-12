using Application.Exceptions;
using Application.Interfaces.Services;
using Application.Request;
using Application.Response;
using Application.UseCases;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Template.Controllers
{
    [ApiController]
    [Route("api/v1/participant")]
    public class ParticipantController : ControllerBase
    {
        private readonly IParticipantService _participantService;

        public ParticipantController(IParticipantService participantService)
        {
            _participantService = participantService;
        }

        [HttpPost("create")]
        [Authorize]
        [ProducesResponseType(typeof(createParticipantResponse), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateParticipant([FromBody] CreateParticipantRequest request)
        {
            try
            {
                var response = await _participantService.CreateParticipant(request);

                return StatusCode(201, response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpGet("getById/{id}")]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<GetParticipantResponse>> GetByIdParticipant(int id)
        {
            try {
                var participant = await _participantService.GetByIdParticipant(id);
                return Ok(participant);
            }
            catch (ExceptionNotFound ex)
            {
                return NotFound(new { message = ex.Message });
            }

        }

        [HttpGet("GetAll")]
        [Authorize]
        [ProducesResponseType(200)]
        public async Task<ActionResult<List<GetParticipantResponse>>> GetAllParticipants()
        {
            try
            {
                var participants = await _participantService.GetAllParticipants();
                return Ok(participants);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

    }
}
