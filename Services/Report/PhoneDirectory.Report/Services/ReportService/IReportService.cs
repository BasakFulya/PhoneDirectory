using PhoneDirectory.Person.Dtos.ContactInfoDtos;
using PhoneDirectory.Person.Dtos.PersonDtos;
using PhoneDirectory.Report.Dtos.ReportDtos;

namespace PhoneDirectory.Report.Services.ReportService
{
    public interface IReportService
    {
        Task<List<ResultReportDto>> GetAllReportAsync();
        Task CreateReportAsync(CreateReportDto createReportDto);
        Task UpdateReportAsync(UpdateReportDto updateReportDto);
        Task DeleteReportAsync(string id);
        Task<GetByIdReportDto> GetByIdReportAsync(string id);
        Task<List<ResultPersonDto>> GetAllPersonAsync();
        Task<List<ResultContactInfoDto>> GetAllContactInfoAsync();
    }
}
