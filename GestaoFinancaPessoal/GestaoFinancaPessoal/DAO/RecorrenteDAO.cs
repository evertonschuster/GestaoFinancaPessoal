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

        public void LancarRecorrente(Lancamento lancamento)
        {
            var lancamentoDAO = this.NewDAO<LancamentoDAO>();

            var lancamentoOLD = lancamento;
            lancamento = lancamento.Clone();
            lancamento.Id = 0;
            lancamento.IsPago = false;

            for (int i = 0; i < lancamentoOLD.Recorrente.Quantidade - 1; i++)
            {
                lancamento = lancamento.Clone();
                lancamento.Id = 0;
                lancamento.Descricao = lancamentoOLD.Descricao + $" - {i + 2}";

                if (lancamento.Recorrente.IsMensal)
                {
                    lancamento.DataPagamento = lancamento.DataPagamento?.AddMonths(1);
                    lancamento.DataVencimento = lancamento.DataVencimento.AddMonths(1);
                }
                else
                {
                    lancamento.DataPagamento = lancamento.DataPagamento?.AddDays(lancamentoOLD.Recorrente.Periodo);
                    lancamento.DataVencimento = lancamento.DataVencimento.AddDays(lancamentoOLD.Recorrente.Periodo);
                }

                lancamento.Recorrente = lancamentoOLD.Recorrente;

                lancamentoDAO.Add(lancamento);
            }
            lancamentoOLD.Descricao += "- 1";
            lancamentoDAO.Add(lancamento);

        }
    }
}
