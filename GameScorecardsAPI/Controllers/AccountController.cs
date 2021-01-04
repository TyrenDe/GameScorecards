using GameScorecardsAPI.Settings;
using GameScorecardsDataAccess.Models;
using GameScorecardsModels;
using GameScorecardsModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameScorecardsAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiversion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> m_SigninManager;
        private readonly UserManager<ApplicationUser> m_UserManager;
        private readonly RoleManager<IdentityRole> m_RoleManager;
        private readonly APISettings m_APISettings;

        public AccountController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<APISettings> options)
        {
            m_RoleManager = roleManager;
            m_UserManager = userManager;
            m_SigninManager = signInManager;
            m_APISettings = options.Value;
        }

        [HttpPost("register")]
        public async Task<ActionResult<SignInResponse>> RegisterAsync([FromBody] RegisterRequest request, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            if (request == null)
            {
                return BadRequest(new ErrorResponse { Messages = new string[] { "No RegisterRequest was supplied." } });
            }

            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                Name = request.Name,
                EmailConfirmed = true,
            };

            var createResult = await m_UserManager.CreateAsync(user, request.Password);
            if (!createResult.Succeeded)
            {
                var errors = createResult.Errors.Select(e => e.Description);
                return BadRequest(new ErrorResponse { Messages = errors });
            }

            var result = await SignInAsync(request.Email, request.Password, token);
            if (result != null)
            {
                return Ok(result);
            }

            return Unauthorized(new ErrorResponse
            {
                Messages = new string[] { "Invalid Authentication" },
            });
        }

        [HttpPost("signin")]
        public async Task<ActionResult<SignInResponse>> SignInAsync([FromBody] SignInRequest request, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            if (request == null)
            {
                return BadRequest(new ErrorResponse { Messages = new string[] { "No SignInRequest was supplied." } });
            }

            var result = await SignInAsync(request.Email, request.Password, token);
            if (result != null)
            {
                return Ok(result);
            }

            return Unauthorized(new ErrorResponse
            {
                Messages = new string[] { "Invalid Authentication" },
            });
        }

        private async Task<SignInResponse> SignInAsync(string email, string password, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            var result = await m_SigninManager.PasswordSignInAsync(email, password, false, false);
            if (result.Succeeded)
            {
                var user = await m_UserManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return null;
                }

                var signinCredentials = GetSigningCredentials();
                var claims = await GetClaimsAsync(user, token);

                var tokenOptions = new JwtSecurityToken(
                    issuer: m_APISettings.ValidIssuer,
                    audience: m_APISettings.ValidAudience,
                    claims: claims,
                    expires: DateTime.Now.AddDays(30),
                    signingCredentials: signinCredentials);

                var jwtToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return new SignInResponse
                {
                    Succeeded = true,
                    Token = jwtToken,
                };
            }

            return null;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(m_APISettings.SecretKey));
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaimsAsync(ApplicationUser user, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("Id",user.Id),
            };

            var roles = await m_UserManager.GetRolesAsync(await m_UserManager.FindByEmailAsync(user.Email));
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }
    }
}
