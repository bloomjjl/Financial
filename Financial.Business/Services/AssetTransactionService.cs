using Financial.Business.ServiceInterfaces;
using Financial.Business.Utilities;
using Financial.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Financial.Business.Services
{
    public class AssetTransactionService : IAssetTransactionService
    {
        private IUnitOfWork _unitOfWork;

        public AssetTransactionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        // Index


        public List<Business.Models.AssetTransaction> GetListOfActiveTransactions()
        {
            // create object to return
            var bmAssetTransactionList = new List<Business.Models.AssetTransaction>();

            // get active transactions
            var dbAssetTransactions = _unitOfWork.AssetTransactions.GetAllActiveByDueDate().ToList();

            // transfer dto to bm
            foreach (var dtoAssetTransaction in dbAssetTransactions)
            {
                // account information
                var dtoAsset = _unitOfWork.Assets.Get(dtoAssetTransaction.AssetId);
                if (dtoAsset == null)
                {
                    return null;
                }

                // account type
                var dtoAssetType = _unitOfWork.AssetTypes.Get(dtoAsset.AssetTypeId);
                if (dtoAssetType == null)
                {
                    return null;
                }

                // get account number
                var dtoAssetSetting = _unitOfWork.AssetSettings.GetAll()
                    .Where(r => r.IsActive)
                    .Where(r => r.AssetId == dtoAsset.Id)
                    .FirstOrDefault(r => r.SettingTypeId == AccountUtility.SettingTypeIdForAccountNumber);
                if (dtoAssetSetting == null)
                {
                    // create valid model
                    dtoAssetSetting = new Core.Models.AssetSetting();
                }

                // transfer to bm
                bmAssetTransactionList.Add(new Business.Models.AssetTransaction(
                    dtoAssetTransaction,
                    dtoAsset,
                    dtoAssetType,
                    dtoAssetSetting));
            }

            return bmAssetTransactionList;
        }


        // Create


        public Business.Models.AssetTransaction GetTransactionOptions(int? assetId)
        {
            // get sli
            var sliAssets = GetAssetsDropDownList(assetId);
            var sliTransactionTypes = GetTransactionTypesDropDownList(null);
            var sliTransactionCategories = GetTransactionCategoriesDropDownList(null);

            // get account information
            var dtoAsset = _unitOfWork.Assets.Get(DataTypeUtility.GetIntegerFromString(assetId.ToString()));
            if (dtoAsset == null)
            {
                // invalid asset provided
                return null;
            }

            // get account number for asset
            var dtoAssetSetting = _unitOfWork.AssetSettings.GetAll()
                .Where(r => r.IsActive)
                .Where(r => r.AssetId == dtoAsset.Id)
                .FirstOrDefault(r => r.SettingTypeId == AccountUtility.SettingTypeIdForAccountNumber);
            if (dtoAssetSetting == null)
            {
                // create valid model
                dtoAssetSetting = new Core.Models.AssetSetting();
            }

            // valid asset information
            return new Business.Models.AssetTransaction(dtoAsset, dtoAssetSetting, sliAssets, sliTransactionTypes, sliTransactionCategories);
        }


        public bool AddTransaction(Business.Models.AssetTransaction bmAssetTransaction)
        {
            // validate input
            if(bmAssetTransaction == null)
            {
                return false;
            }

            // validate ID
            if (_unitOfWork.Assets.Get(bmAssetTransaction.AssetId) == null)
            {
                return false;
            }
            if (_unitOfWork.TransactionTypes.Get(bmAssetTransaction.TransactionTypeId) == null)
            {
                return false;
            }
            if (_unitOfWork.TransactionCategories.Get(bmAssetTransaction.TransactionCategoryId) == null)
            {
                return false;
            }

            // transfer vm to dto
            _unitOfWork.AssetTransactions.Add(new Core.Models.AssetTransaction()
            {
                AssetId = bmAssetTransaction.AssetId,
                TransactionTypeId = bmAssetTransaction.TransactionTypeId,
                TransactionCategoryId = bmAssetTransaction.TransactionCategoryId,
                CheckNumber = bmAssetTransaction.CheckNumber,
                DueDate = bmAssetTransaction.DueDate,
                ClearDate = bmAssetTransaction.ClearDate,
                Amount = bmAssetTransaction.Amount,
                Note = bmAssetTransaction.Note,
                IsActive = true
            });

            // update db
            _unitOfWork.CommitTrans();

            return true;
        }


        // Edit


        public Business.Models.AssetTransaction GetTransactionToEdit(int assetTransactionId)
        {
            var dtoAssetTransaction = _unitOfWork.AssetTransactions.Get(assetTransactionId);
            if (dtoAssetTransaction != null)
            {
                var dtoAsset = _unitOfWork.Assets.Get(dtoAssetTransaction.AssetId);
                if (dtoAsset != null)
                {
                    var dtoAssetType = _unitOfWork.AssetTypes.Get(dtoAsset.AssetTypeId);
                    if (dtoAssetType != null)
                    {
                        // add additional identifying info to asset title
                        var assetNameFormatted = GetAssetIdentificationInformation(dtoAsset);

                        // transfer dto to sli
                        var sliTransactionTypes = GetTransactionTypeSelectList(dtoAssetTransaction.TransactionTypeId.ToString());
                        var sliTransactionCategories = GetTransactionCategorySelectList(dtoAssetTransaction.TransactionCategoryId.ToString());

                        return new Business.Models.AssetTransaction(dtoAssetTransaction,
                            dtoAsset,
                            dtoAssetType,
                            assetNameFormatted,
                            sliTransactionTypes,
                            sliTransactionCategories);
                    }
                }
            }

            return null;
        }
        public string GetAssetIdentificationInformation(Core.Models.Asset dtoAsset)
        {
            // validate input
            if(dtoAsset == null)
            {
                return string.Empty;
            }

            // get additional information
            var dtoAssetSetting = _unitOfWork.AssetSettings.GetAll()
                .Where(r => r.IsActive)
                .FirstOrDefault(r => r.AssetId == dtoAsset.Id);
            if(dtoAssetSetting != null)
            {
                return AccountUtility.FormatAccountName(dtoAsset.Name, dtoAsset.AssetTypeId, dtoAssetSetting.Value);
            }

            // get standard information
            return dtoAsset.Name;
        }
        public List<SelectListItem> GetTransactionTypeSelectList(string selectedId)
        {
            return _unitOfWork.TransactionTypes.GetAll()
                .Where(r => r.IsActive)
                .Select(r => new SelectListItem()
                {
                    Value = r.Id.ToString(),
                    Selected = r.Id.ToString() == selectedId,
                    Text = r.Name
                })
                .OrderBy(r => r.Text)
                .ToList();
        }
        public List<SelectListItem> GetTransactionCategorySelectList(string selectedId)
        {
            return _unitOfWork.TransactionCategories.GetAll()
                .Where(r => r.IsActive)
                .Select(r => new SelectListItem()
                {
                    Value = r.Id.ToString(),
                    Selected = r.Id.ToString() == selectedId,
                    Text = r.Name
                })
                .OrderBy(r => r.Text)
                .ToList();
        }


        public bool UpdateTransaction(Business.Models.AssetTransaction bmAssetTransaction)
        {
            // validate input
            if(bmAssetTransaction == null)
            {
                return false;
            }

            // validate Id
            if (_unitOfWork.Assets.Get(bmAssetTransaction.AssetId) == null ||
                _unitOfWork.TransactionTypes.Get(bmAssetTransaction.TransactionTypeId) == null ||
                _unitOfWork.TransactionCategories.Get(bmAssetTransaction.TransactionCategoryId) == null)
            {
                return false;
            }

            // get dto
            var dtoAssetTransaction = _unitOfWork.AssetTransactions.Get(bmAssetTransaction.AssetTransactionId);
            if(dtoAssetTransaction == null)
            {
                return false;
            }

            // update dto
            dtoAssetTransaction.TransactionTypeId = bmAssetTransaction.TransactionTypeId;
            dtoAssetTransaction.TransactionCategoryId = bmAssetTransaction.TransactionCategoryId;
            dtoAssetTransaction.CheckNumber = bmAssetTransaction.CheckNumber;
            dtoAssetTransaction.DueDate = bmAssetTransaction.DueDate;
            dtoAssetTransaction.ClearDate = bmAssetTransaction.ClearDate;
            dtoAssetTransaction.Amount = bmAssetTransaction.Amount;
            dtoAssetTransaction.Note = bmAssetTransaction.Note;

            // update db
            _unitOfWork.CommitTrans();

            return true;
        }


        // Delete


        public Business.Models.AssetTransaction GetTransactionToDelete(int assetTransactionId)
        {
            var dtoAssetTransaction = _unitOfWork.AssetTransactions.Get(assetTransactionId);
            if (dtoAssetTransaction != null)
            {
                var dtoAsset = _unitOfWork.Assets.Get(dtoAssetTransaction.AssetId);
                if (dtoAsset != null)
                {
                    var dtoAssetType = _unitOfWork.AssetTypes.Get(dtoAsset.AssetTypeId);
                    if (dtoAssetType != null)
                    {
                        var dtoTransactionType = _unitOfWork.TransactionTypes.Get(dtoAssetTransaction.TransactionTypeId);
                        if(dtoTransactionType != null)
                        {
                            var dtoTransactionCategory = _unitOfWork.TransactionCategories.Get(dtoAssetTransaction.TransactionCategoryId);
                            if (dtoTransactionCategory != null)
                            {
                                // add additional identifying info to asset title
                                var assetNameFormatted = GetAssetIdentificationInformation(dtoAsset);

                                return new Business.Models.AssetTransaction(dtoAssetTransaction,
                                    dtoAsset,
                                    dtoAssetType,
                                    assetNameFormatted,
                                    dtoTransactionType,
                                    dtoTransactionCategory);
                            }
                        }
                    }
                }
            }

            return null;
        }


        public bool DeleteTransaction(int assetTransactionId)
        {
            // get dto
            var dtoAssetTransaction = _unitOfWork.AssetTransactions.Get(assetTransactionId);
            if (dtoAssetTransaction == null)
            {
                return false;
            }

            // update dto
            dtoAssetTransaction.IsActive = false;
            
            // update db
            _unitOfWork.CommitTrans();

            return true;
        }




        // -------------------------------





        public List<SelectListItem> GetAssetsDropDownList(int? selectedId)
        {
            var sliAssets = new List<SelectListItem>();

            var dbAssets = _unitOfWork.Assets.GetAll()
                .Where(r => r.IsActive)
                .ToList();

            foreach (var dtoAsset in dbAssets)
            {
                // get account number for asset
                var dtoAssetSetting = _unitOfWork.AssetSettings.GetAll()
                    .Where(r => r.IsActive)
                    .Where(r => r.AssetId == dtoAsset.Id)
                    .FirstOrDefault(r => r.SettingTypeId == AccountUtility.SettingTypeIdForAccountNumber);
                if (dtoAssetSetting == null)
                {
                    // create valid model
                    dtoAssetSetting = new Core.Models.AssetSetting();
                }

                sliAssets.Add(new SelectListItem()
                    {
                        Value = dtoAsset.Id.ToString(),
                        Selected = dtoAsset.Id == selectedId,
                        Text = AccountUtility.FormatAccountName(dtoAsset.Name, dtoAsset.AssetTypeId, dtoAssetSetting.Value)
                });
            }

            return sliAssets;
        }

        public List<SelectListItem> GetTransactionTypesDropDownList(int? selectedId)
        {
            return _unitOfWork.TransactionTypes.GetAll()
                .Where(r => r.IsActive)
                .Select(r => new SelectListItem()
                {
                    Value = r.Id.ToString(),
                    Selected = r.Id == selectedId,
                    Text = r.Name
                })
                .ToList();
        }

        public List<SelectListItem> GetTransactionCategoriesDropDownList(int? selectedId)
        {
            return _unitOfWork.TransactionCategories.GetAll()
                .Where(r => r.IsActive)
                .Select(r => new SelectListItem()
                {
                    Value = r.Id.ToString(),
                    Selected = r.Id == selectedId,
                    Text = r.Name
                })
                .ToList();
        }



        public string CreateTransaction(Business.Models.AssetTransaction bmAssetTransaction)
        {
            // validate input
            if(bmAssetTransaction.AssetId <= 0 ||
                bmAssetTransaction.TransactionTypeId <= 0 ||
                bmAssetTransaction.TransactionCategoryId <= 0 ||
                bmAssetTransaction.DueDate == new DateTime() ||
                bmAssetTransaction.ClearDate == new DateTime())
            {
                return "Information not valid.";
            }

            // transfer bm to dto
            _unitOfWork.AssetTransactions.Add(new Core.Models.AssetTransaction()
            {
                AssetId = bmAssetTransaction.AssetId,
                TransactionTypeId = bmAssetTransaction.TransactionTypeId,
                TransactionCategoryId = bmAssetTransaction.TransactionCategoryId,
                CheckNumber = bmAssetTransaction.CheckNumber,
                DueDate = bmAssetTransaction.DueDate,
                ClearDate = bmAssetTransaction.ClearDate,
                Amount = bmAssetTransaction.Amount,
                Note = bmAssetTransaction.Note,
                IsActive = true
            });

            // update db
            _unitOfWork.CommitTrans();

            return "Success";
        }

        public Business.Models.AssetTransaction EditTransaction()
        {
            return new Business.Models.AssetTransaction();
        }

        public string DeleteTransaction()
        {
            var errorMessage = string.Empty;
            return errorMessage;
        }

    }
}
