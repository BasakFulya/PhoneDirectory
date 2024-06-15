using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneDirectory.Person.Dtos.ContactInfoDtos;
using PhoneDirectory.Person.Services.ContactInfoService;

namespace PhoneDirectory.Person.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactInfosController : ControllerBase
    {
        private readonly IContactInfoService _contactInfoService;

        public ContactInfosController(IContactInfoService contactInfoService)
        {
            _contactInfoService = contactInfoService;
        }

        [HttpGet]
        public async Task<IActionResult> ContactInfoList()
        {
            var values = await _contactInfoService.GetAllContactInfoAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactInfoById(string id)
        {
            var values = await _contactInfoService.GetByIdContactInfoAsync(id);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CrateContactInfo(CreateContactInfoDto createContactInfoDto)
        {
            await _contactInfoService.CreateContactInfoAsync(createContactInfoDto);
            return Ok("İletişim bilgileri başarıyla eklendi.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteContactInfo(string id)
        {
            await _contactInfoService.DeleteContactInfoAsync(id);
            return Ok("İletişim bilgileri başarıyla silindi.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateContactInfo(UpdateContactInfoDto updateContactInfoDto)
        {
            await _contactInfoService.UpdateContactInfoAsync(updateContactInfoDto);
            return Ok("İletişim bilgileri başarıyla güncellendi.");
        }
    }
}
