using LotoMate.Identity.Infrastructure.Models;
using System;

namespace LotoMate.Identity.API.ViewModels
{
    /// <summary>
    /// JWT successful response.
    /// </summary>
    /// <remarks>
    /// See: https://www.oauth.com/oauth2-servers/access-tokens/access-token-response/
    /// </remarks>
    public class AccessTokensResponse
    {
        /// <summary>
        /// Initializes a new instance of <seealso cref="AccessTokensResponse"/>.
        /// </summary>
        /// <param name="token"></param>

        public AccessTokensResponse(Token token)
        {
            access_token = token.AccessToken;
            token_type = "Bearer";
            expires_in = Math.Truncate((token.Expiration - DateTime.UtcNow).TotalSeconds);
        }

        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public string token_type { get; set; }
        public double expires_in { get; set; }
    }
}