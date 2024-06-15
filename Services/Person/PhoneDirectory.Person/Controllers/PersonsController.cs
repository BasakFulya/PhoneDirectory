using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneDirectory.Person.Dtos.PersonDtos;
using PhoneDirectory.Person.Services.PersonService;

namespace PhoneDirectory.Person.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<IActionResult> PersonList()
        {
            var values = await _personService.GetAllPersonAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonById(string id)
        {
            var values = await _personService.GetByIdPersonAsync(id);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CratePerson(CreatePersonDto createPersonDto)
        {
            await _personService.CreatePersonAsync(createPersonDto);
            return Ok("Kişi başarıyla eklendi.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePerson(string id)
        {
            await _personService.DeletePersonAsync(id);
            return Ok("Kişi başarıyla silindi.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePerson(UpdatePersonDto updatePersonDto)
        {
            await _personService.UpdatePersonAsync(updatePersonDto);
            return Ok("Kişi başarıyla güncellendi.");
        }
    }
}
