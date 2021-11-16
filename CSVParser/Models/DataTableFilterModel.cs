using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSVParser.Models
{
    public class DataTableFilterModel
    {
        public string draw { get; set; }
        public string start { get; set; }
        public string length { get; set; }        
        public string sortColumn { set; get; }
        public string sortColumnDirection { set; get; }
        public string searchValue { set; get; }
        public int pageSize { set; get; }
        public int skip { set; get; }
        public int recordsTotal { get; set; }
    }
}
