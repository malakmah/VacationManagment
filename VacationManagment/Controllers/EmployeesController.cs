using Microsoft.AspNetCore.Mvc;
using VacationManagement.Data;
using VacationManagement.Models;

namespace VacationManagement.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Employees()
        {
            return View(_context.Employees.Include(x=>x.Department)
                .AsNoTracking()
                .OrderBy(x => x.Id)
                .ToList()
                );
        }


        public IActionResult Create()
        {
            ViewBag.Departments=_context.Departments.OrderBy(x => x.Name).ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            if(ModelState.IsValid)
            {

                _context.Employees.Add(employee);
            _context.SaveChanges();
                return RedirectToAction("Employees");
            }
            ViewBag.Departments = _context.Departments.OrderBy(x => x.Name).ToList();

            return View(employee);
        }

        public IActionResult Edit(int? Id) 
        {
            ViewBag.Departments=_context.Departments.OrderBy(x => x.Id).ToList();
            return View(_context.Employees.Include(x => x.Department).FirstOrDefault(X=>X.Id==Id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee employee)
        {
        if(ModelState.IsValid)
            {
                _context.Employees.Update(employee);
                _context.SaveChanges();

                return RedirectToAction("Employees");

            }
            ViewBag.Departments = _context.Departments.OrderBy(x => x.Name).ToList();

            return View(employee) ;
        
        }

       
        public IActionResult Delete(int? Id)
        {
 
            return View(_context.Employees.Include(x=>x.Department).SingleOrDefault(x=>x.Id==Id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Employee employee)
        {
            if(employee != null) 
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
                return RedirectToAction("Employees");
            }
              return View(employee) ;
        }

    }
}
