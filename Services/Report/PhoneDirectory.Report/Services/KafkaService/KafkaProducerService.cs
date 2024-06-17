using Confluent.Kafka;
using System.Text.Json;

namespace PhoneDirectory.Report.Services.KafkaService
{
    public class KafkaProducerService
    {
        private readonly string _topic;
        private readonly IProducer<Null, string> _producer;

        public KafkaProducerService(IConfiguration configuration)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = configuration["Kafka:BootstrapServers"]
            };
            _topic = configuration["Kafka:Topic"];
            _producer = new ProducerBuilder<Null, string>(config).Build();
        }

        public async Task SendReportRequestAsync()
        {
            await _producer.ProduceAsync(_topic, new Message<Null, string> { Value = "Report initialized!" });
        }

    }
}
