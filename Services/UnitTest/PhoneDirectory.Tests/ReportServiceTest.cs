using AutoMapper;
using MongoDB.Driver;
using Moq;
using PhoneDirectory.Person.Entities;
using PhoneDirectory.Report.Dtos.ReportDtos;
using PhoneDirectory.Report.Services.ReportService;
using PhoneDirectory.Report.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneDirectory.Tests
{
    public class ReportServiceTest
    {
        private readonly Mock<IMongoCollection<PhoneDirectory.Report.Entities.Report>> _reportCollectionMock;
        private readonly Mock<IMongoCollection<PhoneDirectory.Person.Entities.Person>> _personCollectionMock;
        private readonly Mock<IMongoCollection<ContactInfo>> _contactInfoCollectionMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ReportService _reportService;

        public ReportServiceTest(IDatabaseSettings _databaseSettings)
        {
            _mapperMock = new Mock<IMapper>();

            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _reportCollectionMock = (Mock<IMongoCollection<Report.Entities.Report>>)database.GetCollection<PhoneDirectory.Report.Entities.Report>(_databaseSettings.ReportCollectionName);
            _personCollectionMock = (Mock<IMongoCollection<Person.Entities.Person>>)database.GetCollection<PhoneDirectory.Person.Entities.Person>(_databaseSettings.PersonCollectionName);
            _contactInfoCollectionMock = (Mock<IMongoCollection<ContactInfo>>)database.GetCollection<PhoneDirectory.Person.Entities.ContactInfo>(_databaseSettings.ContactInfoCollectionName);
        }

        [Fact]
        public async Task CreateReportAsync_Should_Call_InsertOneAsync()
        {
            // Arrange
            var createReportDto = new CreateReportDto();
            var report = new PhoneDirectory.Report.Entities.Report();
            _mapperMock.Setup(m => m.Map<PhoneDirectory.Report.Entities.Report>(createReportDto)).Returns(report);

            // Act
            await _reportService.CreateReportAsync(createReportDto);

            // Assert
            _reportCollectionMock.Verify(x => x.InsertOneAsync(report, null, default), Times.Once);
        }

        [Fact]
        public async Task DeleteReportAsync_Should_Call_DeleteOneAsync()
        {
            // Arrange
            var id = "123";

            // Act
            await _reportService.DeleteReportAsync(id);

            // Assert
            _reportCollectionMock.Verify(x => x.DeleteOneAsync(It.IsAny<FilterDefinition<PhoneDirectory.Report.Entities.Report>>(), default), Times.Once);
        }

    }
}
