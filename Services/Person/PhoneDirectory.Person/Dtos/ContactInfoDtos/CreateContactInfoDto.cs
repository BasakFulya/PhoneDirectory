using PhoneDirectory.Person.Entities;

namespace PhoneDirectory.Person.Dtos.ContactInfoDtos
{
    public class CreateContactInfoDto
    {
        public string PersonID { get; set; }
        public ContactType Type { get; set; }
        public string Content { get; set; }
    }
}
