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
    }
}