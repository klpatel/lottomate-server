using LotoMate.Identity.Infrastructure.Models;
using LotoMate.Identity.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LotoMate.Identity.Infrastructure.Services
{
    //TODO: move all hardcode ids column to constants.
    public class TokenGeneratorService : ITokenGeneratorService
    {
        private readonly IConfiguration config;
        private readonly UserManager<User> userManager;
        private readonly IUserRoleRepository userRoleRepository;
        public TokenGeneratorService(IConfiguration config, UserManager<User> userManager,
                    IUserRoleRepository userRoleRepository)
        {
            this.config = config;
            this.userManager = userManager;
            this.userRoleRepository = userRoleRepository;
        }

        public async Task<Token> GenerateTokens(User user, IList<string> roles)
        {

            var claims = new List<Claim>
                     {
                            new  Claim(JwtRegisteredClaimNames.Sub, user.Email),
                            new  Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new  Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                            new  Claim("userId", user.Id.ToString())
                        };
            //ICollection<string> roles = new List<string>();
            claims.AddRange(roles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Security:Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            int.TryParse(config["Security:Tokens:ExpirationInMinutes"], out int expirationInMinutes);

            var token = new JwtSecurityToken(
                config["Security:Tokens:Issuer"],
                config["Security:Tokens:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(expirationInMinutes == 0 ? 30 : expirationInMinutes),
                signingCredentials: creds
                );

            var refreshToken = await GenerateRefreshToken(user);
            var tokendms = Encrypt(user.Email);
            var userrole = userRoleRepository.Queryable().Where(x => x.UserId == user.Id).FirstOrDefault();
            return
                 new Token()
                 {
                     AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                     RefreshToken = refreshToken,
                     Username = user.Email,
                     UserId = user.Id,
                     Expiration = token.ValidTo,
                     Roles = roles.ToArray(),
                     UserFullName = user.FirstName + " " + user.LastName,
                     ClientId = userrole?.ClientId,
                     StoreId = userrole?.StoreId
                 };
        }

        private async Task<string> GenerateRefreshToken(User user)
        {
            return await userManager.GenerateUserTokenAsync(user, "LotoMate", "RefreshToken");
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Security:Tokens:Key"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            //TODO: this throws error when access token is not in JWT format. We need to verify format here in this method.
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                //TODO: this line will be logged in logger. Once we have logger we need to uncomment below line
                //new SecurityTokenException("Invalid token");
                return null;
            }

            return principal;
        }

        public string Encrypt(string stringToEncrypt)
        {
            byte[] key = { };
            byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 80 };
            byte[] inputByteArray; //Convert.ToByte(stringToEncrypt.Length) 
            string sEncryptionKey = "ab48495fdjk4950dj39405fk";
            try
            {
                key = Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                return (string.Empty);
            }
        }
    }
}
