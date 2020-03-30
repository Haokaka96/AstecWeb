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
    [Table("Cities")]
    public class City
    {
        /// <summary>
        /// Id thành phố
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Tên thành phố
        /// </summary>
        [StringLength(50)]
        public string CityName { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        [StringLength(256)]
        public string Description { get; set; }
        public virtual IEnumerable<District> District { get; set; }
    }
}
