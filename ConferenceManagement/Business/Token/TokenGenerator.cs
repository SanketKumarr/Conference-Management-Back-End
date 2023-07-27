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
        /// <summary>
        /// Mohit :- GenerateToken
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
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
            var userSecurityTokenHandler = new JwtSecurityTokenHandler().WriteToken(userJwtSecurityToken);
            return userSecurityTokenHandler;
            //var userSecurityTokenHandler = new JwtSecurityTokenHandler().WriteToken(userJwtSecurityToken);
            //string userJwtSecurityTokenHandler = JsonConvert.SerializeObject(new { Token = userSecurityTokenHandler });

            //return userJwtSecurityTokenHandler;


        }

        

        }
    }
}
