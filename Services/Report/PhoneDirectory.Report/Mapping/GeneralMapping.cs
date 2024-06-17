using AutoMapper;
using PhoneDirectory.Person.Dtos.ContactInfoDtos;
using PhoneDirectory.Person.Dtos.PersonDtos;
using PhoneDirectory.Person.Entities;
using PhoneDirectory.Report.Dtos.ReportDtos;

namespace PhoneDirectory.Report.Mapping
{
    public class GeneralMapping:Profile
    {
        public GeneralMapping()
        {
            CreateMap<PhoneDirectory.Report.Entities.Report, ResultReportDto>().ReverseMap();
            CreateMap<PhoneDirectory.Report.Entities.Report, CreateReportDto>().ReverseMap();
            CreateMap<PhoneDirectory.Report.Entities.Report, UpdateReportDto>().ReverseMap();
            CreateMap<PhoneDirectory.Report.Entities.Report, GetByIdReportDto>().ReverseMap();

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
