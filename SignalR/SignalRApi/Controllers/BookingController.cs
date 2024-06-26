﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.BookingDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public IActionResult BookingList()
        {
            var values = _bookingService.TGetListAll();

            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateBooking(CreateBookingDto createBookingDto)
        {
            Booking booking = new Booking()
            {
                Mail = createBookingDto.Mail,

                Name = createBookingDto.Name,

                PersonCount = createBookingDto.PersonCount,

                Phone = createBookingDto.Phone,

                Date = createBookingDto.Date,

                Description = createBookingDto.Description,
            };

            _bookingService.TAdd(booking);

            return Ok("Rezervasyon Eklendi..");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBooking(int id)
        {
            var values = _bookingService.TGetById(id);

            _bookingService.TDelete(values);

            return Ok("Rezervasyon Silindi..");
        }

        [HttpPut]
        public IActionResult UpdateBooking(UpdateBookingDto updateBookingDto)
        {
            Booking booking = new()
            {
                Mail = updateBookingDto.Mail,

                BookingId = updateBookingDto.BookingId,

                Name = updateBookingDto.Name,

                PersonCount= updateBookingDto.PersonCount,

                Phone = updateBookingDto.Phone,

                Date = updateBookingDto.Date,

                Description= updateBookingDto.Description,
            };

            _bookingService.TUpdate(booking);

            return Ok("Rezervasyon Güncellendi");


        }

        [HttpGet("{id}")]
        public IActionResult GetBooking(int id)
        {
            var values = _bookingService.TGetById(id);

            return Ok(values);
        }

        [HttpGet("BookingStatusApproved/{id}")]
        public IActionResult BookingStatusApproved(int id)
        {
            _bookingService.TBookingStatusApproved(id);

            return Ok("Rezervasyon Açıklaması Değiştirildi..");
        }

        [HttpGet("BookingStatusCancelled/{id}")]
        public IActionResult BookingStatusCancelled(int id)
        {
            _bookingService.TBookingStatusCancelled(id);

			return Ok("Rezervasyon Açıklaması Değiştirildi..");
		}

	}
}
