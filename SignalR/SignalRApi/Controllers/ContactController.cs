﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.ContactDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        private readonly IMapper _mapper;

        public ContactController(IContactService contactService, IMapper mapper)
        {
            _contactService = contactService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult ContactList()
        {
            var values = _mapper.Map<List<ResultContactDto>>(_contactService.TGetListAll());

            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateContact(CreateContactDto createContactDto)
        {
            _contactService.TAdd(new Contact()
            {
                FooterDescription = createContactDto.FooterDescription,

                Location = createContactDto.Location,

                Mail = createContactDto.Mail,

                Phone = createContactDto.Phone,

                FooterTitle = createContactDto.FooterTitle,

                OpenDays = createContactDto.OpenDays,

                OpenDaysDescription = createContactDto.OpenDaysDescription,

                OpenHours = createContactDto.OpenHours
            });

            return Ok("İletişim Bilgisi Eklendi");

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteContact(int id)
        {
            var values = _contactService.TGetById(id);

            _contactService.TDelete(values);

            return Ok("İletişim Bilgisi Silindi");
        }

        [HttpPut]
        public IActionResult UpdateContact(UpdateContactDto updateContactDto)
        {
            _contactService.TUpdate(new Contact()
            {
                ContactId = updateContactDto.ContactId,

                FooterDescription = updateContactDto.FooterDescription,

                Location = updateContactDto.Location,

                Mail = updateContactDto.Mail,

                Phone = updateContactDto.Phone,

                FooterTitle = updateContactDto.FooterTitle,

                OpenDays = updateContactDto.OpenDays,

                OpenDaysDescription= updateContactDto.OpenDaysDescription,

                OpenHours = updateContactDto.OpenHours
            });

            return Ok("İletişim Bilgisi Güncellendi");
        }

        [HttpGet("{id}")]
        public IActionResult GetContact(int id)
        {
            var values = _contactService.TGetById(id);

            return Ok(values);
        }
    }
}
