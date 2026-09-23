using LotoApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LotoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WinnersController : ControllerBase
    {
        private readonly IWinnerService _winnerService;

        public WinnersController(IWinnerService winnerService)
        {
            _winnerService = winnerService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetWinnersBoard()
        {
            try
            {
                var winners = await _winnerService.GetWinnersBoardAsync();

                return Ok(winners);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
