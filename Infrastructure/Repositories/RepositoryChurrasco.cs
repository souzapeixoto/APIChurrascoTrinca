using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class RepositoryChurrasco : RepositoryBase<Churrasco>, IRepositoryChurrasco
    {
        public RepositoryChurrasco(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
