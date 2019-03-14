using GestaoFinacaPessoal.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaoFinancaPessoal
{
    public class FinancaContexto : DbContext
    {
        public DbSet<Usuario> Usuario { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=LojaDB;Trusted_Connection=true;");
        //}

        public FinancaContexto(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

    }


}
