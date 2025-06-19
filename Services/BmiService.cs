using BMICalculator.API.Data;
using BMICalculator.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BMICalculator.API.Services
{
    public class BmiService : IBmiService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BmiService> _logger;

        public BmiService(ApplicationDbContext context, ILogger<BmiService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<BmiCalculationResponse> CalculateBmiAsync(BmiCalculationRequest request)
        {
            _logger.LogInformation("Calculating BMI for weight: {Weight}kg and height: {Height}m", 
                request.WeightInKilograms, request.HeightInMeters);

            var bmi = request.WeightInKilograms / (request.HeightInMeters * request.HeightInMeters);
            var roundedBmi = Math.Round(bmi, 2);

            var category = await _context.BmiCategories
                .FirstOrDefaultAsync(c => roundedBmi >= c.MinValue && 
                    (c.MaxValue == null || roundedBmi < c.MaxValue));

            if (category == null)
            {
                _logger.LogWarning("No BMI category found for BMI value: {Bmi}", roundedBmi);
                throw new InvalidOperationException("Unable to determine BMI category");
            }

            _logger.LogInformation("BMI calculated successfully: {Bmi}, Category: {Category}", 
                roundedBmi, category.Name);

            return new BmiCalculationResponse
            {
                Bmi = roundedBmi,
                Category = category.Name
            };
        }
    }
} 