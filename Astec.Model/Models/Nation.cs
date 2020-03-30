using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astec.Model.Models
{
    [Table("Nations")]
    public class Nation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Mã quốc gia
        /// </summary>
        [StringLength(50)]
        public string NationCode { get; set; }
        /// <summary>
        /// Tên quốc gia
        /// </summary>
        [StringLength(256)]
        public string NationName { get; set; }
        /// <summary>
        /// Mô tả
        /// </summary>
        [StringLength(256)]
        public string Description { get; set; }
    }
}
