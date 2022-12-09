using Microsoft.EntityFrameworkCore;

namespace MyFirstASPNetCoreAPI.Models
{
    public class PetContext : DbContext
    {
        public PetContext(DbContextOptions<PetContext> options)
        : base(options)
        {
        }

        public DbSet<Pet> Pets { get; set; } = null!;
    }
}
