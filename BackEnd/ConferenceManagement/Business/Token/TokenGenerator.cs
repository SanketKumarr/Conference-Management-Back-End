using ConferenceManagement.Infrastructure.Commands.AdminCommands;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ConferenceManagement.Business.Token
{
    public class TokenGenerator : ITokenGenerator
    {
        public async Task<string> GenerateToken(int userId, string name)
        {
            var userClaims = new Claim[]
           {
                 new Claim(JwtRegisteredClaimNames.Jti,new Guid().ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName,name)
           };
            var userSecretKey = Encoding.UTF8.GetBytes("dtdkbtftrtjftdftyekyrtererthfssfjffffiieiwwiwwi");
            var userSymmetricSecurityKey = new SymmetricSecurityKey(userSecretKey);
            var userSigningCredentials = new SigningCredentials(userSymmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var userJwtSecurityToken = new JwtSecurityToken(
                issuer: "ConferenceManagementApp",
                audience: "ConferenceManagementAppUsers",
                claims: userClaims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: userSigningCredentials);
            //var userSecurityTokenHandler = new JwtSecurityTokenHandler().WriteToken(userJwtSecurityToken);
            //return userSecurityTokenHandler;
            var userSecurityTokenHandler = new JwtSecurityTokenHandler().WriteToken(userJwtSecurityToken);
            string userJwtSecurityTokenHandler = JsonConvert.SerializeObject(new { Token = userSecurityTokenHandler });

            return userJwtSecurityTokenHandler;


        }

        public async Task<bool> IsTokenValid(string userSecretKey, string userIssuer, string userAudience, string userToken)
        {
            var userSecretKeyInBytes = Encoding.UTF8.GetBytes(userSecretKey);
            var userSymmetricSecurityKey = new SymmetricSecurityKey(userSecretKeyInBytes);
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = userIssuer,

                ValidateAudience = true,
                ValidAudience = userAudience,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = userSymmetricSecurityKey,

                ValidateLifetime = true
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(userToken, tokenValidationParameters, out SecurityToken securityToken);
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
