using LotoMate.Identity.Infrastructure.Models;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LotoMate.Identity.Infrastructure.Services
{
    public interface ITokenGeneratorService
    {
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        Task<Token> GenerateTokens(User user,IList<string> roles);
    }
}