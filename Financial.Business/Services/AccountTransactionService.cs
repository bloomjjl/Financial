using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Financial.Business.Models;
using Financial.Business.ServiceInterfaces;
using Financial.Business.Utilities;
using Financial.Data;
using Financial.Core.Models;

namespace Financial.Business.Services
{
    public class AccountTransactionService : IAccountTransactionService
    {
        private IUnitOfWork _unitOfWork;

        public AccountTransactionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        // Index


        public List<AccountTransaction> GetListOfActiveTransactions()
        {
            // create object to return
            var bmAssetTransactionList = new List<AccountTransaction>();

            // get active transactions
            var dbAssetTransactionList = _unitOfWork.AssetTransactions.GetAllActiveByDueDate().ToList();

            // transfer dto to bm
            foreach (var dbAssetTransaction in dbAssetTransactionList)
            {
                // account information
                var dbAsset = _unitOfWork.Assets.Get(dbAssetTransaction.AssetId);
                if (dbAsset == null)
                    return new List<AccountTransaction>();

                // format values
                dbAsset.Name = GetAssetNameWithAccountNumber(dbAsset);
                dbAssetTransaction.Amount = TransactionUtility.FormatAmount(
                    dbAssetTransaction.TransactionTypeId, 
                    dbAssetTransaction.Amount);

                // transfer to bm
                bmAssetTransactionList.Add(new AccountTransaction(
                    dbAssetTransaction,
                    dbAsset));
            }

            return bmAssetTransactionList;
        }


        // Create


        public Account GetAccountForTransaction(int? assetId)
        {
            if (assetId == null || assetId == 0)
                return null;

            var dbAsset = _unitOfWork.Assets.Get((int)assetId);
            if (dbAsset == null)
                return null;

            dbAsset.Name = GetAssetNameWithAccountNumber(dbAsset);

            return new Account(dbAsset);
        }
        /*
        public AccountTransaction GetTransactionOptions(int? assetId)
        {
            var intAssetId = DataTypeUtility.GetIntegerFromString(assetId.ToString());

            // get asset information
            var dtoAsset = GetAssetFromDatabase(intAssetId);
            if (dtoAsset == null)
            {
                throw new ArgumentNullException();
                //return new Business.Models.AssetTransaction();
            }

            // asset name with additional information
            var formattedAssetName = GetAssetNameWithAccountNumber(dtoAsset);

            // transfer dto to bm
            var bmAssetTransaction = new AccountTransaction(dtoAsset, formattedAssetName);

            // get sli
            //bmAssetTransaction.AssetSelectList = GetAssetSelectList(intAssetId.ToString());
            //bmAssetTransaction.TransactionTypeSelectList = GetTransactionTypeSelectList(null);
            //bmAssetTransaction.TransactionCategorySelectList = GetTransactionCategorySelectList(null);

            // valid asset information
            return bmAssetTransaction;
        }
        */
        /*
        private Core.Models.Asset GetAssetFromDatabase(int assetId)
        {
            return _unitOfWork.Assets.Get(assetId);
        }
        */
        private string GetAssetNameWithAccountNumber(Asset dbAsset)
        {
            var dbAssetSetting = _unitOfWork.AssetSettings.GetActive(dbAsset.Id, SettingType.IdForAccountNumber);
            if (dbAssetSetting == null)
                return dbAsset.Name;

            return AccountUtility.FormatAccountName(dbAsset.Name, dbAsset.AssetTypeId, dbAssetSetting.Value);
        }
        public List<SelectListItem> GetAccountSelectList(string selectedId)
        {
            return _unitOfWork.Assets.GetAllActiveOrderedByName()
                .Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = GetAssetNameWithAccountNumber(r),
                    Selected = r.Id.ToString() == selectedId
                })
                .ToList();
        }
        public List<SelectListItem> GetTransactionTypeSelectList(string selectedId)
        {
            return _unitOfWork.TransactionTypes.GetAllActiveOrderedByName()
                .Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.Name,
                    Selected = r.Id.ToString() == selectedId,
                })
                .ToList();
        }
        public List<SelectListItem> GetTransactionCategorySelectList(string selectedId)
        {
            return _unitOfWork.TransactionCategories.GetAllActiveOrderedByName()
                .Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.Name,
                    Selected = r.Id.ToString() == selectedId,
                })
                .ToList();
            /*
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
            */
        }


        public bool AddTransaction(Business.Models.AccountTransaction bmAssetTransaction)
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


        public Business.Models.AccountTransaction GetTransactionToEdit(int assetTransactionId)
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

                        return new Business.Models.AccountTransaction(dtoAssetTransaction,
                            dtoAsset,
                            dtoAssetType,
                            assetNameFormatted
                            //sliTransactionTypes,
                            //sliTransactionCategories
                            );
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
 

        public bool UpdateTransaction(Business.Models.AccountTransaction bmAssetTransaction)
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


        public AccountTransaction GetTransactionToDelete(int assetTransactionId)
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
                                // add additional identifying info to asset name
                                dtoAsset.Name = GetAssetIdentificationInformation(dtoAsset);

                                return new AccountTransaction(dtoAssetTransaction, dtoAsset);
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

    }
}
