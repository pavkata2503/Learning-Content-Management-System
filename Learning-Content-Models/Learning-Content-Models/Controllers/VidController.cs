using Humanizer.Localisation;
using Learning_Content_Models.Data;
using Learning_Content_Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Learning_Content_Models.Controllers
{
    public class VidController : Controller
    {
        private readonly ApplicationDbContext context;

        public VidController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var vids = context.Vids
            .Include(m => m.StudyMaterials).ToList();

            return View(vids);
        }
        public IActionResult Add()
        {
            ViewBag.StudyMaterials = context.StudyMaterials.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Add(Vid vid)
        {
            context.Vids.Add(vid);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        //Update Genre
        public IActionResult Edit(int id)
        {
            var vid = context.Vids
                .Include(m => m.StudyMaterials)
                .FirstOrDefault(m => m.Id == id);
            if (vid == null)
            {
                return NotFound();
            }

            ViewBag.StudyMaterials = context.StudyMaterials.ToList();

            return View(vid);
        }

        [HttpPost]
        public IActionResult Edit(Vid vid)
        {
            if (ModelState.IsValid)
            {
                context.Vids.Update(vid);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vid);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var vid = context.Vids.Find(id);

            if (vid == null)
            {
                return NotFound();
            }

            context.Vids.Remove(vid);
            context.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
