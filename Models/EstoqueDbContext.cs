using Microsoft.EntityFrameworkCore;

namespace WebMVC_Tests.Models
{
    public class EstoqueDbContext  : DbContext
    {
        public DbSet<Produto> Produtos {  get; set; }
        public EstoqueDbContext(DbContextOptions<EstoqueDbContext> options) : base(options) 
        {

        }
    }
}
