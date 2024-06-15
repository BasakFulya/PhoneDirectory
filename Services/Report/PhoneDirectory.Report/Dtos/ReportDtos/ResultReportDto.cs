using MongoDB.Bson;
using PhoneDirectory.Report.Entities;

namespace PhoneDirectory.Report.Dtos.ReportDtos
{
    public class ResultReportDto
    {
        public string ReportID { get; set; }
        public DateTime RequestedDate { get; set; }
        public ReportStatus Status { get; set; }
        public BsonDocument ReportContent { get; set; }
    }
}
