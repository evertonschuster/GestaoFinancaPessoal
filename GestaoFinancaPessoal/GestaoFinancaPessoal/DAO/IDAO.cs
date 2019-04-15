using System;
using GestaoFinancaPessoal.Models;
using GestaoFinancaPessoal.Uteis;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace GestaoFinancaPessoal.DAO
{
    public interface IDAO : IDisposable
    {
        DbContext GetContext();
        //protected readonly Microsoft.EntityFrameworkCore.DbSet<T> dbSet;
        Session GetSssion();
        ModelStateDictionary GetModelState();

        Controllers.Controller GetController();


        TDAO NewDAO<TDAO>(TipoConecao tipo = TipoConecao.DEFAULT) where TDAO : IDAO;

    }

    public enum TipoConecao
    {
        DEFAULT,
        NEW_CONNECTION
    }

}