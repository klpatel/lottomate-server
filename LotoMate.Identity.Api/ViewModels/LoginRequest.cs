using System.ComponentModel.DataAnnotations;

namespace LotoMate.Identity.API.ViewModels
{
    /// <summary>
    /// Encapsulates fields for login request.
    /// </summary>
    /// <remarks>
    /// See: https://www.oauth.com/oauth2-servers/access-tokens/
    /// </remarks>
    public class LoginRequest
    {
        [Required]
        public string GrantType { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }
        public string Scope { get; set; }

        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}