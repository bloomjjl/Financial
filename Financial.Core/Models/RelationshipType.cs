using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Core.Models
{
    [Table("RelationshipTypes")]
    public class RelationshipType : BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
