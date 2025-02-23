using System.IdentityModel.Tokens.Jwt;
using App.Interfaces;

namespace App.Utils
{
    public class JwtTokenReader : IJwtTokenReader
    {
        public JwtSecurityToken ReadToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadJwtToken(token);
            return tokenS;
        }
    }
}
