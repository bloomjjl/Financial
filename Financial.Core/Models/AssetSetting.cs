using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Core.Models
{
    [Table("AccountAttributes")]
    public class AssetSetting : BaseEntity
    {
        [Required]
        [Display(Name = "Asset ID")]
        [Column("AccountId")]
        public int AssetId { get; set; }
        [Required]
        [Display(Name = "SettingType ID")]
        [Column("AttributeTypeId")]
        public int SettingTypeId { get; set; }
        public string Value { get; set; }
    }
}
