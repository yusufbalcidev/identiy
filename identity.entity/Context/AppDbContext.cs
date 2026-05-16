using identity.entity.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace identity.entity.Context; // <--- Namespace düzeltildi

public class AppDbContext : IdentityDbContext<AppUser, AppRole, int>
{
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder); 

        builder.Entity<AppRole>().HasData(
            new AppRole { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
            new AppRole { Id = 2, Name = "User", NormalizedName = "USER" }
        );
    }
}


