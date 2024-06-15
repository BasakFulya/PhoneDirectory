using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json;

namespace PhoneDirectory.Report.Entities
{
    public class Report
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ReportID { get; set; }
        public DateTime RequestedDate { get; set; }
        public ReportStatus Status { get; set; }
        public BsonDocument ReportContent { get; set; }
    }
    public enum ReportStatus
    {
        InProgress,
        Completed
    }
}
