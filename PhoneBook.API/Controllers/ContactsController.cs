using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.API.Data;
using PhoneBook.API.Models.Domain;
using PhoneBook.API.Models.DTO;
using PhoneBook.API.Repositories;

namespace PhoneBook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly PhoneBookDbContext dbContext;
        private readonly IContactRepository contactRepository;
        private readonly IMapper mapper;

        public ContactsController(PhoneBookDbContext dbContext, IContactRepository contactRepository,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.contactRepository = contactRepository;
            this.mapper = mapper;
        }

        //GET ALL CONTACTS
        //GET: https://localhost:portnumber/api/contacts
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var contactsDomainModel = await contactRepository.GetAllAsync();

            return Ok(mapper.Map<List<ContactDto>>(contactsDomainModel));
        }
        
        //GET SINGLE CONTACT
        //GET: htpps://localhost:portnumber/api/contacts/{id}
        [HttpGet]
        [Route("{id}: Guid")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var contactDomainModel = await contactRepository.GetByIdAsync(id);
            if(contactDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ContactDto>(contactDomainModel));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddContactRequestDto addContactRequestDto)
        {
            var contactDomainModel = mapper.Map<Contact>(addContactRequestDto);

            contactDomainModel= await contactRepository.CreateAsync(contactDomainModel);

            var contactDto = mapper.Map<ContactDto>(contactDomainModel);
            return CreatedAtAction(nameof(GetById), new { id = contactDomainModel.Id }, contactDto);
        }

        [HttpPut]
        [Route("{id}: Guid")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateContactRequestDto updateContactRequestDto)
        {
            var contactDomainModel = mapper.Map<Contact>(updateContactRequestDto);

            contactDomainModel = await contactRepository.UpdateAsync(id, contactDomainModel);

            if(contactDomainModel == null)
            { return NotFound(); }

            return Ok(mapper.Map<ContactDto>(contactDomainModel));
        }

        [HttpDelete]
        [Route("{id}: Guid")]

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var contactDomainModel = await contactRepository.DeleteAsync(id);
            if(contactDomainModel == null) { return NotFound(); }

            return Ok(mapper.Map<ContactDto>(contactDomainModel));
        }
    }
}
