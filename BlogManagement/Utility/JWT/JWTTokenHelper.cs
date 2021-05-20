using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace BlogManagement.Utility.JWT
{
    public class JWTTokenHelper
    {
        public static TokenResult AuthorizeToken(int userId, string name, JWTTokenOptions jwtTokenOptions)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Sid, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenOptions.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(jwtTokenOptions.Issuer, jwtTokenOptions.Audience, claims,
                expires: DateTime.Now.AddSeconds(jwtTokenOptions.Expire), signingCredentials: credentials);
            var securityToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return new TokenResult()
            {
                Access_token = "Bearer" + securityToken,
                Exprices_in = jwtTokenOptions.Expire
            };
        }
    }

    public class TokenResult
    {
        public string Access_token { get; set; }

        public long Exprices_in { get; set; }
    }
}
