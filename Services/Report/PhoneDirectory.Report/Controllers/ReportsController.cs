using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneDirectory.Person.Entities;
using PhoneDirectory.Report.Dtos.ReportDtos;
using PhoneDirectory.Report.Services.ReportService;

namespace PhoneDirectory.Report.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _ReportService;

        public ReportsController(IReportService ReportService)
        {
            _ReportService = ReportService;
        }

        [HttpGet]
        public async Task<IActionResult> ReportList()
        {
            var values = await _ReportService.GetAllReportAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReportById(string id)
        {
            var values = await _ReportService.GetByIdReportAsync(id);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CrateReport(CreateReportDto createReportDto)
        {
            await _ReportService.CreateReportAsync(createReportDto);
            return Ok("Report Initialized!");
        }

        [HttpPost]
        [Route("api/Report/RequestReport")]
        public async Task<IActionResult> RequestReport()
        {
            var persons = _ReportService.GetAllPersonAsync();
            var contactInfos = _ReportService.GetAllContactInfoAsync();


            var locations = contactInfos.Result.Where(x => x.Type == Person.Entities.ContactType.Location).ToList();
            var uniqueLocation = locations.GroupBy(x => x.Content).Select(y => y.Key);
            var numberOfPhoneNumbers = contactInfos.Result.Where(x => x.Type == Person.Entities.ContactType.PhoneNumber).ToList();
            List<ReportResultDto> result = new List<ReportResultDto>();


            foreach (var location in uniqueLocation)
            { 
                var personIDsInLocation = contactInfos.Result.Where(x => x.Content == location).Select(y => y.PersonID).ToList();
                var numberOfPersonInLocation = contactInfos.Result.Where(x => x.Content == location).Count();
                var numberOfPhoneNumberInLocation = numberOfPhoneNumbers.Where(x => personIDsInLocation.Contains(x.PersonID)).Count();
                result.Add(new ReportResultDto { Location = location, NumberOfPerson = numberOfPersonInLocation, NumberOfPhoneNumber = numberOfPhoneNumberInLocation });
            }

            var createReportDto = new CreateReportDto
            {
                RequestedDate = DateTime.Now,
                Status = Entities.ReportStatus.InProgress,
                ReportContent = result
            };
            await _ReportService.CreateReportAsync(createReportDto);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteReport(string id)
        {
            await _ReportService.DeleteReportAsync(id);
            return Ok("Report Deleted!");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateReport(UpdateReportDto updateReportDto)
        {
            await _ReportService.UpdateReportAsync(updateReportDto);
            return Ok("Report Updated!");
        }
    }
}
