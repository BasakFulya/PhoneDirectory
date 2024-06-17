using Confluent.Kafka;
using PhoneDirectory.Report.Dtos.ReportDtos;
using PhoneDirectory.Report.Services.ReportService;
using System.Text.Json;

namespace PhoneDirectory.Report.Services.KafkaService
{
    public class KafkaConsumerService:BackgroundService
    {
        private readonly IReportService _reportService;

        public KafkaConsumerService(IReportService reportService)
        {
            _reportService = reportService;
        }

        private readonly string _topic;
        private readonly IConsumer<Null, string> _consumer;

        public KafkaConsumerService(IConfiguration configuration)
        {
            var config = new ConsumerConfig
            {
                GroupId = configuration["Kafka:GroupId"],
                BootstrapServers = configuration["Kafka:BootstrapServers"],
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            _topic = configuration["Kafka:Topic"];
            _consumer = new ConsumerBuilder<Null, string>(config).Build();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _consumer.Subscribe(_topic);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var cr = _consumer.Consume(stoppingToken);
                    
                        await ProcessReportRequestAsync();
                    
                }
                catch (ConsumeException e)
                {
                    Console.WriteLine($"Error occurred: {e.Error.Reason}");
                }
            }
        }

        private async Task ProcessReportRequestAsync()
        {
            //Report occuring process
            var createReportDto = new CreateReportDto
            {
                RequestedDate = DateTime.Now,
                Status = Entities.ReportStatus.InProgress,
                ReportContent = ReportResult()
            };

            // Report recording on Database
            SaveReport(createReportDto);

            //Report Status is updating as "Completed"
            var reports = _reportService.GetAllReportAsync();
            var report = reports.Result.Where(b => b.RequestedDate == createReportDto.RequestedDate).FirstOrDefault();
            if (report != null) 
            {
                var updateReportDto = new UpdateReportDto { ReportContent = report.ReportContent, ReportID = report.ReportID, RequestedDate = report.RequestedDate, Status = report.Status };
                updateReportDto.Status = Entities.ReportStatus.Completed;
                UpdateReportStatus(updateReportDto);
            }
            
        }

        private List<ReportResultDto> ReportResult()
        {
            var persons = _reportService.GetAllPersonAsync();
            var contactInfos = _reportService.GetAllContactInfoAsync();


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
            return result;
        }

        private async void SaveReport(CreateReportDto createReportDto)
        {
            // Record report to Database

            await _reportService.CreateReportAsync(createReportDto);
        }

        private async void UpdateReportStatus(UpdateReportDto updateReportDto)
        {
            // Update Report Status as "Completed"
            await _reportService.UpdateReportAsync(updateReportDto);
        }

    }
}
