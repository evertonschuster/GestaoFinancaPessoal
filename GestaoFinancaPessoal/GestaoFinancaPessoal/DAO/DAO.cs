using GestaoFinancaPessoal.Models;
using GestaoFinancaPessoal.Uteis;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestaoFinancaPessoal.Models;
using GestaoFinancaPessoal.Data;

namespace GestaoFinancaPessoal.DAO
{
    public class DAO<T> : IDAO, IDisposable where T : MasterModel, IDisposable 
    {
        protected readonly ApplicationDbContext Context;
        protected readonly DbSet<T> DbSet;
        protected readonly Session Session;
        
        public DbContext GetContext()
        {
            return this.Context;
        }

        public Session GetSssion()
        {
            return this.Session;
        }
        public DAO(ApplicationDbContext context, Session session )
        {
            this.Context = context;
            this.DbSet = context.Set<T>();
            this.Session = session;

            context.Database.EnsureCreated();
        }

        public DAO(IDAO dao)
        {
            this.Context = (ApplicationDbContext)dao.GetContext();
            this.DbSet = this.Context.Set<T>();
            this.Session = dao.GetSssion();

            Context.Database.EnsureCreated();
        }

        public virtual void Add(T p)
        {
            DbSet.Add(p);
        }

        public virtual void Update(T p)
        {
            DbSet.Update(p);
        }

        public virtual void Remove(T  p)
        {
            DbSet.Remove(p);
        }

        public virtual IList<T> List(T p)
        {
            return DbSet.ToList();
        }

        public virtual T getById(T p)
        {
            return DbSet.Where(i => i.Id == p.Id).FirstOrDefault();
        }

        public virtual IList<T> List()
        {
            return DbSet.ToList();
        }

        public virtual void SaveChanges()
        {
            Context.SaveChanges();
        }

        public virtual void Dispose()
        {
            Context.Dispose();
        }


    }
}
