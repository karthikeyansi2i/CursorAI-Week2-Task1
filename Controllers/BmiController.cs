using BMICalculator.API.Models;
using BMICalculator.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BMICalculator.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BmiController : ControllerBase
    {
        private readonly IBmiService _bmiService;
        private readonly ILogger<BmiController> _logger;

        public BmiController(IBmiService bmiService, ILogger<BmiController> logger)
        {
            _bmiService = bmiService;
            _logger = logger;
        }

        [HttpPost("calculate")]
        public async Task<ActionResult<BmiCalculationResponse>> Calculate(BmiCalculationRequest request)
        {
            try
            {
                var response = await _bmiService.CalculateBmiAsync(request);
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Invalid BMI calculation request");
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calculating BMI");
                return StatusCode(500, new { message = "An error occurred while calculating BMI" });
            }
        }
    }
}