using Microsoft.AspNetCore.Mvc;
using VacationManagement.Data;
using VacationManagement.Models;

namespace VacationManagement.Controllers
{
    public class VacationTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VacationTypesController(ApplicationDbContext context)
        {
           _context = context;
        }
        public IActionResult VacationTypes()
        {
            return View(_context.VacationTypes.OrderBy(x=>x.Id).AsNoTracking().ToList());

        }

        public IActionResult Create ()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(VacationType vacation)
        {
            if (ModelState.IsValid) 
            {
                var result =_context.VacationTypes.FirstOrDefault(x => x.VacationName.Contains(vacation.VacationName.Trim()));
                if(result == null)
                {
                    _context.VacationTypes.Add(vacation);
                    _context.SaveChanges();
                    return RedirectToAction("VacationTypes");
                }
                ViewBag.ErrorMsg = false;
           
            }
            return View(vacation);
        }

        public IActionResult Edit (int? Id) 
        {
        return View(_context.VacationTypes
            .AsNoTracking()
            .SingleOrDefault(x => x.Id == Id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit (VacationType vacation)
        {
            if (ModelState.IsValid)
            {
                _context.VacationTypes.Update(vacation);
                _context.SaveChanges();
                return RedirectToAction("VacationTypes");
            }
            return View(vacation);
        }
        public IActionResult Delete (int id)
        {
            return View(_context.VacationTypes
                .AsNoTracking()
                .SingleOrDefault(x=>x.Id == id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(VacationType vacation)
        {
            if (vacation !=null)
            {
                _context.VacationTypes.Remove(vacation);
                _context.SaveChanges();
                return RedirectToAction("VacationTypes");
            }
            return View(vacation);
        }
    }
}
