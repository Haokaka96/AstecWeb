using Astec.Model.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Astec.Model.Models
{
    [Table("Apartments")]
    public class Apartment:Auditable
    {
        /// <summary>
        /// Mã căn hộ
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ApartmentID { set; get; }

        /// <summary>
        /// Tên căn hộ
        /// </summary>
        [StringLength(50)]
        public string ApartmentName { get; set; }

        /// <summary>
        /// Vị trí
        /// </summary>
        [StringLength(50)]
        public string Location { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        [StringLength(200)]
        public string Description { get; set; }
       
    }
}