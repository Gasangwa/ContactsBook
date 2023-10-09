using Microsoft.EntityFrameworkCore;

namespace ContactsBook.Data
{
    public class ContactsBookContext : DbContext
    {
        public ContactsBookContext (DbContextOptions<ContactsBookContext> options)
            : base(options)
        {
        }

        public DbSet<Contacts.Models.Person> Person { get; set; } = default!;
    }
}
