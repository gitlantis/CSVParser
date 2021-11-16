using CSVParser.Models;
using CSVParser.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Linq.Expressions;
using CSVParser.Helpers;

namespace CSVParser.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext _appDbContext;
        public EmployeeService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        /// <summary>
        /// Upload and get only uploaded employes list
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public FileUploadModel SetEmployees(StreamReader reader)
        {

            var csvRows = new FileUploadModel();

            csvRows.NotLoaded = 0;
            csvRows.Loaded = 0;

            try
            {
                while (!reader.EndOfStream)
                {
                    try
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        var employee = new Employee();

                        employee.Payroll_Number = values[0];
                        employee.Forenames = values[1];
                        employee.Surname = values[2];
                        employee.Date_of_Birth = Convert.ToDateTime(DateTime.ParseExact(values[3], "dd/M/yyyy", CultureInfo.InvariantCulture));
                        Int32.TryParse(values[4], out int phon);
                        employee.Telephone = phon;
                        Int32.TryParse(values[5], out phon);
                        employee.Mobile = phon;
                        employee.Address = values[6];
                        employee.Address_2 = values[7];
                        employee.Postcode = values[8];
                        employee.EMail_Home = values[9];
                        employee.Start_Date = Convert.ToDateTime(DateTime.ParseExact(values[10], "dd/M/yyyy", CultureInfo.InvariantCulture));
                        employee.EditedDate = DateTime.Now;
                        employee.CreatedDate = DateTime.Now;

                        _appDbContext.Add(employee);
                        csvRows.Loaded++;


                    }
                    catch (Exception e)
                    {
                        csvRows.NotLoaded++;
                    }
                }

                _appDbContext.SaveChanges();
            }
            catch (Exception)
            {
                return null;
            }

            return csvRows;


        }

        /// <summary>
        /// get employees list
        /// </summary>
        /// <returns></returns>
        public List<Employee> GetEmployees(ref DataTableFilterModel model)
        {
            var result = new List<Employee>();
            try
            {
                var sv = model.searchValue;

                //get result
                var res = _appDbContext.Employee.ToList();

                //order by custom field name
                SortListByPropertyName(res, model.sortColumnDirection, model.sortColumn);

                result = res.Where(c => c.Payroll_Number.Contains(sv)
                            || c.Forenames.Contains(sv) || c.Surname.Contains(sv) || c.Telephone.ToString().Contains(sv)
                            || c.Mobile.ToString().Contains(sv) || c.Address.Contains(sv) || c.Address_2.Contains(sv)
                            || c.Postcode.Contains(sv) || c.EMail_Home.Contains(sv) || c.Date_of_Birth.ToString().Contains(sv)
                            || c.Start_Date.ToString().Contains(sv)).ToList();

                //count result for paginator
                model.recordsTotal = result.Count;

                //result for one page
                result = result.Skip(model.skip).Take(model.pageSize).ToList();

            }
            catch (Exception e)
            {
                model.recordsTotal = 0;
                return null;
            }
            return result;
        }

        public void SortListByPropertyName(List<Employee> list, string ordDirection, string propertyName) 
        {
            var propInfo = typeof(Employee).GetProperty(propertyName);
            Comparison<Employee> asc = (t1, t2) => ((IComparable)propInfo.GetValue(t1, null)).CompareTo(propInfo.GetValue(t2, null));
            Comparison<Employee> desc = (t1, t2) => ((IComparable)propInfo.GetValue(t2, null)).CompareTo(propInfo.GetValue(t1, null));
            list.Sort(ordDirection.Equals("asc") ? asc : desc);
        }
    }


}
