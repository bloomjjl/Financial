using Financial.Business.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Financial.Business.Models
{
    public class AssetTransaction
    {
        public AssetTransaction()
        {

        }

        public AssetTransaction(Core.Models.AssetTransaction dtoAssetTransaction, 
            Core.Models.Asset dtoAsset,
            Core.Models.AssetType dtoAssetType,
            Core.Models.AssetSetting dtoAssetSetting)
        {
            AssetTransactionId = dtoAssetTransaction.Id;
            AssetId = dtoAsset.Id;
            AssetName = AccountUtility.FormatAccountName(dtoAsset.Name, dtoAsset.AssetTypeId, dtoAssetSetting.Value);
            AssetTypeId = dtoAssetType.Id;
            AssetTypeName = dtoAssetType.Name;
            DueDate = dtoAssetTransaction.DueDate;
            ClearDate = dtoAssetTransaction.ClearDate;
            Amount = TransactionUtility.FormatAmount(dtoAssetTransaction.TransactionTypeId, dtoAssetTransaction.Amount);
            Note = dtoAssetTransaction.Note;
        }

        public AssetTransaction(List<SelectListItem> sliAssets,
            List<SelectListItem> sliTransactionTypes,
            List<SelectListItem> sliTransactionCategories)
        {
            AssetSelectList = sliAssets;
            TransactionTypeSelectList = sliTransactionTypes;
            TransactionCategorySelectList = sliTransactionCategories;
        }

        public AssetTransaction(Core.Models.Asset dtoAsset,
            Core.Models.AssetSetting dtoAssetSetting,
            List<SelectListItem> sliAssets,
            List<SelectListItem> sliTransactionTypes,
            List<SelectListItem> sliTransactionCategories)
        {
            AssetId = dtoAsset.Id;
            AssetSelectList = sliAssets;
            TransactionTypeSelectList = sliTransactionTypes;
            TransactionCategorySelectList = sliTransactionCategories;
        }

        public AssetTransaction(Core.Models.AssetTransaction dtoAssetTransaction,
            Core.Models.Asset dtoAsset,
            Core.Models.AssetType dtoAssetType,
            string assetNameFormatted,
            Core.Models.TransactionType dtoTransactionType,
            Core.Models.TransactionCategory dtoTransactionCategory)
        {
            AssetTransactionId = dtoAssetTransaction.Id;
            Amount = dtoAssetTransaction.Amount;
            CheckNumber = dtoAssetTransaction.CheckNumber;
            ClearDate = dtoAssetTransaction.ClearDate;
            DueDate = dtoAssetTransaction.DueDate;
            Note = dtoAssetTransaction.Note;
            AssetId = dtoAsset.Id;
            AssetName = assetNameFormatted;
            AssetTypeId = dtoAssetType.Id;
            AssetTypeName = dtoAssetType.Name;
            TransactionTypeName = dtoTransactionType.Name;
            TransactionCategoryName = dtoTransactionCategory.Name;
        }

        public AssetTransaction(Core.Models.AssetTransaction dtoAssetTransaction,
            Core.Models.Asset dtoAsset,
            Core.Models.AssetType dtoAssetType,
            string assetNameFormatted,
            List<SelectListItem> sliTransactionTypes,
            List<SelectListItem> sliTransactionCategories)
        {
            AssetTransactionId = dtoAssetTransaction.Id;
            Amount = dtoAssetTransaction.Amount;
            CheckNumber = dtoAssetTransaction.CheckNumber;
            ClearDate = dtoAssetTransaction.ClearDate;
            DueDate = dtoAssetTransaction.DueDate;
            Note = dtoAssetTransaction.Note;
            AssetId = dtoAsset.Id;
            AssetName = assetNameFormatted;
            AssetTypeId = dtoAssetType.Id;
            AssetTypeName = dtoAssetType.Name;
            TransactionTypeSelectList = sliTransactionTypes;
            SelectedTransactionTypeId = dtoAssetTransaction.TransactionTypeId.ToString();
            TransactionCategorySelectList = sliTransactionCategories;
            SelectedTransactionCategoryId = dtoAssetTransaction.TransactionCategoryId.ToString();
        }

        public int AssetTransactionId { get; set; }

        public int AssetId { get; set; }
        public string AssetName { get; set; }
        public List<SelectListItem> AssetSelectList { get; set; }
        public string SelectedAssetId { get; set; }

        public int AssetTypeId { get; set; }
        public string AssetTypeName { get; set; }

        public int TransactionTypeId { get; set; }
        public string TransactionTypeName { get; set; }
        public List<SelectListItem> TransactionTypeSelectList { get; set; }
        public string SelectedTransactionTypeId { get; set; }

        public int TransactionCategoryId { get; set; }
        public string TransactionCategoryName { get; set; }
        public List<SelectListItem> TransactionCategorySelectList { get; set; }
        public string SelectedTransactionCategoryId { get; set; }

        public string CheckNumber { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime ClearDate { get; set; }
        public decimal Amount { get; set; }
        public string Note { get; set; }

    }
}
