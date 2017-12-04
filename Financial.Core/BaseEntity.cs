using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Core
{
    public class BaseEntity
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
