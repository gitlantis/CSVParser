using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSVParser.Models
{
    public class FileUploadModel
    {
        public int Loaded { get; set; }
        public int NotLoaded { get; set; }
    }
}
