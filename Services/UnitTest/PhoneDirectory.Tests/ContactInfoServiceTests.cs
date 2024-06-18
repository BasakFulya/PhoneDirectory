using AutoMapper;
using MongoDB.Driver;
using Moq;
using PhoneDirectory.Person.Dtos.ContactInfoDtos;
using PhoneDirectory.Person.Entities;
using PhoneDirectory.Person.Services.ContactInfoService;
using PhoneDirectory.Person.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PhoneDirectory.Tests
{
    public class ContactInfoServiceTests
    {
        private readonly Mock<IMongoCollection<ContactInfo>> _contactInfoCollectionMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ContactInfoService _contactInfoService;

        public ContactInfoServiceTests(IDatabaseSettings _databaseSettings)
        {
            _contactInfoCollectionMock = new Mock<IMongoCollection<ContactInfo>>();
            _mapperMock = new Mock<IMapper>();

            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _contactInfoCollectionMock = (Mock<IMongoCollection<ContactInfo>>)database.GetCollection<PhoneDirectory.Person.Entities.ContactInfo>(_databaseSettings.ContactInfoCollectionName);
        }

        [Fact]
        public async Task CreateContactInfoAsync_ShouldInsertContactInfo()
        {
            // Arrange
            var createContactInfoDto = new CreateContactInfoDto();
            var contactInfo = new ContactInfo();
            _mapperMock.Setup(m => m.Map<ContactInfo>(createContactInfoDto)).Returns(contactInfo);

            // Act
            await _contactInfoService.CreateContactInfoAsync(createContactInfoDto);

            // Assert
            _contactInfoCollectionMock.Verify(c => c.InsertOneAsync(contactInfo, null, default), Times.Once);
        }

        [Fact]
        public async Task DeleteContactInfoAsync_ShouldDeleteContactInfo()
        {
            // Arrange
            var id = "some-id";

            // Act
            await _contactInfoService.DeleteContactInfoAsync(id);

            // Assert
            _contactInfoCollectionMock.Verify(c => c.DeleteOneAsync(It.IsAny<FilterDefinition<ContactInfo>>(), default), Times.Once);
        }

        [Fact]
        public async Task GetAllContactInfoAsync_ShouldReturnAllContactInfo()
        {
            // Arrange
            var contactInfos = new List<ContactInfo> { new ContactInfo(), new ContactInfo() };
            _contactInfoCollectionMock.Setup(c => c.Find(It.IsAny<FilterDefinition<ContactInfo>>(), null).ToListAsync(default)).ReturnsAsync(contactInfos);
            _mapperMock.Setup(m => m.Map<List<ResultContactInfoDto>>(contactInfos)).Returns(new List<ResultContactInfoDto>());

            // Act
            var result = await _contactInfoService.GetAllContactInfoAsync();

            // Assert
            Assert.NotNull(result);
            _contactInfoCollectionMock.Verify(c => c.Find(It.IsAny<FilterDefinition<ContactInfo>>(), null).ToListAsync(default), Times.Once);
        }

        [Fact]
        public async Task GetByIdContactInfoAsync_ShouldReturnContactInfo()
        {
            // Arrange
            var id = "some-id";
            var contactInfo = new ContactInfo();
            _contactInfoCollectionMock.Setup(c => c.Find(It.IsAny<FilterDefinition<ContactInfo>>(), null).FirstOrDefaultAsync(default)).ReturnsAsync(contactInfo);
            _mapperMock.Setup(m => m.Map<GetByIdContactInfoDto>(contactInfo)).Returns(new GetByIdContactInfoDto());

            // Act
            var result = await _contactInfoService.GetByIdContactInfoAsync(id);

            // Assert
            Assert.NotNull(result);
            _contactInfoCollectionMock.Verify(c => c.Find(It.IsAny<FilterDefinition<ContactInfo>>(), null).FirstOrDefaultAsync(default), Times.Once);
        }

        [Fact]
        public async Task UpdateContactInfoAsync_ShouldUpdateContactInfo()
        {
            // Arrange
            var updateContactInfoDto = new UpdateContactInfoDto();
            var contactInfo = new ContactInfo();
            _mapperMock.Setup(m => m.Map<ContactInfo>(updateContactInfoDto)).Returns(contactInfo);

            // Act
            await _contactInfoService.UpdateContactInfoAsync(updateContactInfoDto);

            // Assert
            _contactInfoCollectionMock.Verify(c => c.FindOneAndReplaceAsync(It.IsAny<FilterDefinition<ContactInfo>>(), contactInfo, null, default), Times.Once);
        }
    }
}
