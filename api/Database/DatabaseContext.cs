using api.Database.Entities;
using api.Database.EntityConfigs;
using Microsoft.EntityFrameworkCore;

namespace api.Database;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
    }

    protected DatabaseContext()
    {
    }
    
    public virtual  DbSet<UserEntity> UserEntities { get; set; }
    public virtual  DbSet<RoleEntity> RoleEntities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new UserConfig());
        modelBuilder.ApplyConfiguration(new RoleConfig());
    }
}