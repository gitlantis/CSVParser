using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSVParser.ViewModels
{
    public class FileViewModel
    {
        public int Loaded { get; set; }
        public int NotLoaded { get; set; }
    }
}
