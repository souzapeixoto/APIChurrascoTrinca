using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace Infrastructure.Repositories
{
    public class RepositoryUsuario : RepositoryBase<Usuario>, IRepositoryUsuario
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ApplicationDbContext _context;
        public RepositoryUsuario(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor
            )
            : base(context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }
        public void Dispose()
        {
            {
                if (Context != null)
                {
                    ((IDisposable)Context).Dispose();
                }
            }
        }

        public Usuario UsuarioLogado()
        {
            Usuario currentUser = null;
            string currentUserId = _httpContextAccessor.HttpContext.User.FindFirstValue("Id");

            currentUser = Context.Users
             .FirstOrDefault(x => x.Id == currentUserId);
            return currentUser;
        }


        public override void Insert(Usuario entity)
        {
            entity.Id = Guid.NewGuid().ToString();
            base.Insert(entity);
        }

    }
}
