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
    public class RepositoryChurrasco : RepositoryBase<Churrasco>, IRepositoryChurrasco
    {
        public RepositoryChurrasco(ApplicationDbContext context)
            : base(context)
        {
        }
        public override Churrasco GetById(object id)
        {
            return Context.Churrascos.Include(x => x.Convidados).Include(x => x.Opcoes).Where(x => x.Id == (int)id).FirstOrDefault();
        }

        public override IEnumerable<Churrasco> GetAll()
        {
            return Context.Churrascos.Include(x=>x.Convidados);
        }
        public override IEnumerable<Churrasco> GetAll(Expression<Func<Churrasco, bool>> predicate = null)
        {
            return Context.Churrascos.Include(x => x.Convidados).Include(x => x.Opcoes).Where(predicate);
        }
        public override void Update(Churrasco entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            //Context.Entry(entity.Convidados).State = EntityState.Modified;
            base.Update(entity);
        }
        public override void Delete(Churrasco entity)
        {
            foreach (var c in entity.Convidados)
            {
                Context.Entry(c).State = EntityState.Deleted;
            }
            foreach (var o in entity.Opcoes)
            {
                Context.Entry(o).State = EntityState.Deleted;
            }

            base.Delete(entity);
        }
    }
}
