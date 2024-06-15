using AutoMapper;
using PhoneDirectory.Person.Dtos.PersonDtos;
using PhoneDirectory.Person.Dtos.ContactInfoDtos;
using PhoneDirectory.Person.Dtos.PersonDtos;
using PhoneDirectory.Person.Entities;

namespace PhoneDirectory.Person.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<PhoneDirectory.Person.Entities.Person, ResultPersonDto>().ReverseMap();
            CreateMap<PhoneDirectory.Person.Entities.Person, CreatePersonDto>().ReverseMap();
            CreateMap<PhoneDirectory.Person.Entities.Person, UpdatePersonDto>().ReverseMap();
            CreateMap<PhoneDirectory.Person.Entities.Person, GetByIdPersonDto>().ReverseMap();

            CreateMap<ContactInfo, ResultContactInfoDto>().ReverseMap();
            CreateMap<ContactInfo, CreateContactInfoDto>().ReverseMap();
            CreateMap<ContactInfo, UpdateContactInfoDto>().ReverseMap();
            CreateMap<ContactInfo, GetByIdContactInfoDto>().ReverseMap();
        }
    }
}
