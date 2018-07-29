using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Financial.WebApplication.Models.ViewModels.AssetTransaction
{
    public class CreateViewModel
    {
        public CreateViewModel() { }
        /*
        public CreateViewModel(Core.Models.Asset dtoAsset,string assetNameAdditionalInformaiton,
            Core.Models.AssetType dtoAssetType, DateTime date,
            List<SelectListItem> sliTransactionTypes, List<SelectListItem> sliTransactionCategories)
        {
            AssetId = dtoAsset.Id;
            AssetName = dtoAsset.Name + assetNameAdditionalInformaiton;
            AssetTypeName = dtoAssetType.Name;
            DueDate = date.ToString("MM/dd/yyyy");
            TransactionTypes = sliTransactionTypes;
            TransactionCategories = sliTransactionCategories;
        }*/
        public CreateViewModel(Business.Models.AssetTransaction bmAssetTransaction)
        {
            AssetId = bmAssetTransaction.AssetId;
            AssetName = bmAssetTransaction.AssetName;
            Assets = bmAssetTransaction.AssetSelectList;
            AssetTypeName = bmAssetTransaction.AssetTypeName;
            TransactionTypes = bmAssetTransaction.TransactionTypeSelectList;
            SelectedTransactionTypeId = bmAssetTransaction.TransactionTypeId.ToString();
            TransactionCategories = bmAssetTransaction.TransactionCategorySelectList;
            SelectedTransactionCategoryId = bmAssetTransaction.TransactionCategoryId.ToString();
        }

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
        public DateTime DueDate { get; set; }
        [Display(Name = "Cleared")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ClearDate { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public string Note { get; set; }
        [Required]
        [Display(Name = "Asset")]
        public string SelectedAssetId { get; set; }
        public IEnumerable<SelectListItem> Assets { get; set; }
        [Required]
        [Display(Name = "Type")]
        public string SelectedTransactionTypeId { get; set; }
        public IEnumerable<SelectListItem> TransactionTypes { get; set; }
        [Required]
        [Display(Name = "Category")]
        public string SelectedTransactionCategoryId { get; set; }
        public IEnumerable<SelectListItem> TransactionCategories { get; set; }
        //[Required]
        //[Display(Name = "Description")]
        //public string SelectedTransactionDescriptionId { get; set; }
        //public IEnumerable<SelectListItem> TransactionDescriptions { get; set; }
    }
}
