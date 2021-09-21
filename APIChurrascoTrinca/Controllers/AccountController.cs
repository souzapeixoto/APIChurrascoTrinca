using AutoMapper;
using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace APIChurrascoTrinca.Controllers
{
    [Route("api/Account")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IRepositoryAuth _repositoryAuth;
        private readonly IMapper _map;
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(
            IRepositoryAuth repositoryAuth,
            IMapper map, IUnitOfWork unitOfWork,
            ILogger<AccountController> logger)
        {
            _logger = logger;
            _map = map;
            _unitOfWork = unitOfWork;
            _repositoryAuth = repositoryAuth;
        }

        [HttpPost("token")]
        public async Task<IActionResult> GetTokenAsync(DTOLogin model)
        {
            try
            {
                var result = await _repositoryAuth.GetTokenAsync(model);
                SetRefreshTokenInCookie(result.RefreshToken);
                _unitOfWork.Commit();
                return Ok(result);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register(RegisterUser userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(x => x.Errors));
            }
            var usuario = _map.Map<Usuario>(userModel);
            try
            {

                IdentityResult result = await _repositoryAuth.RegisterUser(usuario, userModel.Password);
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);

                }
                _unitOfWork.Commit();
                return Ok();

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }


        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(string refreshtoken)
        {
            //var refreshToken = Request.Cookies["refreshToken"];
            var response = await _repositoryAuth.RefreshTokenAsync(refreshtoken);
            if (!string.IsNullOrEmpty(response.RefreshToken))
                SetRefreshTokenInCookie(response.RefreshToken);
            if (!response.IsAuthenticated)
            {
                return Unauthorized();
            }
            _unitOfWork.Commit();
            return Ok(response);
        }

        private void SetRefreshTokenInCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(1),
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
    }
}
