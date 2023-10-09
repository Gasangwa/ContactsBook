using Microsoft.EntityFrameworkCore;
namespace ContactsBook.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using(var context = new ContactsBookContext(serviceProvider.GetRequiredService<DbContextOptions<ContactsBookContext>>()))
            {
                if (context.Person.Any())
                {
                    return;
                }
                context.Person.AddRange(
                    new Contacts.Models.Person()
                    {
                        Name = "Eric",
                        Surname = "Gasangwa",
                        PhoneNumber = 730019865,
                        Email = "e.gasangwaeric@gmail.com",
                        Date = DateTime.Now,
                    },
                    new Contacts.Models.Person()
                    {
                        Name = "Neils",
                        Surname = "Neza",
                        PhoneNumber = 573526770,
                        Email = "neilsk45@gmail.com",
                        Date = DateTime.Now.AddDays(-1),
                    },
                    new Contacts.Models.Person()
                    {
                        Name = "Daniel",
                        Surname = "Manzi",
                        PhoneNumber = 692373371,
                        Email = "dan@gamil.com",
                        Date = DateTime.Now.AddDays(-2),
                    }
                    );
                context.SaveChanges();
            }
        }
    }
}
