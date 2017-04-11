using System.Web.Mvc;
using CattleEnd.ServiceLayer.Services;

namespace CattleEnd.Web.Controllers
{
    public class SchedulesController : Controller
    {
        private WebService webService = new WebService();

        public ActionResult Index()
        {
            var schedules = webService.GetAllSchedules();
            return View(schedules);
        }

        public ActionResult Clear()
        {
            return View();
        }

        [HttpPost, ActionName("Clear")]
        [ValidateAntiForgeryToken]
        public ActionResult ClearConfirmed()
        {
            var isCleared = webService.ClearSchedule();
            if (isCleared)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Clear");
        }
    }
}