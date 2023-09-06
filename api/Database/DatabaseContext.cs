using api.Database.Entities;
using api.Database.EntityConfigs;
using Microsoft.EntityFrameworkCore;

namespace api.Database;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
    }


    public virtual required DbSet<UserEntity> UserEntities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new UserConfig());
        
    } 
}