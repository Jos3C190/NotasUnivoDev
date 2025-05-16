using Microsoft.AspNetCore.Mvc;
using NotasUnivoDev.Db;
using NotasUnivoDev.Models;
using System.Threading.Tasks;

namespace NotasUnivoDev.Controllers
{
    public class FacultiesController : Controller
    {
        private readonly AppDbContext DbContext;

        public FacultiesController(AppDbContext Db)
        {
            DbContext = Db;
        }

        public IActionResult Index()
        {
            List<FacultiesModel> list = DbContext.Faculties.ToList();
            return View(list);
        }

        [HttpGet]
        public IActionResult Upsert(int id)
        {
            FacultiesModel model = new();
            if (id > 0)
            {
                model = DbContext.Faculties.FirstOrDefault(x => x.FacultyId == id) ?? new();

            }
           
            return View(model);
        }

        [HttpPost]
        public IActionResult Upsert(FacultiesModel model)
        {
            if (model.FacultyId == 0)
            {
                if (ModelState.IsValid)
                {
                    DbContext.Faculties.Add(model);
                    DbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    DbContext.Faculties.Update(model);
                    DbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var faculty = DbContext.Faculties.FirstOrDefault(x => x.FacultyId == id);
            if (faculty != null)
            {
                faculty.IsActive = !faculty.IsActive;
                DbContext.Faculties.Update(faculty);
                DbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult View(int id)
        {
            FacultiesModel model = DbContext.Faculties.FirstOrDefault(x => x.FacultyId == id) ?? new();
            model.Careers = DbContext.Careers.Where(x => x.FacultyId == id).ToList();
            return View(model);
        }
    }
}
