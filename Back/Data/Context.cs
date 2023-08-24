using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using s10.Back.Services.Auth.Models;

namespace s10.Back.Data
{
    public class Context: IdentityDbContext<ApplicationUser>
    {

        public Context()
        {

        }

        public Context(DbContextOptions options)
        : base(options)
        {

        }
    }
}
