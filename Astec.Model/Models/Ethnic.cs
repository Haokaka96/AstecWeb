using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astec.Model.Models
{
    [Table("Ethnics")]
    public class Ethnic
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Tên 
        /// </summary>
        [StringLength(50)]
        public string EthnicName { get; set; }
        /// <summary>
        /// Mô tả
        /// </summary>
        [StringLength(256)]
        public string Description { get; set; }
    }
}
