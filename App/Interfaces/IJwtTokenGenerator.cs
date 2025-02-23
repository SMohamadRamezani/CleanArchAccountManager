using Domain.Models.User;

namespace App.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
        string GenerateTokenWithNationalCode(string NationalCode);
        string GenerateRefreshToken();
    }

}
