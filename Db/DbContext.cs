using Microsoft.EntityFrameworkCore;

namespace DotNetApi.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        public DbSet<PersonModel> People { get; set; }
        public DbSet<UserModel> Users { get; set; }
    }
}