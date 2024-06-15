using PhoneDirectory.Person.Dtos.ContactInfoDtos;

namespace PhoneDirectory.Person.Services.ContactInfoService
{
    public interface IContactInfoService
    {
        Task<List<ResultContactInfoDto>> GetAllContactInfoAsync();
        Task CreateContactInfoAsync(CreateContactInfoDto createContactInfoDto);
        Task UpdateContactInfoAsync(UpdateContactInfoDto updateContactInfoDto);
        Task DeleteContactInfoAsync(string id);
        Task<GetByIdContactInfoDto> GetByIdContactInfoAsync(string id);
    }
}
