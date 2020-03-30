using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Astec.WebApp.Models
{
    public class ApplicationRoleViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Discriminator { get; set; }
    }
}