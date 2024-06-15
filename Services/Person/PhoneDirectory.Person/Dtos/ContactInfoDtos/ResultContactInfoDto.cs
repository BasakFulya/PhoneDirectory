using PhoneDirectory.Person.Entities;

namespace PhoneDirectory.Person.Dtos.ContactInfoDtos
{
    public class ResultContactInfoDto
    {
        public string ContactInfoID { get; set; }
        public string PersonID { get; set; }
        public ContactType Type { get; set; }
        public string Content { get; set; }
    }
}
