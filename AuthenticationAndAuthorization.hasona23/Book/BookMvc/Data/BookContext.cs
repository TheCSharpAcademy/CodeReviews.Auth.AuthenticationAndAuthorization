using BookMvc.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;

namespace BookMvc.Data
{
    public class BookContext : IdentityDbContext<IdentityUser>
    {
        public BookContext (DbContextOptions<BookContext> options)
            : base(options)
        { }

        public DbSet<Book> Book { get; set; } = default!;
    }
}
