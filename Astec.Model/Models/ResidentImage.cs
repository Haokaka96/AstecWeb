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
    [Table("Images")]
    public class ResidentImage: Auditable
    {
        /// <summary>
        /// Id ảnh
        /// </summary>
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
        /// Ảnh
        /// </summary>
        public byte[] Image { get; set; }
        /// <summary>
        /// Mô tả
        /// </summary>
        [StringLength(256)]
        public string Description { get; set; }

    }
}
