using System;
using GestaoFinancaPessoal.Uteis;
using Microsoft.EntityFrameworkCore;

namespace GestaoFinancaPessoal.DAO
{
    public interface IDAO : IDisposable
    {
        DbContext GetContext();
        //protected readonly Microsoft.EntityFrameworkCore.DbSet<T> dbSet;
        Session GetSssion();
    }
}