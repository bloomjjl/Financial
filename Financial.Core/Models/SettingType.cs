using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Core.Models
{
    [Table("AttributeTypes")]
    public class SettingType : BaseEntity
    {
        [Required]
        public string Name { get; set; }


        public ICollection<AssetSetting> AssetSettings { get; set; }



        public static readonly int IdForAccountNumber = 1;
    }
}
