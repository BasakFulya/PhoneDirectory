using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            var createReportDto = new CreateReportDto
            {
                RequestedDate = DateTime.Now,
                Status = Entities.ReportStatus.InProgress
            };
            await _ReportService.CreateReportAsync(createReportDto);

            var Persons = _ReportService.GetAllPersonAsync();
            var Locations = _ReportService.GetAllContactInfoAsync();

            //var Persons = _PersonService.GetAllPersonAsync();
            //var ContactInfos = _ContactInfoService.GetAllContactInfoAsync();
            //var locations = ContactInfos.Result.Where(x => x.Type == Person.Entities.ContactType.Location).ToList();
            //var numberOfPersons = locations.GroupBy(x => x.Content);

            return Ok("Report Initiliazed!");
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
