using PhoneBook.API.Models.Domain;
using PhoneBook.API.Models.DTO;

namespace PhoneBook.API.Repositories
{
    public interface IContactRepository
    {
        Task<List<Contact>> GetAllAsync();
        Task<Contact?> GetByIdAsync(Guid id);
        Task<Contact> CreateAsync(Contact contact);
        Task<Contact?> UpdateAsync(Guid id, Contact contact);
        Task<Contact?> DeleteAsync(Guid id);
    }
}
