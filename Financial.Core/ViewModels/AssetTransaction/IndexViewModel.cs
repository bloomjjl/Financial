using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Core.ViewModels.AssetTransaction
{
    public class IndexViewModel
    {
        public IndexViewModel() { }

        public IndexViewModel(int index, Models.AssetTransaction dtoAssetTransaction, string clearDate, Models.Asset dtoAsset, string assetNameAdditionalInformaiton, string transactionType, decimal total)
        {
            Index = index;
            Id = dtoAssetTransaction.Id;
            AssetId = dtoAsset.Id;
            AssetName = dtoAsset.Name + assetNameAdditionalInformaiton;
            DueDate = dtoAssetTransaction.DueDate.ToString("MM/dd/yyyy");
            ClearDate = clearDate;
            TransactionType = transactionType;
            Amount = dtoAssetTransaction.Amount;
            Total = total;
        }

        public int Index { get; set; }
        public int Id { get; set; }
        public int AssetId { get; set; }
        [Display(Name = "Asset Name")]
        public string AssetName { get; set; }
        [Required]
        [Display(Name = "Due")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public string DueDate { get; set; }
        [Display(Name = "Cleared")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public string ClearDate { get; set; }
        public string TransactionType { get; set; }
        public string Income { get; set; }
        public string Expense { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal Amount { get; set; }
        [Display(Name = "Balance")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal Total { get; set; }

    }
}
