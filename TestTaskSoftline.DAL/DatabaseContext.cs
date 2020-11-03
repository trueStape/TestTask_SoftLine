using Microsoft.EntityFrameworkCore;
using TestTaskSoftline.DAL.Entities;

namespace TestTaskSoftline.DAL
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options)
            : base(options)
        {

        }
        public DbSet<UserEntity> User { get; set; }
    }
}