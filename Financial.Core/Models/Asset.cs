using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [Required]
        [Display(Name = "Version Explanation")]
        public string VersionExplanation { get; set; }

        [Required]
        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Created On")]
        public DateTime CreateDate { get; set; }

        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Updated On")]
        public DateTime UpdateDate { get; set; }

    }
}
