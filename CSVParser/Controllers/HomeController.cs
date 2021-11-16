using CSVParser.Models;
using CSVParser.Services.Interfaces;
using CSVParser.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CSVParser.Controllers
{
    public class HomeController : Controller
    {
        public IEmployeeService _employee;
        private readonly ILogger<HomeController> _logger;
        public HomeController(IEmployeeService employee)
        {
            _employee = employee;
        }

        /// <summary>
        /// Index page
        /// </summary>
        /// <returns>View()</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Post file
        /// </summary>
        /// <param name="postedFile"></param>
        /// <returns>List<Employee></returns>
        [HttpPost]
        public IActionResult Upload(IFormFile file)
        {
            var result = new FileViewModel();

            try
            {                
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    var res = _employee.SetEmployees(reader);

                    result.Loaded = res.Loaded;
                    result.NotLoaded = res.NotLoaded;
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
            
            return Ok(result);
        }

        /// <summary>
        /// Method works for server side filtering
        /// actions: search, order, sort
        /// </summary>
        /// <returns>Json(new { draw , recordsFiltered, recordsTotal, data})</returns>
        /// 
        [HttpPost]
        public IActionResult GetEmployees()
        {
            var listModel = new List<EmployeeViewModel>();
            var dtfm = new DataTableFilterModel();
            try
            {
                dtfm.draw = Request.Form["draw"].FirstOrDefault();
                dtfm.start = Request.Form["start"].FirstOrDefault();
                dtfm.length = Request.Form["length"].FirstOrDefault();
                dtfm.sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                dtfm.sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                dtfm.searchValue = Request.Form["search[value]"].FirstOrDefault();
                dtfm.pageSize = dtfm.length != null ? Convert.ToInt32(dtfm.length) : 10;
                dtfm.skip = dtfm.start != null ? Convert.ToInt32(dtfm.start) : 0;
                dtfm.recordsTotal = 0;

                var result = _employee.GetEmployees(ref dtfm);

                foreach (var res in result)
                {
                    var model = new EmployeeViewModel();

                    model.Id = res.Id;
                    model.Payroll_Number = res.Payroll_Number;
                    model.Forenames = res.Forenames;
                    model.Surname = res.Surname;
                    model.Date_of_Birth = res.Date_of_Birth;
                    model.Telephone = res.Telephone;
                    model.Mobile = res.Mobile;
                    model.Address = res.Address;
                    model.Address_2 = res.Address_2;
                    model.Postcode = res.Postcode;
                    model.EMail_Home = res.EMail_Home;
                    model.Start_Date = res.Start_Date;

                    listModel.Add(model);

                }

                var jsonData = new { draw = dtfm.draw, recordsFiltered = dtfm.recordsTotal, recordsTotal = dtfm.recordsTotal, data = listModel };
                return Json(jsonData);
            }
            catch
            {
                var jsonData = new { draw = dtfm.draw, recordsFiltered = dtfm.recordsTotal, recordsTotal = dtfm.recordsTotal, data = listModel };
                return Json(jsonData);
            }
            
        }
    }
}
