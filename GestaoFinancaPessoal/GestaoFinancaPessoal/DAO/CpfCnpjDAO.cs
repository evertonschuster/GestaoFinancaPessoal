using GestaoFinancaPessoal.Controllers;
using GestaoFinancaPessoal.Models;
using GestaoFinancaPessoal.Uteis.Exception.ModelErrorException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoFinancaPessoal.DAO
{
    public class CpfCnpjDAO : DAO.DAO<CPFCNPJ>
    {

        public CpfCnpjDAO(Controller controller) : base(controller)
        {
        }

        public CpfCnpjDAO(IDAO dao) : base(dao)
        {
        }

        public override void Add(CPFCNPJ p)
        {
            if (this.IsValid(p))
            {
                var listContribuintes = this.GetByNome(p.Nome);
                if( listContribuintes.Count != 0)
                {
                    this.ModelState.AddModelError(nameof(p.Nome), "Favorecido/Pagador já cadastrado.");
                    throw new ModelErrorException();
                }
                listContribuintes = this.GetByCpfCnpj(p.CpfCnpj);
                if (listContribuintes.Count != 0)
                {
                    this.ModelState.AddModelError(nameof(p.CpfCnpj), "Favorecido/Pagador já cadastrado.");
                    throw new ModelErrorException();

                }

                DbSet.Add(p);
            }
        }

        public IList<CPFCNPJ> GetByNome(string Nome)
        {
            return DbSet.Where(c => c.Nome == Nome).ToList();
        }

        public IList<CPFCNPJ> GetByCpfCnpj(string CpfCnpj)
        {
            return DbSet.Where(c => c.CpfCnpj == CpfCnpj).ToList();
        }
    }
}
