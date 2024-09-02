using AutoMapper;
using PhoneBook.API.Models.Domain;
using PhoneBook.API.Models.DTO;

namespace PhoneBook.API.Mapping
{
    public class AutoMapperProfiles : Profile // Semicolon added here
    {
        public AutoMapperProfiles()
        {
            CreateMap<Contact, ContactDto>().ReverseMap();
            CreateMap<AddContactRequestDto, Contact>().ReverseMap();
            CreateMap<UpdateContactRequestDto, Contact>().ReverseMap();
        }
    }
}