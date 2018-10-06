using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Core.Models
{
    [Table("AccountTypeAttributeTypes")]
    public class AssetTypeSettingType : BaseEntity
    {
        [Required]
        [Column("AccountTypeId")]
        public int AssetTypeId { get; set; }
        [Required]
        [Column("AttributeTypeId")]
        public int SettingTypeId { get; set; }
    }
}
