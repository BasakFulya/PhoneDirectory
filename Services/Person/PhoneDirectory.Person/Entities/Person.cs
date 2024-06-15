using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PhoneDirectory.Person.Entities
{
    public class Person
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string PersonID { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Company { get; set; }
    }
}
