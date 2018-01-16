using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Core.Models
{
    [Table("ParentChildRelationshipTypes")]
    public class ParentChildRelationshipType : BaseEntity
    {
        [Required]
        [Display(Name = "ParentRelationshipType ID")]
        public int ParentRelationshipTypeId { get; set; }
        [Required]
        [Display(Name = "ChildRelationshipType ID")]
        public int ChildRelationshipTypeId { get; set; }

    }
}
