using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astec.Model.Models
{
    public class Image
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
    }
}
