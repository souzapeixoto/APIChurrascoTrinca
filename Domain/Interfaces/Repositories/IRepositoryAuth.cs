using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IRepositoryAuth
    {
        Task<IdentityResult> RegisterUser(Usuario model, string password);
        Task<Usuario> FindUser(string userName, string password);
        Task AddUpdateClaimAsync(IPrincipal currentPrincipal, Usuario user, string key, string value);
        Task<Autentication> GetTokenAsync(Login model);
        Task<Autentication> RefreshTokenAsync(string token);

    }
}
