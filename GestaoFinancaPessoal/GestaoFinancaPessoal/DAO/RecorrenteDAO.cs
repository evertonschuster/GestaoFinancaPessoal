using GestaoFinancaPessoal.Data;
using GestaoFinancaPessoal.Models;
using GestaoFinancaPessoal.Uteis;

namespace GestaoFinancaPessoal.DAO
{
    public class RecorrenteDAO : DAO.DAO<Recorrente>
    {
        public RecorrenteDAO(IDAO dao) : base(dao)
        {
        }

        public RecorrenteDAO(ApplicationDbContext context, Session session) : base(context, session)
        {
        }

        
    }
}
