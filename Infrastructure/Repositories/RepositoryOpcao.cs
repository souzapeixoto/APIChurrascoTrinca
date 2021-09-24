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
    public class RepositoryOpcao : RepositoryBase<Opcao>, IRepositoryOpcao
    {
        public RepositoryOpcao(ApplicationDbContext context)
            : base(context)
        {
        }
        public override Opcao GetById(object id)
        {
            return Context.Opcoes.Include(x => x.Churrasco).Where(x => x.Id == (int)id).FirstOrDefault();
        }

        public override IEnumerable<Opcao> GetAll()
        {
            return Context.Opcoes.Include(x=>x.Churrasco);
        }
        public override IEnumerable<Opcao> GetAll(Expression<Func<Opcao, bool>> predicate = null)
        {
            return Context.Opcoes.Include(x => x.Churrasco).Where(predicate);
        }
    }
}
