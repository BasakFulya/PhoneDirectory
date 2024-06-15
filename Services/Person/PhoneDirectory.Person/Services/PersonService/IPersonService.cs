using PhoneDirectory.Person.Dtos.PersonDtos;

namespace PhoneDirectory.Person.Services.PersonService
{
    public interface IPersonService
    {
        Task<List<ResultPersonDto>> GetAllPersonAsync();
        Task CreatePersonAsync(CreatePersonDto createPersonDto);
        Task UpdatePersonAsync(UpdatePersonDto updatePersonDto);
        Task DeletePersonAsync(string id);
        Task<GetByIdPersonDto> GetByIdPersonAsync(string id);
    }
}
