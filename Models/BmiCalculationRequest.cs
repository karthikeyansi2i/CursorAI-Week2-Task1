using System.ComponentModel.DataAnnotations;

namespace BMICalculator.API.Models
{
    public class BmiCalculationRequest
    {
        [Required]
        [Range(0.1, 3.0, ErrorMessage = "Height must be between 0.1 and 3.0 meters")]
        public double HeightInMeters { get; set; }

        [Required]
        [Range(0.1, 500, ErrorMessage = "Weight must be between 0.1 and 500 kilograms")]
        public double WeightInKilograms { get; set; }
    }
}