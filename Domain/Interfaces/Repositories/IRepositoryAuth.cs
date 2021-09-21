using Domain.DTO;
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
        Task<DTOAutentication> GetTokenAsync(DTOLogin model);
        Task<DTOAutentication> RefreshTokenAsync(string token);

    }
}
