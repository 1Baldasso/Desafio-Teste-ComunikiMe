using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComunikiMe.Domain;

namespace ComunikiMe.WebAPI.Data
{
    public class ComunikiMeWebAPIContext : DbContext
    {
        public ComunikiMeWebAPIContext (DbContextOptions<ComunikiMeWebAPIContext> options)
            : base(options)
        {
        }

        public DbSet<ComunikiMe.Domain.Produto> Produto { get; set; } = default!;

        public DbSet<ComunikiMe.Domain.Usuario> Usuario { get; set; }
    }
}
