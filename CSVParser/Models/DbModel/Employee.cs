using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CSVParser.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string Payroll_Number { get; set; }
        public string Forenames { get; set; }
        public string Surname { get; set; }
        public DateTime Date_of_Birth { get; set; }
        public int Telephone { get; set; }
        public int Mobile { get; set; }
        public string Address { get; set; }
        public string Address_2 { get; set; }
        public string Postcode { get; set; }
        public string EMail_Home { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime EditedDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
