using BMICalculator.API.Models;

namespace BMICalculator.API.Services
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
    }
} 