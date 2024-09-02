using Microsoft.EntityFrameworkCore;
using PhoneBook.API.Data;
using PhoneBook.API.Models.Domain;

namespace PhoneBook.API.Repositories
{
    public class SQLContactRepository: IContactRepository
    {
        private readonly PhoneBookDbContext dbContext;

        public SQLContactRepository(PhoneBookDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Contact>> GetAllAsync()
        {
            return await dbContext.Contacts.ToListAsync();
        }

        public async Task<Contact?> GetByIdAsync(Guid id)
        {
            return await dbContext.Contacts.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Contact> CreateAsync(Contact contact)
        {
            await dbContext.Contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync();
            return contact;
        }

        public async Task<Contact?> UpdateAsync(Guid id, Contact contact)
        {
            var existingContact = await dbContext.Contacts.FirstOrDefaultAsync(x => x.Id == id);
            if (existingContact == null) { return null; }
            
            existingContact.FirstName = contact.FirstName;
            existingContact.LastName = contact.LastName;
            existingContact.Email = contact.Email;
            existingContact.PhoneNumber = contact.PhoneNumber;

            await dbContext.SaveChangesAsync();
            return existingContact;

        }

        public async Task<Contact?> DeleteAsync(Guid id)
        {
            var existingContact = await dbContext.Contacts.FirstOrDefaultAsync(x => x.Id == id);
            if (existingContact == null)
            {
                return null;
            }

            dbContext.Contacts.Remove(existingContact);
            await dbContext.SaveChangesAsync();
            return existingContact;
        }
    }
}
