using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.JsonWebTokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace BlogManagement.Utility.JWT
{
    public class JWTTokenHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tokenModel"></param>
        /// <param name="jwtTokenOptions"></param>
        /// <returns></returns>
        public static string JwtEncrypt(TokenModelJwt tokenModel, JWTTokenOptions jwtTokenOptions)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, tokenModel.UserId.ToString()),//Jwt唯一标识Id
                new Claim(JwtRegisteredClaimNames.Iat, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),//令牌签发时间
                new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,//不早于的时间声明
                new Claim(JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddSeconds(jwtTokenOptions.Expire)).ToUnixTimeSeconds()}"),//令牌过期时间
                new Claim(ClaimTypes.Expiration, DateTime.Now.AddSeconds(jwtTokenOptions.Expire).ToString(CultureInfo.CurrentCulture)),//令牌截至时间
                new Claim(JwtRegisteredClaimNames.Iss,jwtTokenOptions.Issuer),//发行人
                new Claim(JwtRegisteredClaimNames.Aud,jwtTokenOptions.Audience),//订阅人
                new Claim(ClaimTypes.Role,tokenModel.Level)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenOptions.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(claims: claims, signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        /// <summary>
        /// jwt解密
        /// </summary>
        /// <param name="jwtStr"></param>
        /// <returns></returns>
        public static TokenModelJwt JwtDecrypt(string jwtStr)
        {
            if (string.IsNullOrEmpty(jwtStr) || string.IsNullOrWhiteSpace(jwtStr))
            {
                return new TokenModelJwt();
            }
            jwtStr = jwtStr.Substring(7);//截取前面的Bearer和空格
            var jwtHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);

            jwtToken.Payload.TryGetValue(ClaimTypes.Role, out object level);

            var model = new TokenModelJwt
            {
                UserId = int.Parse(jwtToken.Id),
                Level = level == null ? "" : level.ToString()
            };
            return model;
        }
    }

    /// <summary>
    /// 令牌包含的信息
    /// </summary>
    public class TokenModelJwt
    {
        public long UserId { get; set; }

        public string Level { get; set; }
    }
}
