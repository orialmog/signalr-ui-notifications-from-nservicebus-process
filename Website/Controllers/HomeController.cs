using System;
using System.Web.Mvc;
using NServiceBus;
using SomeLongRunningProcess.TypeContracts;
using UINotifications.TypeContracts;

namespace WaitForEventLocking.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBus _bus;

        public HomeController(IBus bus)
        {
            _bus = bus;
        }
         
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult StartSomeProcess(ViewModel model)
        {
            _bus.Send(new SomeLongRunningProcessCommand()
            {
                MessageId = Guid.NewGuid(),
                UIElementId = model.Id 
            });
            return Json(new { State = UIStateEnum.Busy.ToString() });
        }

    }
}