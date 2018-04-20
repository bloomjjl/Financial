using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Financial.WebApplication.Models.ViewModels.AssetTransaction
{
    public class EditViewModel
    {
        public EditViewModel() { }

        public EditViewModel(Core.Models.AssetTransaction dtoAssetTransaction,
            Core.Models.Asset dtoAsset, Core.Models.AssetType dtoAssetType,
            List<SelectListItem> sliTransactionTypes, List<SelectListItem> sliTransactionCategories)
        {
            Id = dtoAssetTransaction.Id;
            AssetId = dtoAsset.Id;
            AssetName = dtoAsset.Name;
            AssetTypeName = dtoAssetType.Name;
            DueDate = dtoAssetTransaction.DueDate.ToString("MM/dd/yyyy");
            ClearDate = dtoAssetTransaction.ClearDate.ToString("MM/dd/yyyy");
            CheckNumber = dtoAssetTransaction.CheckNumber;
            Amount = dtoAssetTransaction.Amount;
            Note = dtoAssetTransaction.Note;
            TransactionTypes = sliTransactionTypes;
            SelectedTransactionTypeId = dtoAssetTransaction.TransactionTypeId.ToString();
            TransactionCategories = sliTransactionCategories;
            SelectedTransactionCategoryId = dtoAssetTransaction.TransactionCategoryId.ToString();
        }


        public int Id { get; set; }
        public int AssetId { get; set; }
        [Display(Name = "Asset Name")]
        public string AssetName { get; set; }
        [Display(Name = "Asset Type")]
        public string AssetTypeName { get; set; }
        [Display(Name = "Check Number")]
        public string CheckNumber { get; set; }
        [Required]
        [Display(Name = "Due")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public string DueDate { get; set; }
        [Display(Name = "Cleared")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public string ClearDate { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public string Note { get; set; }
        [Required]
        [Display(Name = "Type")]
        public string SelectedTransactionTypeId { get; set; }
        public IEnumerable<SelectListItem> TransactionTypes { get; set; }
        [Required]
        [Display(Name = "Category")]
        public string SelectedTransactionCategoryId { get; set; }
        public IEnumerable<SelectListItem> TransactionCategories { get; set; }
    }
}
