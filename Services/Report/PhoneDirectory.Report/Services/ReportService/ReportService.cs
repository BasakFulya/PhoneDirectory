using AutoMapper;
using MongoDB.Driver;
using PhoneDirectory.Person.Dtos.ContactInfoDtos;
using PhoneDirectory.Person.Dtos.PersonDtos;
using PhoneDirectory.Report.Settings;
using PhoneDirectory.Report.Dtos.ReportDtos;

namespace PhoneDirectory.Report.Services.ReportService
{
    public class ReportService:IReportService
    {
        private readonly IMongoCollection<PhoneDirectory.Report.Entities.Report> _ReportCollection;
        private readonly IMongoCollection<PhoneDirectory.Person.Entities.Person> _PersonCollection;
        private readonly IMongoCollection<PhoneDirectory.Person.Entities.ContactInfo> _ContactInfoCollection;
        private readonly IMapper _mapper;

        public ReportService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _ReportCollection = database.GetCollection<PhoneDirectory.Report.Entities.Report>(_databaseSettings.ReportCollectionName);
            _PersonCollection = database.GetCollection<PhoneDirectory.Person.Entities.Person>(_databaseSettings.PersonCollectionName);
            _ContactInfoCollection = database.GetCollection<PhoneDirectory.Person.Entities.ContactInfo>(_databaseSettings.ContactInfoCollectionName);
            _mapper = mapper;
        }

        public async Task CreateReportAsync(CreateReportDto createReportDto)
        {
            var value = _mapper.Map<PhoneDirectory.Report.Entities.Report>(createReportDto);
            await _ReportCollection.InsertOneAsync(value);
        }

        public async Task DeleteReportAsync(string id)
        {
            await _ReportCollection.DeleteOneAsync(x => x.ReportID == id);
        }

        public async Task<List<ResultReportDto>> GetAllReportAsync()
        {
            var values = await _ReportCollection.Find(x => true).ToListAsync();
            return (_mapper.Map<List<ResultReportDto>>(values));
        }

        public async Task<GetByIdReportDto> GetByIdReportAsync(string id)
        {
            var value = await _ReportCollection.Find<PhoneDirectory.Report.Entities.Report>(x => x.ReportID == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdReportDto>(value);
        }

        public async Task UpdateReportAsync(UpdateReportDto updateReportDto)
        {
            var value = _mapper.Map<PhoneDirectory.Report.Entities.Report>(updateReportDto);
            await _ReportCollection.FindOneAndReplaceAsync(x => x.ReportID == updateReportDto.ReportID, value);
        }

        public async Task<List<ResultPersonDto>> GetAllPersonAsync()
        {
            var values = await _PersonCollection.Find(x => true).ToListAsync();
            return (_mapper.Map<List<ResultPersonDto>>(values));
        }

        public async Task<List<ResultContactInfoDto>> GetAllContactInfoAsync()
        {
            var values = await _ContactInfoCollection.Find(x => true).ToListAsync();
            return (_mapper.Map<List<ResultContactInfoDto>>(values));
        }
    }
}
