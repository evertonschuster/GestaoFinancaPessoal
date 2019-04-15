using GestaoFinancaPessoal.Controllers;
using GestaoFinancaPessoal.Data;
using GestaoFinancaPessoal.Models;
using GestaoFinancaPessoal.Uteis;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GestaoFinancaPessoal.DAO
{
    public class RecorrenteDAO : DAO.DAO<Recorrente>
    {
        public RecorrenteDAO(IDAO dao) : base(dao)
        {
        }

        public RecorrenteDAO(Controller controller) : base(controller)
        {
        }
    }
}
