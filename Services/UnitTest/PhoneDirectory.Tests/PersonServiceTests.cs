using AutoMapper;
using MongoDB.Driver;
using Moq;
using PhoneDirectory.Person.Dtos.PersonDtos;
using PhoneDirectory.Person.Entities;
using PhoneDirectory.Person.Services.PersonService;
using PhoneDirectory.Person.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PhoneDirectory.Tests
{
    public class PersonServiceTests
    {
        private readonly Mock<IMongoCollection<PhoneDirectory.Person.Entities.Person>> _personCollectionMock;

        private readonly Mock<IMapper> _mapperMock;
        private readonly PersonService _personService;
        public PersonServiceTests(IDatabaseSettings _databaseSettings)
        {
            _mapperMock = new Mock<IMapper>();

            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _personCollectionMock = (Mock<IMongoCollection<Person.Entities.Person>>)database.GetCollection<PhoneDirectory.Person.Entities.Person>(_databaseSettings.PersonCollectionName);
        }

        [Fact]
        public async Task CreatePersonAsync_ShouldInsertPerson()
        {
            // Arrange
            var createPersonDto = new CreatePersonDto();
            var person = new PhoneDirectory.Person.Entities.Person();
            _mapperMock.Setup(m => m.Map<PhoneDirectory.Person.Entities.Person>(createPersonDto)).Returns(person);

            // Act
            await _personService.CreatePersonAsync(createPersonDto);

            // Assert
            _personCollectionMock.Verify(c => c.InsertOneAsync(person, null, default), Times.Once);
        }

        [Fact]
        public async Task DeletePersonAsync_ShouldDeletePerson()
        {
            // Arrange
            var id = "some-id";

            // Act
            await _personService.DeletePersonAsync(id);

            // Assert
            _personCollectionMock.Verify(c => c.DeleteOneAsync(It.IsAny<FilterDefinition<PhoneDirectory.Person.Entities.Person>>(), default), Times.Once);
        }

        [Fact]
        public async Task GetAllPersonAsync_ShouldReturnAllPersons()
        {
            // Arrange
            var persons = new List<PhoneDirectory.Person.Entities.Person> { new PhoneDirectory.Person.Entities.Person(), new PhoneDirectory.Person.Entities.Person() };
            _personCollectionMock.Setup(c => c.Find(It.IsAny<FilterDefinition<PhoneDirectory.Person.Entities.Person>>(), null).ToListAsync(default)).ReturnsAsync(persons);
            _mapperMock.Setup(m => m.Map<List<ResultPersonDto>>(persons)).Returns(new List<ResultPersonDto>());

            // Act
            var result = await _personService.GetAllPersonAsync();

            // Assert
            Assert.NotNull(result);
            _personCollectionMock.Verify(c => c.Find(It.IsAny<FilterDefinition<PhoneDirectory.Person.Entities.Person>>(), null).ToListAsync(default), Times.Once);
        }

        [Fact]
        public async Task GetByIdPersonAsync_ShouldReturnPerson()
        {
            // Arrange
            var id = "some-id";
            var person = new PhoneDirectory.Person.Entities.Person();
            _personCollectionMock.Setup(c => c.Find(It.IsAny<FilterDefinition<PhoneDirectory.Person.Entities.Person>>(), null).FirstOrDefaultAsync(default)).ReturnsAsync(person);
            _mapperMock.Setup(m => m.Map<GetByIdPersonDto>(person)).Returns(new GetByIdPersonDto());

            // Act
            var result = await _personService.GetByIdPersonAsync(id);

            // Assert
            Assert.NotNull(result);
            _personCollectionMock.Verify(c => c.Find(It.IsAny<FilterDefinition<PhoneDirectory.Person.Entities.Person>>(), null).FirstOrDefaultAsync(default), Times.Once);
        }

        [Fact]
        public async Task UpdatePersonAsync_ShouldUpdatePerson()
        {
            // Arrange
            var updatePersonDto = new UpdatePersonDto();
            var person = new PhoneDirectory.Person.Entities.Person();
            _mapperMock.Setup(m => m.Map<PhoneDirectory.Person.Entities.Person>(updatePersonDto)).Returns(person);

            // Act
            await _personService.UpdatePersonAsync(updatePersonDto);

            // Assert
            _personCollectionMock.Verify(c => c.FindOneAndReplaceAsync(It.IsAny<FilterDefinition<PhoneDirectory.Person.Entities.Person>>(), person, null, default), Times.Once);
        }
    }
}
