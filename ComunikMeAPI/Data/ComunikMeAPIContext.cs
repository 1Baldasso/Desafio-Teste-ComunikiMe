using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComunikMeAPI;

namespace ComunikMeAPI.Data
{
    public class ComunikMeAPIContext : DbContext
    {
        public ComunikMeAPIContext (DbContextOptions<ComunikMeAPIContext> options)
            : base(options)
        {
        }

        public DbSet<ComunikMeAPI.ProdutoModel> ProdutoModel { get; set; } = default!;
    }
}
