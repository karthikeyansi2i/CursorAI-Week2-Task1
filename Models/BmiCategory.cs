namespace BMICalculator.API.Models
{
    public class BmiCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double MinValue { get; set; }
        public double? MaxValue { get; set; }
    }
} 