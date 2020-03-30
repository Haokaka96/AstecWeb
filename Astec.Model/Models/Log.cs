using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astec.Model.Models
{
    [Table("Logs")]
    public class Log
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Thời gian ghi
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Luồng
        /// </summary>
        [StringLength(255)]
        public string Thread { get; set; }
        /// <summary>
        /// Level
        /// </summary>
        [StringLength(50)]
        public string Level { get; set; }
        /// <summary>
        /// Logger
        /// </summary>
        [StringLength(255)]
        public string Logger { get; set; }
        /// <summary>
        /// Nội dung
        /// </summary>
        [StringLength(4000)]
        public string Message { get; set; }
        /// <summary>
        /// Lỗi
        /// </summary>
        [StringLength(2000)]
        public string Exception { get; set; }
    }
}
