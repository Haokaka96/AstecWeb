using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Astec.WebApp.Models
{
    public class ApplicationModuleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int ParentId { get; set; }
    }
}