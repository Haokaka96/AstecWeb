using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astec.Model.Models
{
    [Table("Streets")]
    public class Street
    {
        /// <summary>
        /// Id Đường/phố
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int WardId { get; set; }
        /// <summary>
        /// Tên đường/phố
        /// </summary>
        public string StreetName { get; set; }
        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }

        [ForeignKey("WardId")]
        public virtual Wards Ward { get; set; }
    }
}
