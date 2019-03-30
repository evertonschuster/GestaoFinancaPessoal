using GestaoFinancaPessoal.Data;
using GestaoFinancaPessoal.Models;
using GestaoFinancaPessoal.Uteis;
using GestaoFinancaPessoal.Uteis.Exception.ModelErrorException;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoFinancaPessoal.DAO
{
    public class CategoriaDAO : DAO.DAO<Categoria>
    {
        public CategoriaDAO(IDAO dao) : base(dao)
        {
        }

        public CategoriaDAO(ApplicationDbContext context, Session session) : base(context, session)
        {
        }

        public override void Add(Categoria Categoria )
        {
            if (this.GetCategoriaByDescricao(Categoria).Count != 0)
            {
                ModelError erro = new ModelError(nameof(Categoria.Nome),"Categoria já cadastrada.");
                throw new ModelErrorException(erro);
            }
            base.Add(Categoria);
        }

        public override Categoria getById(Categoria p)
        {
            return DbSet.Where(i => i.Id == p.Id).Include(i => i.Hierarquia).FirstOrDefault();
        }

        public override Categoria getById(int id)
        {
            return DbSet.Where(i => i.Id == id).Include(i => i.Hierarquia).FirstOrDefault();
        }

        public List<Categoria> GetCategoriaByDescricao(Categoria Categoria)
        {
            var result = this.DbSet.Where(c => c.Nome == Categoria.Nome).ToList();
            return result;
        }
    }
}
