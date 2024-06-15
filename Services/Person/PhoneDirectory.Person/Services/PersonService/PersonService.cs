using AutoMapper;
using MongoDB.Driver;
using PhoneDirectory.Person.Dtos.PersonDtos;
using PhoneDirectory.Person.Settings;

namespace PhoneDirectory.Person.Services.PersonService
{
    public class PersonService : IPersonService
    {
        private readonly IMongoCollection<PhoneDirectory.Person.Entities.Person> _PersonCollection;
        private readonly IMapper _mapper;

        public PersonService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _PersonCollection = database.GetCollection<PhoneDirectory.Person.Entities.Person>(_databaseSettings.PersonCollectionName);
            _mapper = mapper;
        }

        public async Task CreatePersonAsync(CreatePersonDto createPersonDto)
        {
            var value = _mapper.Map<PhoneDirectory.Person.Entities.Person>(createPersonDto);
            await _PersonCollection.InsertOneAsync(value);
        }

        public async Task DeletePersonAsync(string id)
        {
            await _PersonCollection.DeleteOneAsync(x => x.PersonID == id);
        }

        public async Task<List<ResultPersonDto>> GetAllPersonAsync()
        {
            var values = await _PersonCollection.Find(x => true).ToListAsync();
            return (_mapper.Map<List<ResultPersonDto>>(values));
        }

        public async Task<GetByIdPersonDto> GetByIdPersonAsync(string id)
        {
            var value = await _PersonCollection.Find<PhoneDirectory.Person.Entities.Person>(x => x.PersonID == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdPersonDto>(value);
        }

        public async Task UpdatePersonAsync(UpdatePersonDto updatePersonDto)
        {
            var value = _mapper.Map<PhoneDirectory.Person.Entities.Person>(updatePersonDto);
            await _PersonCollection.FindOneAndReplaceAsync(x => x.PersonID == updatePersonDto.PersonID, value);
        }
    }
}
