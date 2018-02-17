using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Financial.Core.ViewModels.AssetTransaction
{
    public class EditViewModel
    {
        public EditViewModel() { }

        public EditViewModel(Models.AssetTransaction dtoAssetTransaction, Models.Asset dtoAsset, Models.AssetType dtoAssetType,
            List<SelectListItem> sliTransactionTypes, List<SelectListItem> sliTransactionCategories, List<SelectListItem> sliTransactionDescriptions)
        {
            Id = dtoAssetTransaction.Id;
            AssetId = dtoAsset.Id;
            AssetName = dtoAsset.Name;
            AssetTypeName = dtoAssetType.Name;
            Date = dtoAssetTransaction.TransactionDate;
            CheckNumber = dtoAssetTransaction.CheckNumber;
            Amount = dtoAssetTransaction.Amount;
            Note = dtoAssetTransaction.Note;
            TransactionTypes = sliTransactionTypes;
            SelectedTransactionTypeId = dtoAssetTransaction.TransactionTypeId.ToString();
            TransactionCategories = sliTransactionCategories;
            SelectedTransactionCategoryId = dtoAssetTransaction.TransactionCategoryId.ToString();
            TransactionDescriptions = sliTransactionDescriptions;
            SelectedTransactionDescriptionId = dtoAssetTransaction.TransactionDescriptionId.ToString();
            IsActive = dtoAssetTransaction.IsActive;
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
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Date { get; set; }
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
        [Required]
        [Display(Name = "Description")]
        public string SelectedTransactionDescriptionId { get; set; }
        public IEnumerable<SelectListItem> TransactionDescriptions { get; set; }
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
    }
}
