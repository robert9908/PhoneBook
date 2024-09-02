using Microsoft.EntityFrameworkCore;
using PhoneBook.API.Models.Domain;

namespace PhoneBook.API.Data
{
    public class PhoneBookDbContext: DbContext
    {

        public PhoneBookDbContext(DbContextOptions<PhoneBookDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
