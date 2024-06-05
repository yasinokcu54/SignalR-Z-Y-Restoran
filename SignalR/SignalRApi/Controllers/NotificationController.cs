using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.NotificationDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        public IActionResult NotificationList()
        {
            return Ok(_notificationService.TGetListAll());
        }

        [HttpGet("NotificationCountByStatusFalse")]
        public IActionResult NotificationCountByStatusFalse()
        {
            return Ok(_notificationService.TNotificationCountByStatusFalse());
        }

        [HttpGet("GetAllNotificationByFalse")]
        public IActionResult GetAllNotificationByFalse()
        {
            return Ok(_notificationService.TGetAllNotificationByFalse());
        }

        [HttpPost]
        public IActionResult CreateNotification(CreateNotificationDto createNotificationDto)
        {
            Notification notification = new Notification()
            {
                Description = createNotificationDto.Description,

                Icon = createNotificationDto.Icon,

                Status =false,

                Type = createNotificationDto.Type,

                Date = Convert.ToDateTime(DateTime.Now.ToShortDateString())
            };

            _notificationService.TAdd(notification);

            return Ok("Ekleme İşlemi Başarıyla Yapıldı..");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteNotification(int id)
        {
            var values = _notificationService.TGetById(id);

            _notificationService.TDelete(values);

            return Ok("Bildirim Silindi..");
        }

        [HttpGet("{id}")]
        public IActionResult GetNotification(int id)
        {
            var values = _notificationService.TGetById(id);

            return Ok(values);
        }

        [HttpPut]
        public IActionResult UpdateNotification(UpdateNotificationDto updateNotificationDto)
        {
            Notification notification = new Notification()
            {
                NotificationId = updateNotificationDto.NotificationId,

                Description = updateNotificationDto.Description,

                Icon = updateNotificationDto.Icon,

                Status = updateNotificationDto.Status,

                Type = updateNotificationDto.Type,

                Date = updateNotificationDto.Date,
            };

            _notificationService.TUpdate(notification);

            return Ok("Güncelleme İşlemi Başarıyla Yapıldı..");
        }


        [HttpGet("NotificationStatusToFalse/{id}")]
        public IActionResult NotificationStatusToFalse(int id)
        {
            _notificationService.TNotificationStatusToFalse(id);

            return Ok("Güncelleme Yapıldı");
        }

        [HttpGet("NotificationStatusToTrue/{id}")]
        public IActionResult NotificationStatusToTrue(int id)
        {
            _notificationService.TNotificationStatusToTrue(id);

            return Ok("Güncelleme Yapıldı");
        }
    }
}
