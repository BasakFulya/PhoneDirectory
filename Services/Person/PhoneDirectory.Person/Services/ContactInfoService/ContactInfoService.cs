using AutoMapper;
using MongoDB.Driver;
using PhoneDirectory.Person.Dtos.ContactInfoDtos;
using PhoneDirectory.Person.Entities;
using PhoneDirectory.Person.Settings;

namespace PhoneDirectory.Person.Services.ContactInfoService
{
    public class ContactInfoService:IContactInfoService
    {
        private readonly IMongoCollection<ContactInfo> _ContactInfoCollection;
        private readonly IMapper _mapper;

        public ContactInfoService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _ContactInfoCollection = database.GetCollection<ContactInfo>(_databaseSettings.ContactInfoCollectionName);
            _mapper = mapper;
        }

        public async Task CreateContactInfoAsync(CreateContactInfoDto createContactInfoDto)
        {
            var value = _mapper.Map<ContactInfo>(createContactInfoDto);
            await _ContactInfoCollection.InsertOneAsync(value);
        }

        public async Task DeleteContactInfoAsync(string id)
        {
            await _ContactInfoCollection.DeleteOneAsync(x => x.ContactInfoID == id);
        }

        public async Task<List<ResultContactInfoDto>> GetAllContactInfoAsync()
        {
            var values = await _ContactInfoCollection.Find(x => true).ToListAsync();
            return (_mapper.Map<List<ResultContactInfoDto>>(values));
        }

        public async Task<GetByIdContactInfoDto> GetByIdContactInfoAsync(string id)
        {
            var value = await _ContactInfoCollection.Find<ContactInfo>(x => x.ContactInfoID == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdContactInfoDto>(value);
        }

        public async Task UpdateContactInfoAsync(UpdateContactInfoDto updateContactInfoDto)
        {
            var value = _mapper.Map<ContactInfo>(updateContactInfoDto);
            await _ContactInfoCollection.FindOneAndReplaceAsync(x => x.ContactInfoID == updateContactInfoDto.ContactInfoID, value);
        }
    }
}
