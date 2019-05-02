using GestaoFinancaPessoal.Controllers;
using GestaoFinancaPessoal.Data;
using GestaoFinancaPessoal.Models;
using GestaoFinancaPessoal.Uteis;
using GestaoFinancaPessoal.Uteis.Exception.ModelErrorException;
using GestaoFinancaPessoal.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

        public CategoriaDAO(Controller controller) : base(controller)
        {
        }

        public override void Add(Categoria Categoria)
        {
            if (this.GetCategoriaByDescricao(Categoria).Count != 0)
            {
                this.ModelState.AddModelError(nameof(Categoria.Nome), "Categoria já cadastrada.");
                throw new ModelErrorException();
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
            var result = this.DbSet.Where(c => c.Nome == Categoria.Nome && c.IsSuspenco == false).ToList();
            return result;
        }

        public IList<Categoria> ListSubCategoria(Boolean mapeado = false)
        {
            if (mapeado)
            {
                return DbSet.Where(c => c.Hierarquia != null && !c.IsSuspenco).ToList();//ele mapeia todos objetos 
            }
            else
            {
                return DbSet.AsNoTracking().Where(c => c.Hierarquia != null && !c.IsSuspenco).ToList();//ele nao mapeia os objetos 
            }
        }

        public IList<Categoria> ListCategoria(Boolean mapeado = false)
        {
            if (mapeado)
            {
                return DbSet.Where(c => c.Hierarquia == null && !c.IsSuspenco ).ToList();//ele mapeia todos objetos 
            }
            else
            {
                return DbSet.AsNoTracking().Where(c => c.Hierarquia == null && !c.IsSuspenco).ToList();//ele nao mapeia os objetos 
            }
        }

        public new IList<CategoriaViewModel> List(Boolean mapeado = false)
        {
            if (mapeado)
            {
                return DbSet.Where(c => !c.IsSuspenco).Include(c => c.Hierarquia).Select(c => new CategoriaViewModel(c,
                    Context.Set<Lancamento>().Where(l => l.Categoria.Id == c.Id).FirstOrDefault() != null
                    )).ToList();
            }
            else
            {
                return DbSet.Where(c => !c.IsSuspenco).Include(c => c.Hierarquia).Select(c => new CategoriaViewModel(c,
                    Context.Set<Lancamento>().Where(l => l.Categoria.Id == c.Id).FirstOrDefault() != null
                    )).AsNoTracking().ToList();
            }
        }
    }
}
