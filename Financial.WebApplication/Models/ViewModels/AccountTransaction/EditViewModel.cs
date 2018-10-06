using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Financial.WebApplication.Models.ViewModels.AccountTransaction
{
    public class EditViewModel
    {
        public EditViewModel() { }

        public EditViewModel(Business.Models.AccountTransaction bmAssetTransaction)
        {
            Id = bmAssetTransaction.AssetTransactionId;
            AssetId = bmAssetTransaction.AssetId;
            AssetName = bmAssetTransaction.AssetName;
            AssetTypeName = bmAssetTransaction.AssetTypeName;
            DueDate = bmAssetTransaction.DueDate;
            ClearDate = bmAssetTransaction.ClearDate;
            CheckNumber = bmAssetTransaction.CheckNumber;
            Amount = bmAssetTransaction.Amount;
            Note = bmAssetTransaction.Note;
            //TransactionTypes = bmAssetTransaction.TransactionTypeSelectList;
            //SelectedTransactionTypeId = bmAssetTransaction.SelectedTransactionTypeId;
            //TransactionCategories = bmAssetTransaction.TransactionCategorySelectList;
            SelectedTransactionCategoryId = bmAssetTransaction.SelectedTransactionCategoryId;
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
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; }
        [Display(Name = "Cleared")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ClearDate { get; set; }
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
