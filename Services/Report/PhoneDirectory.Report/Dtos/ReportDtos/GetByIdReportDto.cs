using MongoDB.Bson;
using PhoneDirectory.Report.Entities;

namespace PhoneDirectory.Report.Dtos.ReportDtos
{
    public class GetByIdReportDto
    {
        public string ReportID { get; set; }
        public DateTime RequestedDate { get; set; }
        public ReportStatus Status { get; set; }
        public List<ReportResultDto> ReportContent { get; set; }
    }
}
