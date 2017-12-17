﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Core.Models
{
    [Table("AccountTypesRelationshipTypes")]
    public class AssetTypeRelationshipType : BaseEntity
    {
        [Required]
        [Display(Name = "AssetType ID")]
        [Column("AccountTypeId")]
        public int AssetTypeId { get; set; }
        [Required]
        [Display(Name = "RelationshipType ID")]
        public int RelationshipTypeId { get; set; }
    }
}