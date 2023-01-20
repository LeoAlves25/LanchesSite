using LanchesSite.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LanchesSite.Context
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Lanche> Lanches { get; set; }
        public DbSet<CarrinhoCompraItem> CarrinhoCompraItems { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<PedidoDetalhe> PedidoDetalhe { get; set; }
    }
}
