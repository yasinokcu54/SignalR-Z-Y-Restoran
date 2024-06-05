using Microsoft.AspNetCore.SignalR;
using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;

namespace SignalRApi.Hubs
{
    public class SignalRHub : Hub
    {
        private readonly ICategoryService _categoryService;

        private readonly IProductService _productService;

        private readonly IOrderService _orderService;

        private readonly IMoneyCaseService _moneyCaseService;

        private readonly IMenuTableService _menuTableService;

        private readonly IBookingService _bookingService;

        private readonly INotificationService _notificationService;

        private readonly IMenuTableService _menuTableService1;

        public SignalRHub(ICategoryService categoryService, IProductService productService, IOrderService orderService, IMoneyCaseService moneyCaseService, IMenuTableService menuTableService, IBookingService bookingService, INotificationService notificationService, IMenuTableService menuTableService1)
        {
            _categoryService = categoryService;

            _productService = productService;

            _orderService = orderService;

            _moneyCaseService = moneyCaseService;

            _menuTableService = menuTableService;

            _bookingService = bookingService;

            _notificationService = notificationService;
            _menuTableService1 = menuTableService1;
        }
        public static int clientCount { get; set; } = 0;

        public async Task SendStatistic()
        {
            var values = _categoryService.TCategoryCount();
            await Clients.All.SendAsync("ReceiveCategoryCount", values);

            var values1 = _productService.TProductCount();
            await Clients.All.SendAsync("ReceiveProductCount", values1);

            var values2 = _categoryService.TActiveCategoryCount();
            await Clients.All.SendAsync("ReceiveActiveCategoryCount", values2);

            var values3 = _categoryService.TPassiveCategoryCount();
            await Clients.All.SendAsync("ReceivePassiveCategoryCount", values3);

            var values4 = _productService.TProductCountByCategoryNameHamburger();
            await Clients.All.SendAsync("ReceiveProductCountByCategoryNameHamburger", values4);

            var values5 = _productService.TProductCountByCategoryNameDrink();
            await Clients.All.SendAsync("ReceiveProductCountByCategoryNameDrink", values5);

            var values6 = _productService.TProductPriceAvg();
            await Clients.All.SendAsync("ReceiveProductPriceAvg", values6.ToString("0.00") + "₺");

            var values7 = _productService.TProductNameByMaxPrice();
            await Clients.All.SendAsync("ReceiveProductNameByMaxPrice", values7);

            var values8 = _productService.TProductNameByMinPrice();
            await Clients.All.SendAsync("ReceiveProductNameByMinPrice", values8);

            var values9 = _productService.TProductAvgPriceByHamburger();
            await Clients.All.SendAsync("ReceiveProductAvgPriceByHamburger", values9.ToString("0.00") + "₺");

            var values10 = _orderService.TTotalOrderCount();
            await Clients.All.SendAsync("ReceiveTotalOrderCount", values10);

            var values11 = _orderService.TActiveOrderCount();
            await Clients.All.SendAsync("ReceiveActiveOrderCount", values11);

            var values12 = _orderService.TLastOrderPrice();
            await Clients.All.SendAsync("ReceiveLastOrderPrice", values12.ToString("0.00") + "₺");

            var values13 = _moneyCaseService.TTotalMoneyCaseAmount();
            await Clients.All.SendAsync("ReceiveTotalMoneyCaseAmount", values13.ToString("0.00") + "₺");

            var values14 = _menuTableService.TMenuTableCount();
            await Clients.All.SendAsync("ReceiveMenuTableCount", values14);

        }

        public async Task SendProgress()
        {
            var values = _moneyCaseService.TTotalMoneyCaseAmount();
            await Clients.All.SendAsync("ReceiveTotalMoneyCaseAmount", values.ToString("0.00") + "₺");

            var values1 = _orderService.TActiveOrderCount();
            await Clients.All.SendAsync("ReceiveActiveOrderCount", values1);

            var values2 = _menuTableService.TMenuTableCount();
            await Clients.All.SendAsync("ReceiveMenuTableCount", values2);

            var values3 = _productService.TProductPriceAvg();
            await Clients.All.SendAsync("ReceiveProductPriceAvg", values3);

            var value4 = _productService.TProductAvgPriceByHamburger();
            await Clients.All.SendAsync("ReceiveAvgPriceByHamburger", value4);

            var value5 = _productService.TProductCountByCategoryNameDrink();
            await Clients.All.SendAsync("ReceiveProductCountByCategoryNameDrink", value5);

            var value6 = _orderService.TTotalOrderCount();
            await Clients.All.SendAsync("ReceiveTotalOrderCount", value6);

            var value7 = _productService.TProductPriceBySteakBurger();
            await Clients.All.SendAsync("ReceiveProductPriceBySteakBurger", value7);

            var value8 = _productService.TTotalPriceByDrinkCategory();
            await Clients.All.SendAsync("ReceiveTotalPriceByDrinkCategory", value8);

            var value9 = _productService.TTotalPriceBySaladCategory();
            await Clients.All.SendAsync("ReceiveTotalPriceBySaladCategory", value9);
        }

        public async Task GetBookingList()
        {
            var values = _bookingService.TGetListAll();

            await Clients.All.SendAsync("ReceiveBookingList", values);
        }

        public async Task SendNotification()
        {
            var values = _notificationService.TNotificationCountByStatusFalse();

            await Clients.All.SendAsync("ReceiveNotificationCountByStatusFalse", values);

            var notificationListByFalse = _notificationService.TGetAllNotificationByFalse();

            await Clients.All.SendAsync("ReceiveNotificationListByFalse", notificationListByFalse);
        }

        public async Task GetMenuTableStatus()
        {
            var values = _menuTableService.TGetListAll();

            await Clients.All.SendAsync("ReceiveMenuTableStatus", values);
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public override async Task OnConnectedAsync()
        {
            clientCount++;

            await Clients.All.SendAsync("ReceiveClientCount", clientCount);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            clientCount--;

            await Clients.All.SendAsync("ReceiveClientCount", clientCount);

            await base.OnDisconnectedAsync(exception);
        }



    }
}
