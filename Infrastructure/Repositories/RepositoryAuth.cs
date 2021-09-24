using Application.DTO;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RepositoryAuth : IRepositoryAuth, IDisposable
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly JWT _jwt;


        public RepositoryAuth(UserManager<Usuario> userManager,
            ApplicationDbContext context,
            IOptions<JWT> jwt,
            IHttpContextAccessor httpContextAccessor)
        {
            _jwt = jwt.Value;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IdentityResult> RegisterUser(Usuario model, string password)
        {
            model.Id = Guid.NewGuid().ToString();
            IdentityResult result = null;
            var user = model;
            user.UserName = model.Email;
            try
            {
                result = await _userManager.CreateAsync(user, password);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            if (result.Succeeded == true)
            {
                await this._userManager.AddClaimAsync(user, new Claim(ClaimTypes.Name, model?.Nome));
                await this._userManager.AddClaimAsync(user, new Claim("Imagem", model?.Imagem ?? ""));
                await this._userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, model?.Email));
                await this._userManager.AddClaimAsync(user, new Claim("Id", model?.Id));

            }
            else
            {
                throw new Exception(result.Errors.First().Description);
            }



            return result;
        }

        public async Task AddUpdateClaimAsync(IPrincipal currentPrincipal, Usuario user, string key, string value)
        {
            var identity = currentPrincipal.Identity as ClaimsIdentity;

            if (identity == null)
                return;

            // check for existing claim and remove it
            var existingClaim = identity.FindFirst(key);
            if (existingClaim != null)
            {
                identity.RemoveClaim(existingClaim);
                await this._userManager.RemoveClaimAsync(user, existingClaim);
            }


            // add new claim
            identity.AddClaim(new Claim(key, value));

            await this._userManager.AddClaimAsync(user, new Claim(key, value));
        }

        public string GetClaimValue(IPrincipal currentPrincipal, string key)
        {
            var identity = currentPrincipal.Identity as ClaimsIdentity;
            if (identity == null)
                return null;

            var claim = identity.Claims.FirstOrDefault(c => c.Type == key);
            return claim.Value;
        }


        public async Task<Usuario> FindUser(string userName, string password)
        {
            Usuario user = await _userManager.FindByLoginAsync(userName, password);
            return user;
        }

        private RefreshToken CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var generator = new RNGCryptoServiceProvider())
            {
                generator.GetBytes(randomNumber);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomNumber),
                    Expires = DateTime.UtcNow.AddDays(10),
                    Created = DateTime.UtcNow
                };
            }
        }

        public async Task<Autentication> GetTokenAsync(Login model)
        {
            var authenticationModel = new Autentication();
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = $"No Accounts Registered with {model.Email}.";
                return authenticationModel;
            }
            if (await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authenticationModel.IsAuthenticated = true;
                JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
                authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                authenticationModel.Email = user.Email;
                authenticationModel.UserName = user.UserName;
                var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                authenticationModel.Roles = rolesList.ToList();

                //Limpa os refresh tokens e add um novo
                var refreshTokens = _context.RefreshTokens.Where(x => x.Usuario.Id == user.Id).ToList();
                _context.RefreshTokens.RemoveRange(refreshTokens);

                var refreshToken = CreateRefreshToken();
                authenticationModel.RefreshToken = refreshToken.Token;
                authenticationModel.RefreshTokenExpiration = refreshToken.Expires;
                user.RefreshTokens.Add(refreshToken);
                _context.Users.Update(user);

                return authenticationModel;
            }
            authenticationModel.IsAuthenticated = false;
            authenticationModel.Message = $"Incorrect Credentials for user {user.Email}.";
            return authenticationModel;
        }

        private async Task<JwtSecurityToken> CreateJwtToken(Usuario user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user?.Email ?? ""),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }

        public async Task<Autentication> RefreshTokenAsync(string token)
        {
            var authenticationModel = new Autentication();

            var user = _context.Users.Include(x => x.RefreshTokens).SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));
            if (user == null)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = $"Token did not match any users.";
                return authenticationModel;
            }
            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);
            if (!refreshToken.IsActive)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = $"Token Not Active.";
                return authenticationModel;
            }
            //Revoke Current Refresh Token            
            _context.RefreshTokens.Remove(refreshToken);
            //Generate new Refresh Token and save to Database
            var newRefreshToken = CreateRefreshToken();
            user.RefreshTokens.Add(newRefreshToken);
            _context.Users.Update(user);
            //Generates new jwt
            authenticationModel.IsAuthenticated = true;
            JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
            authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authenticationModel.Email = user.Email;
            authenticationModel.UserName = user.UserName;
            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            authenticationModel.Roles = rolesList.ToList();
            authenticationModel.RefreshToken = newRefreshToken.Token;
            authenticationModel.RefreshTokenExpiration = newRefreshToken.Expires;
            return authenticationModel;
        }

        public void Dispose()
        {
            _userManager.Dispose();

        }

    }
}

