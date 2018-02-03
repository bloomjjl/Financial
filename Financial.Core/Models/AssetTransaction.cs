using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Core.Models
{
    [Table("AccountTransactions")]
    public class AssetTransaction : BaseEntity
    {
        [Required]
        [Display(Name = "Asset ID")]
        [Column("AccountId")]
        public int AssetId { get; set; }
        [Required]
        public int TransactionTypeId { get; set; }
        [Required]
        public int TransactionCategoryId { get; set; }
        [Required]
        public int TransactionDescriptionId { get; set; }
        [Display(Name = "Check Number")]
        public string CheckNumber { get; set; }
        [Required]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime TransactionDate { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public string Note { get; set; }
    }
}
