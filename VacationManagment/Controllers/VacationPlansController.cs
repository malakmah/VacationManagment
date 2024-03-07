using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;
using VacationManagement.Data;
using VacationManagement.Models;

namespace VacationManagement.Controllers
{

    public class VacationPlansController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VacationPlansController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult VacationPlans()
        {
            return View(_context.RequestVacations.
                Include(X => X.Employee)
                .Include(X => X.VacationType)
                .Include(x => x.VacationPlanList)
                .AsNoTracking()
                .OrderByDescending(X => X.RequestDate)
                .ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create (VacationPlan model, int[] DayOfWeeKCheck)
        {
            if(ModelState.IsValid)
            {
                var Result = _context.VacationPlans
                    .Where(x => x.RequestVacation.EmployeeId == model.RequestVacation.EmployeeId
                    && x.VacationDate >= model.RequestVacation.StartDate
                    && x.VacationDate <= model.RequestVacation.EndDate).FirstOrDefault();
                if(Result != null)
                {

                    ViewBag.ErrorVacation=false; 
                    return View(model);
                }
                for (DateTime date = model.RequestVacation.StartDate;
                  date <= model.RequestVacation.EndDate; date = date.AddDays(1))
                {
                    if (Array.IndexOf(DayOfWeeKCheck, (int)date.DayOfWeek) != -1)
                    {
                        model.Id = 0;
                        model.VacationDate = date;
                        model.RequestVacation.RequestDate = DateTime.Now;
                        _context.VacationPlans.Add(model);
                        _context.SaveChanges();
                    }
                }
                return RedirectToAction("VacationPlans");
            }
            return View(model);
        }

        public IActionResult Edit (int? id)
        {
            ViewBag.Employees= _context.Employees.OrderBy(x=> x.Name).ToList();
            ViewBag.VacationTypes = _context.VacationTypes.OrderBy(x => x.VacationName).ToList();

            return View(_context.RequestVacations.Include
                (x=>x.Employee)
                .Include(x=>x.VacationType)
                .Include(x=>x.VacationPlanList).FirstOrDefault(x=>x.Id==id));
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit (RequestVacation model)
        {
            if(ModelState.IsValid)
            {
               if(model.Approved==true)
                model.DateApproved = DateTime.Now;
               _context.RequestVacations.Update(model);
                _context.SaveChanges();
                return RedirectToAction("VacationPlans");
            }
            ViewBag.Employees = _context.Employees.OrderBy(x => x.Name).ToList();
            ViewBag.VacationTypes = _context.VacationTypes.OrderBy(x => x.VacationName).ToList();

            return View(model);
        }

        public IActionResult GetVacationsType()
        {
            return Json(_context.VacationTypes.OrderBy(x => x.Id).ToList());
        }

        public IActionResult GetCountVcationsEmployee(int? id)
        {
            return Json(_context.VacationPlans.Where(x => x.RequestVacationId == id).Count());
        }

   
       
        public IActionResult Delete(int? Id)
        {

            return View(_context.RequestVacations
                .Include(x => x.Employee)
                .Include(x => x.VacationType)
                .Include(x => x.VacationPlanList)
                .FirstOrDefault(x => x.Id == Id));
        }
        [HttpPost]
         public IActionResult Delete(RequestVacation vacation)
        { 
            if (vacation != null) 
            {
                _context.RequestVacations.Remove(vacation);
                _context.SaveChanges();
                return RedirectToAction("VacationPlans");
            }
            return View(vacation); 
        
        }

        public IActionResult ViewVacationReport()
        {
            ViewBag.Employees=_context.Employees.ToList();
            return View();
        }

        public IActionResult ViewVacationReport2()
        {
            ViewBag.Employees = _context.Employees.ToList();
            return View();
        }

        public IActionResult GetVacationReport(int EmployeeId,DateTime FromDate, DateTime ToDate,int Approved)
        {
            string Id = "";
            if (EmployeeId != 0 && EmployeeId.ToString() != "")
                Id = $"AND E.Id={EmployeeId}";
            #region MyRegion
            //var sqlQuery = _context.SqlDataTable($@"SELECT DISTINCT      
            //                                      E.Id,E.Name, E.VacationBalance,
            //                                      SUM(VT.NumberDays) AS TotalBalance,
            //                       E.VacationBalance - SUM(VT.NumberDays) AS TOTAL
            //                                      FROM  
            //                                      Employees E INNER JOIN
            //                                      RequestVacations R ON E.Id = R.EmployeeId INNER JOIN
            //                                      VacationPlans   V ON R.Id = V.RequestVacationId INNER JOIN
            //                                      VacationTypes   VT ON r.VacationTypeId = VT.Id
            //                             WHERE v.VacationDate between 
            //                                     '" + FromDate.ToString("yyyy-MM-dd") + "' AND '" + ToDate.ToString("yyyy-MM-dd") + "' " +
            //                                     " AND R.Approved='True'" +
            //                                     $"{Id}GROUP BY  E.Id,E.Name, E.VacationBalance"); 
            #endregion

            #region MyRegion
            //string sqlQuery = ($@"SELECT DISTINCT      
            //                                      E.Id,E.Name, E.VacationBalance,
            //                                      SUM(VT.NumberDays) AS TotalBalance,
            //                                      E.VacationBalance - SUM(VT.NumberDays) AS Total
            //                                      FROM  
            //                                      Employees E INNER JOIN
            //                                      RequestVacations R ON E.Id = R.EmployeeId INNER JOIN
            //                                      VacationPlans   V ON R.Id = V.RequestVacationId INNER JOIN
            //                                      VacationTypes   VT ON r.VacationTypeId = VT.Id
            //                                      WHERE v.VacationDate between 
            //                                     '" + FromDate.ToString("yyyy-MM-dd") + "' AND '" + ToDate.ToString("yyyy-MM-dd") + "' " +
            //                                        " AND R.Approved='True'" +
            //                                        $"{Id}GROUP BY  E.Id,E.Name, E.VacationBalance"); 
            #endregion

             var SqlGetReport = _context.sqlGetReports.FromSqlRaw("spl_GetReports {0},{1},{2},{3}",EmployeeId,FromDate,ToDate,Approved).ToList();

            ViewBag.Employees = _context.Employees.ToList();

            return View("ViewVacationReport2", SqlGetReport);
        }
    }    
 }
