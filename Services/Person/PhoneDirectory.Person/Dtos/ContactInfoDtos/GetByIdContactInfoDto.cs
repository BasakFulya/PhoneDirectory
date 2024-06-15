using PhoneDirectory.Person.Entities;

namespace PhoneDirectory.Person.Dtos.ContactInfoDtos
{
    public class GetByIdContactInfoDto
    {
        public string ContactInfoId { get; set; }
        public string PersonId { get; set; }
        public ContactType Type { get; set; }
        public string Content { get; set; }
    }
}
