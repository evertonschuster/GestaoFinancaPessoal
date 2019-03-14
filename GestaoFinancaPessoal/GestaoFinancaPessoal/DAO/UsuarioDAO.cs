
using GestaoFinacaPessoal.Models;
using GestaoFinancaPessoal.Uteis;
using GestaoFinancaPessoal.Uteis.Exception.ModelErrorException;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoFinancaPessoal.DAO
{
    public class UsuarioDAO : DAO<Usuario>
    {
        public UsuarioDAO(IDAO dao) : base(dao)
        {
        }

        public UsuarioDAO(FinancaContexto context, Session session) : base(context, session)
        {
        }

        public override void Add(Usuario p)
        {

            StackTrace stackTrace = new StackTrace();
            var classe = stackTrace.GetFrame(2).GetMethod().DeclaringType;

            if (this.getByNome(p).Count > 0)
            {
                throw new ModelErrorException(new ModelError(nameof(p.Nome), "Nome já Cadastrado"));
            }

            if (this.getByEmail(p).Count > 0)
            {
                throw new ModelErrorException(new ModelError(nameof(p.Nome), "E-Mail já Cadastrado"));
            }
        }

        public List<Usuario> getByNomeOrEmail(Usuario p)
        {
            return DbSet.Where(i => i.Nome == p.Nome || i.Email == p.Email).ToList();
        }
        public List<Usuario> getByNome(Usuario p)
        {
            return DbSet.Where(i => i.Nome == p.Nome ).ToList();
        }

        public List<Usuario> getByEmail(Usuario p)
        {
            return DbSet.Where(i => i.Email == p.Email).ToList();
        }
    }
}
