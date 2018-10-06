using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Core.Models
{
    [Table("Accounts")]
    public class Asset : BaseEntity
    {
        [Required]
        [Display(Name = "AssetType ID")]
        [Column("AccountTypeId")]
        public int AssetTypeId { get; set; }

        [Required]
        public string Name { get; set; }


        [ForeignKey("AssetTypeId")]
        public AssetType AssetType { get; set; }

        //public ICollection<AssetSetting> AssetSettings { get; set; }
        //public ICollection<AssetTransaction> AssetTransactions { get; set; }

    }
}
