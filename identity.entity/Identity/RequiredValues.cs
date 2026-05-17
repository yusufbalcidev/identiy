using identity.entity.Context;
using identity.entity.Model;
using identity.entity.Validations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration; // IConfiguration için gerekli
using Microsoft.Extensions.DependencyInjection;

namespace identity.entity.Identity;

public static class RequiredValues
{
    
    public static IServiceCollection AddIdentityInfo(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<AppUser, AppRole>(options =>
            {
                
                options.Password.RequiredLength = configuration.GetValue<int>("IdentitySettings:Password:RequiredLength", 6);
                options.Password.RequireNonAlphanumeric = configuration.GetValue<bool>("IdentitySettings:Password:RequireNonAlphanumeric", false);
                options.Password.RequireLowercase = configuration.GetValue<bool>("IdentitySettings:Password:RequireLowercase", false);
                options.Password.RequireUppercase = configuration.GetValue<bool>("IdentitySettings:Password:RequireUppercase", false);
                options.Password.RequireDigit = configuration.GetValue<bool>("IdentitySettings:Password:RequireDigit", false);
                
                options.User.RequireUniqueEmail = configuration.GetValue<bool>("IdentitySettings:User:RequireUniqueEmail", true);
                options.User.AllowedUserNameCharacters = configuration.GetValue<string>("IdentitySettings:User:AllowedUserNameCharacters", 
                    "abcçdefghiıjklmnoöpqrsştuüvwxyzABCÇDEFGHIİJKLMNOÖPQRSŞTUÜVWXYZ0123456789-._@+");
            })
            .AddErrorDescriber<CustomIdentityErrorDescriber>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        // return satırı eklendi (Eksikti)
        return services;
    }
}