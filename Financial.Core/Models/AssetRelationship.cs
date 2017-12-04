using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Core.Models
{
    [Table("AccountRelationships")]
    public class AssetRelationship : BaseEntity
    {
        [Required]
        [Display(Name = "First Asset ID")]
        [Column("FirstAccountId")]
        public int FirstAssetId { get; set; }
        [Required]
        [Display(Name = "RelationshipType ID")]
        public int RelationshipTypeId { get; set; }
        [Required]
        [Display(Name = "Second Asset ID")]
        [Column("SecondAccountId")]
        public int SecondAssetId { get; set; }
    }
}
