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
    [Table("FingerPrints")]
    public class FingerPrint:Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Id cư dân
        /// </summary>
        [StringLength(20)]
        [Column(TypeName ="varchar")]
        public string ResidentId { get; set; } 
        /// <summary>
        /// Số thứ tự các ngón tay
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// Hình ảnh vân tay các ngón
        /// </summary>
        public byte[] FingerImage { get; set; }
        /// <summary>
        /// Mô tả
        /// </summary>
        [StringLength(256)]
        public string Description { get; set; }
    }
}
