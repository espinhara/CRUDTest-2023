
using CRUDTest.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDTest.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }
        public DbSet<Products> Products { get; set; } = default!;
        public DbSet<Users> Users { get; set; } = default!;
        public DbSet<Carts> Carts{ get; set; } = default!;

    }
}
