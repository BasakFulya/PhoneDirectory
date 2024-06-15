using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PhoneDirectory.Person.Entities
{
    public class ContactInfo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string ContactInfoId { get; set; }
        public string PersonId { get; set; }
        public ContactType Type { get; set; }
        public string Content { get; set; }
        [BsonIgnore]
        public List<Person> Person { get; set; }
    }

    public enum ContactType
    {
        PhoneNumber,
        EmailAddress,
        Location
    }
}
