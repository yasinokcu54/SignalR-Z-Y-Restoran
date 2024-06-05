using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.TestimonialDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialController : ControllerBase
    {
        private readonly ITestimonialService _testimonialService;

        private readonly IMapper _mapper;

        public TestimonialController(ITestimonialService testimonialService, IMapper mapper)
        {
            _testimonialService = testimonialService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult TestimonialList()
        {
            var values = _mapper.Map<List<ResultTestimonialDto>>(_testimonialService.TGetListAll());

            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateTestimonial(CreateTestimonialDto createTestimonialDto)
        {
            _testimonialService.TAdd(new Testimonial()
            {
                Title = createTestimonialDto.Title,

                Comment = createTestimonialDto.Comment,

                ImageUrl = createTestimonialDto.ImageUrl,

                Name = createTestimonialDto.Name,

                Status = createTestimonialDto.Status,

            });

            return Ok("Müşteri Yorum Bilgisi Eklendi");

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTestimonial(int id)
        {
            var values = _testimonialService.TGetById(id);

            _testimonialService.TDelete(values);

            return Ok("Müşteri Yorum  Bilgisi Silindi");
        }

        [HttpPut]
        public IActionResult UpdateTestimonial(UpdateTestimonialDto updateTestimonialDto)
        {
            _testimonialService.TUpdate(new Testimonial()
            {
                Status = updateTestimonialDto.Status,

                Title = updateTestimonialDto.Title,

                Comment = updateTestimonialDto.Comment,

                Name = updateTestimonialDto.Name,

                ImageUrl = updateTestimonialDto.ImageUrl,

                TestimonialId = updateTestimonialDto.TestimonialId
            });

            return Ok("Müşteri Yorum  Bilgisi Güncellendi");
        }

        [HttpGet("{id}")]
        public IActionResult GetTestimonial(int id)
        {
            var values = _testimonialService.TGetById(id);

            return Ok(values);
        }
    }
}
