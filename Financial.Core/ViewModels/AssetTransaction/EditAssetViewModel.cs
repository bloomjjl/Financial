using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Financial.Core.ViewModels.AssetTransaction
{
    public class EditAssetViewModel
    {
        public EditAssetViewModel() { }

        public EditAssetViewModel(Models.AssetTransaction dtoAssetTransaction, List<SelectListItem> sliAssets, string selectdAssetId,
            Models.AssetType dtoAssetType, string transactionType, string transactionCategory)
        {
            Id = dtoAssetTransaction.Id;
            Assets = sliAssets;
            SelectedAssetId = selectdAssetId;
            AssetTypeName = dtoAssetType.Name;
            DueDate = dtoAssetTransaction.DueDate.ToString("MM/dd/yyyy");
            ClearDate = dtoAssetTransaction.ClearDate.ToString("MM/dd/yyyy");
            CheckNumber = dtoAssetTransaction.CheckNumber;
            Amount = string.Format("{0:C}", dtoAssetTransaction.Amount);
            Note = dtoAssetTransaction.Note;
            TransactionType = transactionCategory;
            TransactionCategory = transactionCategory;
        }


        public int Id { get; set; }
        [Required]
        [Display(Name = "Asset Name")]
        public string SelectedAssetId { get; set; }
        public IEnumerable<SelectListItem> Assets { get; set; }
        [Display(Name = "Asset Type")]
        public string AssetTypeName { get; set; }
        [Display(Name = "Check Number")]
        public string CheckNumber { get; set; }
        [Display(Name = "Due")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public string DueDate { get; set; }
        [Display(Name = "Cleared")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public string ClearDate { get; set; }
        public string Amount { get; set; }
        public string Note { get; set; }
        [Display(Name = "Type")]
        public string TransactionType { get; set; }
        [Display(Name = "Category")]
        public string TransactionCategory { get; set; }
    }
}
