using System;
using System.ComponentModel.DataAnnotations;

namespace Astec.Model.Abstract
{
    public abstract class Auditable :IAuditable
    {
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }
        /// <summary>
        /// Người tạo
        /// </summary>
        [StringLength(256)]
        public string CreatedBy { set; get; }
        /// <summary>
        /// Ngày sửa
        /// </summary>
        public DateTime? UpdatedDate { set; get; }
        /// <summary>
        /// Người sửa
        /// </summary>
        [StringLength(256)]
        public string UpdatedBy { get; set; }
        [StringLength(256)]
        public string MetaKeyword { set; get; }
        [StringLength(256)]
        public string MetaDescription { set; get; }
        /// <summary>
        /// Trạng thái
        /// </summary>
        public bool Status { set; get; }
    }
}