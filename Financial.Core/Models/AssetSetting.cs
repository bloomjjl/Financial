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
        [Column("AccountId")]
        public int AssetId { get; set; }
        [Required]
        [Column("AttributeTypeId")]
        public int SettingTypeId { get; set; }
        public string Value { get; set; }





        [ForeignKey("AssetId")]
        public Asset Asset { get; set; }

        [ForeignKey("SettingTypeId")]
        public SettingType SettingType { get; set; }

    }
}
