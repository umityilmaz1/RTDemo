﻿using Api.Model.Contact;
using Api.Models;
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
    public class ContactController : ControllerBase
    {

        private readonly ILogger<ContactController> _logger;
        private readonly IMapper _mapper;
        private readonly IContactService _contactService;
        private readonly IContactInformationService _contactInformationService;

        public ContactController(ILogger<ContactController> logger, IMapper mapper, IContactService contactService, IContactInformationService contactInformationService)
        {
            _logger = logger;
            _mapper = mapper;
            _contactService = contactService;
            _contactInformationService = contactInformationService;
        }

        [HttpPost(nameof(Create))]
        public IActionResult Create(ContactCreateDto dto) 
        { 
            Contact contact = _mapper.Map<Contact>(dto);
            _contactService.Create(contact);
            return Ok();
        }

        [HttpPut(nameof(Update))]
        public IActionResult Update(ContactUpdateDto dto)
        {
            Contact contact = _contactService.GetById(dto.Id);

            if (contact == null)
            {
                return NotFound();
            }

            _mapper.Map(dto, contact);
            _contactService.Update(contact);
            return Ok();
        }

        [HttpDelete(nameof(Delete))]
        public IActionResult Delete(Guid id)
        {
            Contact contact = _contactService.GetById(id);

            if (contact == null)
            {
                return NotFound();
            }

            _contactService.Delete(contact);
            return Ok();
        }

        [HttpGet(nameof(GetContacts))]
        public IActionResult GetContacts()
        {
            IList<Contact> contacts = _contactService.GetList().ToList();
            IList<ContactListDto> contactsDto = _mapper.Map<IList<ContactListDto>>(contacts);
            return new JsonResult(contactsDto);
        }

        [HttpGet(nameof(GetContactsWithContactInformations))]
        public IActionResult GetContactsWithContactInformations(Guid contactId)
        {
            Contact contact = _contactService.GetById(contactId);
            ContactListWithContactInformationsDto contactDto = _mapper.Map<ContactListWithContactInformationsDto>(contact);
            return new JsonResult(contactDto);
        }

        [HttpGet(nameof(GetReport))]
        public IActionResult GetReport()
        {
            IList<Contact> contacts = _contactService.GetList().ToList();
            IList<string> locations = contacts.Select(a => a.Location).Distinct().ToList();
            List<ReportDetailDto> reportData = new();

            foreach (string location in locations)
            {
                IList<Contact> contactInSpesificLocation = contacts.Where(a => a.Location == location).ToList();
                ReportDetailDto data = new()
                {
                    Location = location,
                    ContactCount = contactInSpesificLocation.Count(),
                    PhoneNumberCount = contactInSpesificLocation.Sum(a => a.ContactInformations.Where(a => a.IsActive).Count(b => b.Type == Enums.ContactInformationType.Phone))
                };
                reportData.Add(data);
            }

            return new JsonResult(reportData);
        }
    }
}
