using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ToDoList.Data;

namespace ToDoList
{
    public class AuthenticationDbContext : IdentityDbContext<ToDoUser>
    {
        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options)
            : base(options)
        {
        }
    }

    public class AuthenticationContextFactory : IDesignTimeDbContextFactory<AuthenticationDbContext>
    {
        public AuthenticationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AuthenticationDbContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=todo_authentication;");
            //ptionsBuilder.UseSqlServer("AutheticationDbContextConnection");

            return new AuthenticationDbContext(optionsBuilder.Options);
        }
    }
}
