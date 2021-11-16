using CSVParser.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CSVParser.Services.Interfaces
{
    public interface IEmployeeService
    {
        FileUploadModel SetEmployees(StreamReader reader);
        List<Employee> GetEmployees(ref DataTableFilterModel model);
    }
}
