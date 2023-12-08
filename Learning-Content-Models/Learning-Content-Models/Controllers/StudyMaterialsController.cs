using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Learning_Content_Models.Data;
using Learning_Content_Models.Models;
using Type = Learning_Content_Models.Models.Vid;
using Microsoft.AspNetCore.Authorization;

namespace Learning_Content_Models.Controllers
{
    public class StudyMaterialsController : Controller
    {
        private readonly ApplicationDbContext context;

        public StudyMaterialsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var materials = context.StudyMaterials
            .Include(m => m.Vid)
            .Include(m => m.Category)
            .ToList();

            return View(materials);
        }

        //Add Movie
        public IActionResult Add()
        {
            ViewBag.Vids = context.Vids.ToList();
            ViewBag.Categories = context.Categories.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Add(StudyMaterial studyMaterial)
        {
            context.StudyMaterials.Add(studyMaterial);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        //Update Movie
        public IActionResult Edit(int id)
        {
            var studyMaterial = context.StudyMaterials
                .Include(m => m.Vid)
                .Include(m => m.Category)
                .FirstOrDefault(m => m.Id == id);
            if (studyMaterial == null)
            {
                return NotFound();
            }

            ViewBag.Vids = context.Vids.ToList();
            ViewBag.Categories = context.Categories.ToList();

            return View(studyMaterial);
        }

        [HttpPost]
        public IActionResult Edit(StudyMaterial studyMaterial)
        {
            context.StudyMaterials.Update(studyMaterial);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var studyMaterial = context.StudyMaterials.Find(id);

            if (studyMaterial == null)
            {
                return NotFound();
            }

            context.StudyMaterials.Remove(studyMaterial);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
