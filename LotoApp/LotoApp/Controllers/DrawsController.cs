using LotoApp.Domain.Enums;
using LotoApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LotoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrawsController : ControllerBase
    {
        private readonly IDrawService _drawService;

        public DrawsController(IDrawService drawService)
        {
            _drawService = drawService;
        }

        [HttpPost("trigger")]
        [Authorize(Roles = nameof(Role.Admin))]
        public async Task<IActionResult> TriggerDraw()
        {
            try
            {
                int adminId = GetUserIdFromClaims();
                var result = await _drawService.ExecuteDrawAsync(adminId);
                return Ok(result);
            }
            catch(NullReferenceException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("latest")]
        [AllowAnonymous]
        public async Task<IActionResult> GetLatestDraw()
        {
            try
            {
                var result = await _drawService.GetLatestDrawAsync();
                if(result == null)
                {
                    return NotFound("No draws have been exetuted yet");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        private int GetUserIdFromClaims()
        {
            var userIdClaim = User.FindFirst("id")?.Value
                ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                throw new UnauthorizedAccessException("Invalid user token claim.");
            }

            return userId;
        }

    }
}
