using Financial.Business.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Financial.Business.Models
{
    public class AccountTransaction
    {
        public AccountTransaction()
        {

        }

        public AccountTransaction(Core.Models.Asset dbAsset)
        {
            AssetId = dbAsset.Id;
            AssetName = dbAsset.Name;
        }

        public AccountTransaction(Core.Models.Asset dbAsset, string assetNameFormatted)
        {
            AssetId = dbAsset.Id;
            AssetName = assetNameFormatted;
        }

        public AccountTransaction(Core.Models.AssetTransaction dbAssetTransaction, 
            Core.Models.Asset dbAsset)
        {
            AssetTransactionId = dbAssetTransaction.Id;
            AssetId = dbAsset.Id;
            AssetName = dbAsset.Name;
            AssetTypeId = dbAsset.AssetTypeId;
            AssetTypeName = dbAsset.AssetType.Name;
            DueDate = dbAssetTransaction.DueDate;
            ClearDate = dbAssetTransaction.ClearDate;
            Amount = dbAssetTransaction.Amount;
            Note = dbAssetTransaction.Note;
        }

        public AccountTransaction(Core.Models.AssetTransaction dbAssetTransaction,
            Core.Models.Asset dbAsset,
            Core.Models.AssetType dbAssetType,
            string assetNameFormatted)
        {
            AssetTransactionId = dbAssetTransaction.Id;
            Amount = dbAssetTransaction.Amount;
            CheckNumber = dbAssetTransaction.CheckNumber;
            ClearDate = dbAssetTransaction.ClearDate;
            DueDate = dbAssetTransaction.DueDate;
            Note = dbAssetTransaction.Note;
            AssetId = dbAsset.Id;
            AssetName = assetNameFormatted;
            AssetTypeId = dbAssetType.Id;
            AssetTypeName = dbAssetType.Name;
            SelectedTransactionCategoryId = dbAssetTransaction.TransactionCategoryId.ToString();
        }

        public AccountTransaction(Core.Models.AssetTransaction dbAssetTransaction,
            Core.Models.Asset dbAsset,
            Core.Models.AssetType dbAssetType,
            Core.Models.TransactionType dbTransactionType,
            Core.Models.TransactionCategory dbTransactionCategory,
            string assetNameFormatted)
        {
            AssetTransactionId = dbAssetTransaction.Id;
            Amount = dbAssetTransaction.Amount;
            CheckNumber = dbAssetTransaction.CheckNumber;
            ClearDate = dbAssetTransaction.ClearDate;
            DueDate = dbAssetTransaction.DueDate;
            Note = dbAssetTransaction.Note;
            AssetId = dbAsset.Id;
            AssetName = assetNameFormatted;
            AssetTypeId = dbAssetType.Id;
            AssetTypeName = dbAssetType.Name;
            TransactionTypeName = dbTransactionType.Name;
            TransactionCategoryName = dbTransactionCategory.Name;
        }


        public int AssetTransactionId { get; set; }

        public int AssetId { get; set; }
        public string AssetName { get; set; }
        //public List<SelectListItem> AssetSelectList { get; set; }
        //public string SelectedAssetId { get; set; }

        public int AssetTypeId { get; set; }
        public string AssetTypeName { get; set; }

        public int TransactionTypeId { get; set; }
        public string TransactionTypeName { get; set; }
        //public List<SelectListItem> TransactionTypeSelectList { get; set; }
        //public string SelectedTransactionTypeId { get; set; }

        public int TransactionCategoryId { get; set; }
        public string TransactionCategoryName { get; set; }
        //public List<SelectListItem> TransactionCategorySelectList { get; set; }
        public string SelectedTransactionCategoryId { get; set; }

        public string CheckNumber { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime ClearDate { get; set; }
        public decimal Amount { get; set; }
        public string Note { get; set; }

    }
}
