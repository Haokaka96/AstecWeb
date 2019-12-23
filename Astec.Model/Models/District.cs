using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astec.Model.Models
{
    [Table("Districts")]
    public class District
    {
        /// <summary>
        /// Id Quận/huyện
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CityId { get; set; }
        /// <summary>
        /// Tên Quận/huyện
        /// </summary>
        public string DistrictName { get; set; }
        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }
        [ForeignKey("CityId")]
        public virtual City City { get; set; }
        public virtual IEnumerable<Wards> Ward { get; set; }
    }
}
