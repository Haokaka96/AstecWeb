using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astec.Model.Models
{
    [Table("Wards")]
    public class Wards
    {
        /// <summary>
        /// Id phường/xã
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int DistrictId { get; set; }
        /// <summary>
        /// Tên phường/xã
        /// </summary>
        public string WardName { get; set; }
        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }

        [ForeignKey("DistrictId")]
        public virtual District District { get; set; }
        public virtual IEnumerable<Street> Streets { get; set; }
    }
}
