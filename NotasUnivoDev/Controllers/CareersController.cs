using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotasUnivoDev.Db;
using NotasUnivoDev.Models;
using NotasUnivoDev.Models.ViewModels;

namespace NotasUnivoDev.Controllers
{
    public class CareersController : Controller
    {
        private readonly AppDbContext DbContext;
        public CareersController(AppDbContext Db)
        {
            DbContext = Db;
        }

        public IActionResult Index()
        {
            List<CareersModel> list = DbContext.Careers.Include(x => x.Faculty).ToList();
            return View(list);
        }

        [HttpGet]
        public IActionResult Upsert(int id)
        {
            CareersViewModel model = new();

            if (id > 0)
            {
                CareersModel career = DbContext.Careers.FirstOrDefault(x => x.CareerId == id) ?? new();
                model.CareerId = career.CareerId;
                model.CareerName = career.CareerName;
                model.FacultyId = career.FacultyId;
                model.IsActive = career.IsActive;
            }

            model.FacultiesList = DbContext.Faculties.ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult Upsert(CareersViewModel model)
        {
            CareersModel careersModel = new();
            careersModel.CareerName = model.CareerName;
            careersModel.FacultyId = model.FacultyId;
            careersModel.CareerId = model.CareerId;

            ModelState.Remove("FacultiesList");

            if (careersModel.CareerId == 0)
            {
                if (ModelState.IsValid)
                {
                    DbContext.Careers.Add(careersModel);
                    DbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    careersModel.EditedBy = "ADMIN EDITED";
                    careersModel.Edited = DateTime.Now;
                    DbContext.Careers.Update(careersModel);
                    DbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            model.FacultiesList = DbContext.Faculties.ToList();
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            CareersModel career = DbContext.Careers.FirstOrDefault(row => row.CareerId == id) ?? new();
            career.IsActive = !career.IsActive;
            DbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult View(int id)
        {
            CareersModel career = DbContext.Careers.FirstOrDefault(x => x.CareerId == id) ?? new();
            if (career is null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                career.Faculty = DbContext.Faculties.FirstOrDefault(x => career.FacultyId == x.FacultyId);
                return View(career);
            }

        }
    }
}
