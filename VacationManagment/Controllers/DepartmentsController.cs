using Microsoft.AspNetCore.Mvc;
using VacationManagement.Data;
using VacationManagement.Models;

namespace VacationManagement.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly ApplicationDbContext _Context;

        public DepartmentsController(ApplicationDbContext Context)
        {
            _Context = Context;
        }
        public IActionResult Departments()
        {
            return View(_Context.Departments
                .OrderBy(x => x.Name)
                .AsNoTracking()
                .ToList()
                );
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Department model)
        {
            if (ModelState.IsValid)
            {
                _Context.Departments.Add(model);
                _Context.SaveChanges();
                return RedirectToAction("Departments");
            }
            return View(model);
        }
        public IActionResult Edit(int? Id)
        {

            return View(_Context.Departments.FirstOrDefault(x => x.Id == Id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Department model)
        {
            if (ModelState.IsValid)
            {
                _Context.Departments.Update(model);
                _Context.SaveChanges();
                return RedirectToAction("Departments");
            }
            return View(model);
        }
        public IActionResult Delete(int? Id)
        {

            return View(_Context.Departments.FirstOrDefault(x => x.Id == Id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Department model)
        {
            if (model != null)
            {
                _Context.Departments.Remove(model);
                _Context.SaveChanges();
                return RedirectToAction("Departments");

            }
            return View(model);
        }



    }
    
}
