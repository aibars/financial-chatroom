using AutoMapper;
using Financial.Chat.Domain.ApiModels.Request;
using Financial.Chat.Domain.ApiModels.Response;
using Financial.Chat.Domain.Data;
using Financial.Chat.Logic.Interface;
using Financial.Chat.Providers.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Southapps.Aqui.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        protected readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        protected readonly IDatabaseProvider _databaseProvider;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        
        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IDatabaseProvider databaseProvider,
            IMapper mapper,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _databaseProvider = databaseProvider;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]LoginRequestDto model)
        {
            var user = _userManager.Users.SingleOrDefault(r => r.Email == model.UserName);

            if (user == null)
            {
                ModelState.AddModelError("notExists", "Usuario y/o contraseña incorrectos.");
                return BadRequest(ModelState);
            }

            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, true, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("notExists", "Username or password are incorrect.");
                return BadRequest(ModelState);
            }
            else
            {
                var jsonWebToken = _tokenService.GenerateJwtToken(model.UserName, user);

                var requestedAt = DateTime.UtcNow;

                var returnModel = _mapper.Map<LoggedInUserDto>(user);
                returnModel.LastLoginDate = user.LastLoginDate;
                user.LastLoginDate = requestedAt;
                await _userManager.UpdateAsync(user);
                returnModel.Token = jsonWebToken.Token;
                returnModel.Expires = jsonWebToken.Expiration - requestedAt.Ticks;
                return Ok(returnModel);
            }
        }
    }
}