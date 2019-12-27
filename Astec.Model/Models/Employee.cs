using Astec.Model.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astec.Model.Models
{
    [Table("Employees")]
    public class Employee:Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(256)]
        public string Name { get; set; }
        [StringLength(50)]
        public string ImageName { get; set; }
        public byte[] Image { get; set; }
        public DateTime DateOfBirth { get; set; }    
        public bool Gender { get; set; }
        [StringLength(50)]
        public string Phone { get; set; }
        [StringLength(256)]
        public string Address { get; set; }
        
    }
}
