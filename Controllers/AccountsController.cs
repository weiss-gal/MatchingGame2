using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MatchingGame2.database;
using MatchingGame2.models.user.dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace MatchingGame2.Controllers
{
    [Route("api/public/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        public AccountsController(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        private UserToken buildToken(UserInfo userInfo, string userId)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, userInfo.EmailAddress),
                new Claim(ClaimTypes.Email, userInfo.EmailAddress),
                new Claim(ClaimTypes.NameIdentifier, userId)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddDays(90);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: creds
                );

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token), 
                Expiration = expiration
            };

        }

        [HttpPost("create")]
        public async Task<ActionResult<UserToken>> createUser([FromBody]UserInfo userInfo)
        {
            var newUser = new ApplicationUser { UserName = userInfo.EmailAddress, Email = userInfo.EmailAddress, DisplayName = userInfo.DisplayName};
            var result = await _userManager.CreateAsync(newUser, userInfo.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            var userId = await _userManager.GetUserIdAsync(newUser);

            return buildToken(userInfo, userId);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserToken>> Login([FromBody]UserInfo userInfo)
        {
            var result = await _signInManager.PasswordSignInAsync(userInfo.EmailAddress, userInfo.Password, false, false);

            if (!result.Succeeded)
                return BadRequest("Invalid login attempt");

            var user = await _userManager.FindByNameAsync(userInfo.EmailAddress);
            var userId = await _userManager.GetUserIdAsync(user);
            return buildToken(userInfo, userId);
        }
    }
}