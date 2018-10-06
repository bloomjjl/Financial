using Financial.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.WebApplication.Models.ViewModels.AccountTransaction
{
    public class IndexViewModel
    {
        public IndexViewModel() { }

        public IndexViewModel(Business.Models.AccountTransaction bmAssetTransaction)
        {
            Id = bmAssetTransaction.AssetTransactionId;
            AssetId = bmAssetTransaction.AssetId;
            AssetName = bmAssetTransaction.AssetName;
            DueDate = bmAssetTransaction.DueDate;
            ClearDate = bmAssetTransaction.ClearDate;
            TransactionType = bmAssetTransaction.TransactionTypeName;
            Amount = bmAssetTransaction.Amount;
            Note = bmAssetTransaction.Note;
        }

        public int Index { get; set; }
        public int Id { get; set; }
        public int AssetId { get; set; }
        [Display(Name = "Asset Name")]
        public string AssetName { get; set; }
        [Required]
        [Display(Name = "Due")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DueDate { get; set; }
        [Display(Name = "Cleared")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ClearDate { get; set; }
        public string TransactionType { get; set; }
        public string Income { get; set; }
        public string Expense { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal Amount { get; set; }
        [Display(Name = "Balance")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal Total { get; set; }
        public string Note { get; set; }

    }
}
