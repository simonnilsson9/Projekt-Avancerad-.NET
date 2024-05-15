using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Projekt_Avancerad_.NET.Data;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace Projekt_Avancerad_.NET.Authentication
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUserService _userService;
        public BasicAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        IUserService userService) : base(options, logger, encoder, clock)
        {
            _userService = userService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            string authorizationHeader = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(authorizationHeader))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            if (!authorizationHeader.StartsWith("basic ", StringComparison.OrdinalIgnoreCase))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            var token = authorizationHeader.Substring(6);
            var credentialAsString = Encoding.UTF8.GetString(Convert.FromBase64String(token));

            var credentials = credentialAsString.Split(":");
            if (credentials.Length != 2)
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            var email = credentials[0];
            var password = credentials[1];

            // Validera användarautentisering mot databasen
            var isAuthenticated = await _userService.ValidateCredentialsAsync(email, password);
            if (isAuthenticated == false)
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            // Hämta användarroller från databasen
            var roles = await _userService.GetRolesAsync(email);

            if (roles == null || !roles.Any())
            {
                return AuthenticateResult.Fail("Unauthorized: No roles found for the user.");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, email),
                new Claim(ClaimTypes.Email, email) 
            };

            // Skapa en ClaimsIdentity med användarroller
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var claimsPrincipal = new ClaimsPrincipal(identity);

            return AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name));

        }
    }
}
