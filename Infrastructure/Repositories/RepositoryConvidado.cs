using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class RepositoryConvidado : RepositoryBase<Convidado>, IRepositoryConvidado
    {
        public RepositoryConvidado(ApplicationDbContext context)
            : base(context)
        {
        }
        public override Convidado GetById(object id)
        {
            return Context.Convidados.Include(x => x.Churrasco).Include(x => x.Opcao).Where(x => x.Id == (int)id).FirstOrDefault();
        }

        public override IEnumerable<Convidado> GetAll()
        {
            return Context.Convidados.Include(x => x.Churrasco).Include(x => x.Opcao);
        }
        public override IEnumerable<Convidado> GetAll(Expression<Func<Convidado, bool>> predicate = null)
        {
            return Context.Convidados.Include(x => x.Churrasco).Include(x => x.Opcao);
        }
    }
}
