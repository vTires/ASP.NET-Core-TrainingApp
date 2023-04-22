using ASP.NETCore_TrainingApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ASP.NETCore_TrainingApp.Controllers
{
    public class EmployeeController : Controller
    {
        private static List<EmployeeModel> employees = new List<EmployeeModel>();
        private static List<EmployeeModel> expiredEmployees = new List<EmployeeModel>();

        public IActionResult Index()
        {
            var model = new Tuple<List<EmployeeModel>, List<EmployeeModel>>(employees, expiredEmployees);
            return View(model);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(EmployeeModel employee, EmployeeModel expireemployee)
        {
            if (ModelState.IsValid)
            {
                int nextId = employees.Any() ? employees.Max(e => e.Id) + 1 : 1;
                employees.Add(new EmployeeModel
                {
                    Id = nextId,
                    Name = employee.Name,
                    Position = employee.Position,
                    IsAgreement = employee.IsAgreement,
                    Date = DateTime.Now
                });
                expiredEmployees.Add(new EmployeeModel
                {
                    Id = nextId,
                    Name = expireemployee.Name,
                    Position = expireemployee.Position,
                    IsAgreement = expireemployee.IsAgreement,
                    Date = DateTime.Now
                });

                return RedirectToAction("Index");
            }

            return View(employee);
        }

        public IActionResult Remove()
        {
            DateTime currentDate = DateTime.Now;
            List<int> removedEmployeeIds = new List<int>();

            for (int i = expiredEmployees.Count - 1; i >= 0; i--)
            {

                if (expiredEmployees[i].Date.AddSeconds(10) < currentDate)
                {
 
                    int removedEmployeeId = expiredEmployees[i].Id;
                    employees.RemoveAll(e => e.Id == removedEmployeeId);
                    expiredEmployees.RemoveAt(i);
                    removedEmployeeIds.Add(removedEmployeeId);
                }
            }

            return Json(new { removedEmployeeIds });
        }

        public IActionResult GetExpiredEmployees()
        {
            return PartialView("_ExpiredEmployees", expiredEmployees);
        }


    }
}