using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Financial.WebApplication.Models.ViewModels.AccountTransaction
{
    public class CreateViewModel
    {
        public CreateViewModel() { }

        public CreateViewModel(List<SelectListItem> sliAccount, List<SelectListItem> sliTransactionType, List<SelectListItem> sliTransactionCategory)
        {
            Accounts = sliAccount;
            TransactionTypes = sliTransactionType;
            TransactionCategories = sliTransactionCategory;
        }
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
        public CreateViewModel(Business.Models.AccountTransaction bmAssetTransaction)
        {
            AssetId = bmAssetTransaction.AssetId;
            AccountName = bmAssetTransaction.AssetName;
            //Assets = bmAssetTransaction.AssetSelectList;
            AssetTypeName = bmAssetTransaction.AssetTypeName;
            //TransactionTypes = bmAssetTransaction.TransactionTypeSelectList;
            SelectedTransactionTypeId = bmAssetTransaction.TransactionTypeId.ToString();
            //TransactionCategories = bmAssetTransaction.TransactionCategorySelectList;
            SelectedTransactionCategoryId = bmAssetTransaction.TransactionCategoryId.ToString();
        }

        public int AssetId { get; set; }
        [Display(Name = "Account Name")]
        public string AccountName { get; set; }
        [Display(Name = "Account Type")]
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
        public string SelectedAccountId { get; set; }
        public IEnumerable<SelectListItem> Accounts { get; set; }
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
