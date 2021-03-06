﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Core.Models
{
    [Table("AccountTypeRelationshipTypes")]
    public class AssetTypeRelationshipType : BaseEntity
    {
        [Required]
        [Display(Name = "ParentAssetType ID")]
        [Column("ParentAccountTypeId")]
        public int ParentAssetTypeId { get; set; }
        [Display(Name = "ChildAssetType ID")]
        [Column("ChildAccountTypeId")]
        public int ChildAssetTypeId { get; set; }
        [Required]
        [Display(Name = "ParentChildRelationshipType ID")]
        public int ParentChildRelationshipTypeId { get; set; }
    }
}
