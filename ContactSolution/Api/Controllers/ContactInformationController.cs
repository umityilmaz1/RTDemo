using Api.Model.Contact;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Service.Abstract;
using Service.Concrete;
using System.Runtime.CompilerServices;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactInformationController : ControllerBase
    {

        private readonly ILogger<ContactInformationController> _logger;
        private readonly IMapper _mapper;
        private readonly IContactInformationService _contactInformationService;
        private readonly IContactService _contactService;

        public ContactInformationController(ILogger<ContactInformationController> logger, IMapper mapper, IContactInformationService contactInformationService, IContactService contactService)
        {
            _logger = logger;
            _mapper = mapper;
            _contactInformationService = contactInformationService;
            _contactService = contactService;
        }

        [HttpPost(nameof(Create))]
        public IActionResult Create(ContactInformationCreateDto dto) 
        { 
            ContactInformation contactInformation = _mapper.Map<ContactInformation>(dto);
            _contactInformationService.Create(contactInformation);
            return Ok();
        }

        [HttpDelete(nameof(Delete))]
        public IActionResult Delete(Guid id)
        {
            ContactInformation contactInformation = _contactInformationService.GetById(id);

            if (contactInformation == null)
            {
                return NotFound();
            }

            _contactInformationService.Delete(contactInformation);
            return Ok();
        }   
    }
}
