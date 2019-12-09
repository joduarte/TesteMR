using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteMR.Models;

namespace TesteMR.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Artigo> Artigos { get; set; }
        
        public DbSet<Unidade> Unidades { get; set; }

        public DbSet<UnidadeArtigo> UnidadesArtigos { get; set; }


        
    }


}
