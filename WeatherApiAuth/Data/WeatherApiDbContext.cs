using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WeatherApiAuth.Models;

namespace WeatherApiAuth.Data
{
    public class WeatherApiDbContext: IdentityDbContext<AppUser>
    {
        public WeatherApiDbContext(DbContextOptions<WeatherApiDbContext> options)
            : base(options) 
        { 
        }       
    }
}
