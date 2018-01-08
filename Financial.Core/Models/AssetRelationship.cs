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
        [Display(Name = "Parent Asset ID")]
        [Column("ParentAccountId")]
        public int ParentAssetId { get; set; }
        [Required]
        [Display(Name = "Child Asset ID")]
        [Column("ChildAccountId")]
        public int ChildAssetId { get; set; }
        [Required]
        [Display(Name = "AccountTypeRelationshipType ID")]
        public int AssetTypeRelationshipTypeId { get; set; }
    }
}
