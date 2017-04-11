using CattleEnd.ServiceLayer.Services;
using CattleEnd.SharedModels.Models;
using System.Web.Mvc;

namespace CattleEnd.Web.Controllers
{
    public class WarriorsController : Controller
    {
        private WebService webService = new WebService();

        public ActionResult Index()
        {
            var warriors = webService.GetAllWarriors();
            return View(warriors);
        }

        public ActionResult Create()
        {
            var species = webService.GetWarriorSpecies();
            ViewBag.Species = species;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Warrior warrior)
        {
            if (ModelState.IsValid)
            {
                var isProcessed = webService.CreateWarrior(warrior);
                if (isProcessed)
                {
                    return RedirectToAction("Index");
                }
            }
            var species = webService.GetWarriorSpecies();
            ViewBag.Species = species;
            return View(warrior);
        }

        public ActionResult Assign(int id)
        {
            var warrior = webService.GetWarriorById(id);
            if (warrior == null)
            {
                return HttpNotFound();
            }
            return View(warrior);
        }

        [HttpPost, ActionName("Assign")]
        [ValidateAntiForgeryToken]
        public ActionResult AssignConfirmed(int id)
        {
            var isAssigned = webService.AssignAdditionalDay(id);
            if (isAssigned)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Assign", id);
        }

        public ActionResult Delete(int id)
        {
            var warrior = webService.GetWarriorById(id);
            if (warrior == null)
            {
                return HttpNotFound();
            }
            return View(warrior);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var isDeleted = webService.DeleteWarrior(id);
            if (isDeleted)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Delete", id);
        }
    }
}
