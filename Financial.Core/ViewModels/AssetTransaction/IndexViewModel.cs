﻿using System;
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

        public IndexViewModel(Models.AssetTransaction dtoAssetTransaction, Models.Asset dtoAsset, string transactionType, decimal total)
        {
            Id = dtoAssetTransaction.Id;
            AssetId = dtoAsset.Id;
            AssetName = dtoAsset.Name;
            DueDate = dtoAssetTransaction.DueDate.ToString("MM/dd/yyyy");
            ClearDate = dtoAssetTransaction.ClearDate.ToString("MM/dd/yyyy");
            TransactionType = transactionType;
            Amount = dtoAssetTransaction.Amount;
            Total = total;
        }


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
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal Total { get; set; }

    }
}
