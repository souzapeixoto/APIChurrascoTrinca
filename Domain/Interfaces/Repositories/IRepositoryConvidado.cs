using Domain.Entities;
using System;

namespace Domain.Interfaces.Repositories
{
    public interface IRepositoryConvidado : IRepositoryBase<Convidado>, IDisposable
    {
    }
}
