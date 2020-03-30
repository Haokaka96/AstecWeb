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
    [Table("Residents")]
    public class Resident :Auditable
    {
        /// <summary>
        /// Mã cư dân
        /// </summary>
        [Key]
        [Required]
        [StringLength(20)]       
        public string ResidentId { get; set; }

        /// <summary>
        /// Mã căn hộ
        /// </summary>
        //[StringLength(20)]
        //[Column(TypeName ="varchar")]
        public int ApartmentId { get; set; }

        /// <summary>
        /// Họ và tên đệm
        /// </summary>
        [StringLength(50)]
        public string FirstName { get; set; }

        /// <summary>
        /// Tên cư dân
        /// </summary>
        [StringLength(50)]
        public string LastName { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public bool Gender { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [StringLength(50)]
        public string Email { get; set; }

        /// <summary>
        /// Tuổi
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Số CMT/ thẻ căn cước
        /// </summary>
        [StringLength(20)]
        public string IdCardNumber { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        [StringLength(20)]
        public string Phone { get; set; }

        /// <summary>
        /// Nghề nghiệp
        /// </summary>
        [StringLength(50)]
        public string Job { get; set; }

        /// <summary>
        /// Ảnh
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Dân tộc
        /// </summary>
        [StringLength(20)]
        public string Ethnic { get; set; }

        /// <summary>
        /// Tôn giáo
        /// </summary>
        [StringLength(20)]
        public string Religion { get; set; }

        /// <summary>
        /// Là chủ hộ?
        /// </summary>
        public bool IsHeadOfHousehold { get; set; }

        /// <summary>
        /// Hộ khẩu thường trú
        /// </summary>
        [StringLength(256)]
        public string PermanentResidence { get; set; }

        /// <summary>
        /// Quê quán
        /// </summary>
        [StringLength(256)]
        public string HomeTown { get; set; }

        [ForeignKey("ApartmentId")]
        public virtual Apartment Apartment { get; set; }
    }
}
