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
        [Display(Name = "Due")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; }
        [Required]
        [Display(Name = "Cleared")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime ClearDate { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public string Note { get; set; }





        [ForeignKey("AssetId")]
        public Asset Asset { get; set; }

        [ForeignKey("TransactionTypeId")]
        public TransactionType TransactionType { get; set; }

        [ForeignKey("TransactionCategoryId")]
        public TransactionCategory TransactionCategory { get; set; }

        [ForeignKey("TransactionDescriptionId")]
        public TransactionDescription TransactionDescription { get; set; }
    }
}
