using BMICalculator.API.Models;

namespace BMICalculator.API.Services
{
    public interface IBmiService
    {
        Task<BmiCalculationResponse> CalculateBmiAsync(BmiCalculationRequest request);
    }
} 