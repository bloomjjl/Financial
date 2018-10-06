using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Financial.WebApplication.Models.ViewModels.AccountTransaction
{
    public class DeleteViewModel
    {
        public DeleteViewModel() { }

        public DeleteViewModel(Business.Models.AccountTransaction bmAssetTransaction)
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
            TransactionTypeName = bmAssetTransaction.TransactionTypeName;
            TransactionCategoryName = bmAssetTransaction.TransactionCategoryName;
        }


        public int Id { get; set; }
        public int AssetId { get; set; }
        [Display(Name = "Asset Name")]
        public string AssetName { get; set; }
        [Display(Name = "Asset Type")]
        public string AssetTypeName { get; set; }
        [Display(Name = "Check Number")]
        public string CheckNumber { get; set; }
        [Display(Name = "Due")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; }
        [Display(Name = "Cleared")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ClearDate { get; set; }
        public decimal Amount { get; set; }
        public string Note { get; set; }
        [Display(Name = "Type")]
        public string TransactionTypeName { get; set; }
        [Display(Name = "Category")]
        public string TransactionCategoryName { get; set; }
    }
}